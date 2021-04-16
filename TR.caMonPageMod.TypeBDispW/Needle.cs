using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TypeBDispW
{
	public class Needle : Control
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

		public double RectangleWidth { get => (double)GetValue(RectangleWidthProperty); set => SetValue(RectangleWidthPropertyKey, value); }
		static private readonly DependencyPropertyKey RectangleWidthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(RectangleWidth), typeof(double), typeof(Needle), new());
		static public readonly DependencyProperty RectangleWidthProperty = RectangleWidthPropertyKey.DependencyProperty;

		public Thickness CenterCircleMargin { get => (Thickness)GetValue(CenterCircleMarginProperty); set => SetValue(CenterCircleMarginPropertyKey, value); }
		static private readonly DependencyPropertyKey CenterCircleMarginPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CenterCircleMargin), typeof(Thickness), typeof(Needle), new());
		static public readonly DependencyProperty CenterCircleMarginProperty = CenterCircleMarginPropertyKey.DependencyProperty;

		/// <summary>針の中心点の円の半径</summary>
		public double CenterCircleRadius { get => (double)GetValue(CenterCircleRadiusProperty); set => SetValue(CenterCircleRadiusProperty, value); }
		/// <summary>針の中心点の円の半径の依存関係プロパティ</summary>
		static public readonly DependencyProperty CenterCircleRadiusProperty = DependencyProperty.Register(nameof(CenterCircleRadius), typeof(double), typeof(Needle), new(ChangeCenterCircleMargin));

		static Needle() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Needle), new FrameworkPropertyMetadata(typeof(Needle)));
		public Needle() => DependencyPropertyDescriptor.FromProperty(PaddingProperty, typeof(CircleScale)).AddValueChanged(this, (_, _) => RectangleWidthUpdater());

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

		private void RectangleWidthUpdater() => RectangleWidth = Radius - Padding.Left - TriangleWidth;
		
	}
}
