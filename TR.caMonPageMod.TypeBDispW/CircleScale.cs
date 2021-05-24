using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TR.caMonPageMod.TypeBDispW
{
	public interface ICircleScaleProperties
	{
		Brush Foreground { get; }
		double StartAngle { get; }
		double EndAngle { get; }
		double Radius { get; }
		int StartValue { get; }
		int EndValue { get; }
		int MarkStep { get; }
		double MarkHeight { get; }
		double MarkWidth { get; }
		Func<int, bool> ExecludeWhenTrue { get; }
	}

	public class CircleScale : Control, ICircleScaleProperties
	{
		/// <summary>目盛の開始角度</summary>
		public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
		/// <summary>目盛の開始角度の依存関係プロパティ</summary>
		static public readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleScale), new(ScalePropertyChanged));

		/// <summary>目盛の終了角度</summary>
		public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
		/// <summary>目盛の終了角度の依存関係プロパティ</summary>
		static public readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(CircleScale), new(ScalePropertyChanged));

		//--- <-(Padding)-><-(ActualRadius)-> *(Center)
		//--- <--------   (Radius)  --------> *(Center)
		/// <summary>円形目盛計器の半径</summary>
		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		/// <summary>円形目盛計器の半径の依存関係プロパティ</summary>
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleScale), new(ScalePropertyChanged));

		/// <summary>メモリを配置する開始値</summary>
		public int StartValue { get => (int)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		/// <summary>メモリを配置する開始値の依存関係プロパティ</summary>
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(int), typeof(CircleScale), new(ScalePropertyChanged));

		/// <summary>メモリを配置する終了値</summary>
		public int EndValue { get => (int)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		/// <summary>メモリを配置する終了値の依存関係プロパティ</summary>
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(int), typeof(CircleScale), new(ScalePropertyChanged));

		/// <summary>目盛を配置する間隔</summary>
		public int MarkStep { get => (int)GetValue(MarkStepProperty); set => SetValue(MarkStepProperty, value); }
		/// <summary>目盛を配置する間隔の依存関係プロパティ</summary>
		static public readonly DependencyProperty MarkStepProperty = DependencyProperty.Register(nameof(MarkStep), typeof(int), typeof(CircleScale), new(ScalePropertyChanged));

		/// <summary>目盛の高さ</summary>
		public double MarkHeight { get => (double)GetValue(MarkHeightProperty); set => SetValue(MarkHeightProperty, value); }
		/// <summary>目盛の高さの依存関係プロパティ</summary>
		static public readonly DependencyProperty MarkHeightProperty = DependencyProperty.Register(nameof(MarkHeight), typeof(double), typeof(CircleScale));

		/// <summary>目盛の幅</summary>
		public double MarkWidth { get => (double)GetValue(MarkWidthProperty); set => SetValue(MarkWidthProperty, value); }
		/// <summary>目盛の幅の依存関係プロパティ</summary>
		static public readonly DependencyProperty MarkWidthProperty = DependencyProperty.Register(nameof(MarkWidth), typeof(double), typeof(CircleScale));

		/// <summary>除外条件を設定する関数  引数に除外するか確認する数値が入り, 返り値は除外する(true)か否か</summary>
		public Func<int, bool> ExecludeWhenTrue { get => GetValue(ExecludeWhenTrueProperty) as Func<int, bool>; set => SetValue(ExecludeWhenTrueProperty, value); }
		/// <summary>除外条件を設定する関数の依存関係プロパティ</summary>
		static public readonly DependencyProperty ExecludeWhenTrueProperty = DependencyProperty.Register(nameof(ExecludeWhenTrue), typeof(Func<int, bool>), typeof(CircleScale), new(ScalePropertyChanged));

		static void ScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is CircleScale cs)
				cs.DrawRectangles();//四角形の再描画
		}

		Grid ScaleBaseGrid { get; set; }

		static CircleScale() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleScale), new FrameworkPropertyMetadata(typeof(CircleScale)));
		
		public CircleScale()
		{
			Loaded += (_, _) =>
			{
				ScaleBaseGrid = Template.FindName(nameof(ScaleBaseGrid), this) as Grid;
				DrawRectangles();
			};
		}

		public void DrawRectangles()
		{
			if (!IsLoaded)
				return;//初回Load時の負荷軽減のため
			if (ScaleBaseGrid is null)
				return; //nullなら以降の処理は実行できないため

			Binding GetBinding(string name) => new(name) { Source = this };

			if (EndValue == StartValue || MarkStep <= 0)
			{
				ScaleBaseGrid.Children.Clear();
				return;
			}


			//四角形の総数が変化しているか調べる
			//変化していて, 増加なら追加, 減少なら削除
			int Count = 0;
			for (int i = StartValue; i <= EndValue; i += MarkStep)
				if (ExecludeWhenTrue?.Invoke(i) != true)//NULL or trueで分岐
					Count++;//描画個数インクリメント

			if (ScaleBaseGrid.Children.Count != Count)
			{
				/* eg.
				 * 5 > 2 =>> Remove 2,3,4
					 * 2 < 5 =>> Add 3 instance
				*/
				int Diff = Math.Abs(ScaleBaseGrid.Children.Count - Count);
				if (ScaleBaseGrid.Children.Count > Count)
					ScaleBaseGrid.Children.RemoveRange(Count, Diff);
				else
					for(int i = 0; i < Diff; i++)
					{
						Rectangle rect = new();
						rect.SetBinding(WidthProperty, GetBinding(nameof(MarkWidth)));
						rect.SetBinding(HeightProperty, GetBinding(nameof(MarkHeight)));
						rect.SetBinding(MarginProperty, GetBinding(nameof(Padding)));
						rect.SetBinding(Shape.FillProperty, GetBinding(nameof(Foreground)));
						rect.VerticalAlignment = VerticalAlignment.Center;
						rect.HorizontalAlignment = HorizontalAlignment.Left;
						rect.RenderTransform = new RotateTransform();
						rect.RenderTransformOrigin = new Point(0, 0.5);
						ScaleBaseGrid.Children.Add(rect);
					}
			}
			
			Count = 0;
			double _AngleStepBy1 = (EndAngle - StartAngle) / (EndValue - StartValue);
			double _StartAngle = StartAngle;//Cache
			double rtRadius = Radius - Padding.Left;
			for (int i = StartValue; i <= EndValue; i += MarkStep)
				if (ExecludeWhenTrue?.Invoke(i) != true)//NULL or trueで分岐
				{
					//var rect = ScaleBaseGrid.Children[Count] as Rectangle;
					var rt = ScaleBaseGrid.Children[Count].RenderTransform as RotateTransform;
					rt.Angle = _StartAngle + (_AngleStepBy1 * i);
					rt.CenterX = rtRadius;
					Count++;
				}
		}
	}
}
