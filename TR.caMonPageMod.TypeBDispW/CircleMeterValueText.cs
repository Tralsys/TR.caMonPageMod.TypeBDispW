using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TypeBDispW
{
	public class CircleMeterValueText : Control
	{
		public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
		static public readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleMeterValueText), new(PositionPropertyChanged));

		public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
		static public readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(CircleMeterValueText), new(PositionPropertyChanged));

		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleMeterValueText));

		public int StartValue { get => (int)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(int), typeof(CircleMeterValueText), new(PositionPropertyChanged));

		public int EndValue { get => (int)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(int), typeof(CircleMeterValueText), new(PositionPropertyChanged));

		public Style TextStyle { get => (Style)GetValue(TextStyleProperty); set => SetValue(TextStyleProperty, value); }
		static public readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(nameof(TextStyle), typeof(Style), typeof(CircleMeterValueText));

		public double TextValue { get => (double)GetValue(TextValueProperty); set => SetValue(TextValueProperty, value); }
		static public readonly DependencyProperty TextValueProperty = DependencyProperty.Register(nameof(TextValue), typeof(double), typeof(CircleMeterValueText), new(PositionPropertyChanged));
		public double Angle { get => (double)GetValue(AngleProperty); private set => SetValue(AnglePropertyKey, value); }
		static private readonly DependencyPropertyKey AnglePropertyKey = DependencyProperty.RegisterReadOnly(nameof(Angle), typeof(double), typeof(CircleMeterValueText), new());
		static public readonly DependencyProperty AngleProperty = AnglePropertyKey.DependencyProperty;

		static CircleMeterValueText() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMeterValueText), new FrameworkPropertyMetadata(typeof(CircleMeterValueText)));
		static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as CircleMeterValueText)?.PositionPropertyChanger();
		void PositionPropertyChanger()
		{
			double DegPerValue = (EndAngle - StartAngle) / (EndValue - StartValue);
			Angle = StartAngle + (TextValue * DegPerValue);
		}

	}
}
