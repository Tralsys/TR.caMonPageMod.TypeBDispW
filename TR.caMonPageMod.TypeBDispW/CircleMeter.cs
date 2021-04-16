using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace TR.caMonPageMod.TypeBDispW
{
	public class CircleMeter : Control
	{
		public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
		static public readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleMeter));

		public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
		static public readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(CircleMeter));

		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleMeter));

		public double StartValue { get => (double)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(double), typeof(CircleMeter));

		public double EndValue { get => (double)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(double), typeof(CircleMeter));

		public int MarkLStep { get => (int)GetValue(MarkLStepProperty); set => SetValue(MarkLStepProperty, value); }
		static public readonly DependencyProperty MarkLStepProperty = DependencyProperty.Register(nameof(MarkLStep), typeof(int), typeof(CircleMeter));
		public int MarkMStep { get => (int)GetValue(MarkMStepProperty); set => SetValue(MarkMStepProperty, value); }
		static public readonly DependencyProperty MarkMStepProperty = DependencyProperty.Register(nameof(MarkMStep), typeof(int), typeof(CircleMeter));
		public int MarkSStep { get => (int)GetValue(MarkSStepProperty); set => SetValue(MarkSStepProperty, value); }
		static public readonly DependencyProperty MarkSStepProperty = DependencyProperty.Register(nameof(MarkSStep), typeof(int), typeof(CircleMeter));

		public Style MarkLStyle { get => (Style)GetValue(MarkLStyleProperty); set => SetValue(MarkLStyleProperty, value); }
		static public readonly DependencyProperty MarkLStyleProperty = DependencyProperty.Register(nameof(MarkLStyle), typeof(Style), typeof(CircleMeter));
		public Style MarkMStyle { get => (Style)GetValue(MarkMStyleProperty); set => SetValue(MarkMStyleProperty, value); }
		static public readonly DependencyProperty MarkMStyleProperty = DependencyProperty.Register(nameof(MarkMStyle), typeof(Style), typeof(CircleMeter));
		public Style MarkSStyle { get => (Style)GetValue(MarkSStyleProperty); set => SetValue(MarkSStyleProperty, value); }
		static public readonly DependencyProperty MarkSStyleProperty = DependencyProperty.Register(nameof(MarkSStyle), typeof(Style), typeof(CircleMeter));
		public Style NeedleStyle { get => (Style)GetValue(NeedleStyleProperty); set => SetValue(NeedleStyleProperty, value); }
		static public readonly DependencyProperty NeedleStyleProperty = DependencyProperty.Register(nameof(NeedleStyle), typeof(Style), typeof(CircleMeter));

		public double CurrentValue { get => (double)GetValue(CurrentValueProperty); set => SetValue(CurrentValueProperty, value); }
		static public readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register(nameof(CurrentValue), typeof(double), typeof(CircleMeter));

		public BitmapEffect NeedleBitmapEffect { get => (BitmapEffect)GetValue(NeedleBitmapEffectProperty); set => SetValue(NeedleBitmapEffectProperty, value); }
		static public readonly DependencyProperty NeedleBitmapEffectProperty = DependencyProperty.Register(nameof(NeedleBitmapEffect), typeof(BitmapEffect), typeof(CircleMeter));


		static CircleMeter() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMeter), new FrameworkPropertyMetadata(typeof(CircleMeter)));
		
	}
}
