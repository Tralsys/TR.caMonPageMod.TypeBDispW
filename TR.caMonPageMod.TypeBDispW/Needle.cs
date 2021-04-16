using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TypeBDispW
{
	public interface INeedleProperties
	{
		Brush Foreground { get; }
		double Radius { get; }
		double NeedleHeight { get; }
		double TriangleWidth { get; }
		double WeightRectangleWidth { get; }
		double CenterCircleRadius { get; }
	}

	public class Needle : Control, INeedleProperties
	{
		//<-(Padding)-><--------    (ActualRadius)   --------> *(Center)
		//<-(Padding)-><-(TriangleWidth)-><-(RectangleWidth)-> *(Center) <-(WeightRectangleWidth)->
		//<----------------      (Radius)    ----------------> *(Center)
		/// <summary>針の半径</summary>
		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		/// <summary>針の半径の依存関係プロパティ</summary>
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(Needle), new(RadiusPropertyChanged));

		/// <summary>針の高さ</summary>
		public double NeedleHeight { get => (double)GetValue(NeedleHeightProperty); set => SetValue(NeedleHeightProperty, value); }
		/// <summary>針の高さの依存関係プロパティ</summary>
		static public readonly DependencyProperty NeedleHeightProperty = DependencyProperty.Register(nameof(NeedleHeight), typeof(double), typeof(Needle));

		/// <summary>針の先端の三角形の幅</summary>
		public double TriangleWidth { get => (double)GetValue(TriangleWidthProperty); set => SetValue(TriangleWidthProperty, value); }
		/// <summary>針の先端の三角形の幅の依存関係プロパティ</summary>
		static public readonly DependencyProperty TriangleWidthProperty = DependencyProperty.Register(nameof(TriangleWidth), typeof(double), typeof(Needle), new(TriangleWidthPropertyChanged));

		/// <summary>針のおもり部分の幅</summary>
		public double WeightRectangleWidth { get => (double)GetValue(WeightRectangleWidthProperty); set => SetValue(WeightRectangleWidthProperty, value); }
		/// <summary>針のおもり部分の幅の依存関係プロパティ</summary>
		static public readonly DependencyProperty WeightRectangleWidthProperty = DependencyProperty.Register(nameof(WeightRectangleWidth), typeof(double), typeof(Needle));

		public double RectangleWidth { get => (double)GetValue(RectangleWidthProperty); private set => SetValue(RectangleWidthPropertyKey, value); }
		static private readonly DependencyPropertyKey RectangleWidthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(RectangleWidth), typeof(double), typeof(Needle), new());
		static public readonly DependencyProperty RectangleWidthProperty = RectangleWidthPropertyKey.DependencyProperty;

		public Thickness CenterCircleMargin { get => (Thickness)GetValue(CenterCircleMarginProperty); private set => SetValue(CenterCircleMarginPropertyKey, value); }
		static private readonly DependencyPropertyKey CenterCircleMarginPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CenterCircleMargin), typeof(Thickness), typeof(Needle), new());
		static public readonly DependencyProperty CenterCircleMarginProperty = CenterCircleMarginPropertyKey.DependencyProperty;

		/// <summary>針の中心点の円の半径</summary>
		public double CenterCircleRadius { get => (double)GetValue(CenterCircleRadiusProperty); set => SetValue(CenterCircleRadiusProperty, value); }
		/// <summary>針の中心点の円の半径の依存関係プロパティ</summary>
		static public readonly DependencyProperty CenterCircleRadiusProperty = DependencyProperty.Register(nameof(CenterCircleRadius), typeof(double), typeof(Needle), new(ChangeCenterCircleMargin));

		/// <summary>針移動の開始角度</summary>
		public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
		/// <summary>針移動の開始角度の依存関係プロパティ</summary>
		static public readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(Needle), new(AngleCalcBaseChanged));

		/// <summary>針移動の終了角度</summary>
		public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
		/// <summary>針移動の終了角度の依存関係プロパティ</summary>
		static public readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(Needle), new(AngleCalcBaseChanged));

		/// <summary>針が表す値の開始値</summary>
		public int StartValue { get => (int)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		/// <summary>針が表す値の開始値の依存関係プロパティ</summary>
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(int), typeof(Needle), new(AngleCalcBaseChanged));

		/// <summary>針が表す値の終了値</summary>
		public int EndValue { get => (int)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		/// <summary>針が表す値の終了値の依存関係プロパティ</summary>
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(int), typeof(Needle), new(AngleCalcBaseChanged));

		/// <summary>現在の表示値</summary>
		public double CurrentValue { get => (double)GetValue(CurrentValueProperty); set => SetValue(CurrentValueProperty, value); }
		/// <summary>現在の表示値の依存関係プロパティ</summary>
		static public readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register(nameof(CurrentValue), typeof(double), typeof(Needle), new((d, _) => (d as Needle)?.ChangeCurrentAngle()));

		/// <summary>現在の表示角度</summary>
		public double CurrentAngle { get => (double)GetValue(CurrentAngleProperty); private set => SetValue(CurrentAnglePropertyKey, value); }
		/// <summary>現在の表示角度の依存関係プロパティ</summary>
		static private readonly DependencyPropertyKey CurrentAnglePropertyKey = DependencyProperty.RegisterReadOnly(nameof(CurrentAngle), typeof(double), typeof(Needle), new(0.0));
		/// <summary>現在の表示角度の依存関係プロパティ</summary>
		static public readonly DependencyProperty CurrentAngleProperty = CurrentAnglePropertyKey.DependencyProperty;

		static Needle() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Needle), new FrameworkPropertyMetadata(typeof(Needle)));
		public Needle()
		{
			DependencyPropertyDescriptor.FromProperty(PaddingProperty, typeof(Needle)).AddValueChanged(this, (_, _) => RectangleWidthUpdater());
			Loaded += (s, _) =>
			{
				NeedleRotater = (GetTemplateChild("Needle_Base") as Grid)?.RenderTransform as RotateTransform;
				ChangeCurrentAngle();
			};
		}

		private double AngleCalcMultipler { get; set; }
		private RotateTransform NeedleRotater = null;
		static void AngleCalcBaseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as Needle)?.UpdateAngleCalcMultipler();
		static void RadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as Needle)?.RectangleWidthUpdater();
			ChangeCenterCircleMargin(d, e);
		}
		static void TriangleWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as Needle)?.RectangleWidthUpdater();
		static void ChangeCenterCircleMargin(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (d is not Needle n)
				return;
			n.CenterCircleMargin = new Thickness(n.Radius - n.CenterCircleRadius, 0, 0, 0);
		}
		void ChangeCurrentAngle()
		{
			if (NeedleRotater is not null)
				NeedleRotater.Angle = StartAngle + ((CurrentValue - StartValue) * AngleCalcMultipler);
		}
		void UpdateAngleCalcMultipler()
		{
			AngleCalcMultipler = (StartValue == EndValue) ? 0
				: ((EndAngle - StartAngle) / (EndValue - StartValue));
			ChangeCurrentAngle();
		}
		private void RectangleWidthUpdater() => RectangleWidth = Radius - Padding.Left - TriangleWidth;
	}
}
