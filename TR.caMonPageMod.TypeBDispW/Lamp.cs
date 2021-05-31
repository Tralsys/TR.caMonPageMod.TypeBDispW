using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TypeBDispW
{
	public class Lamp : ContentControl
	{
		public Brush LampBrush { get => (Brush)GetValue(LampBrushProperty); set => SetValue(LampBrushProperty, value); }
		static public readonly DependencyProperty LampBrushProperty = DependencyProperty.Register(nameof(LampBrush), typeof(Brush), typeof(Lamp));
		public Brush LampBaseBrush { get => (Brush)GetValue(LampBaseBrushProperty); set => SetValue(LampBaseBrushProperty, value); }
		static public readonly DependencyProperty LampBaseBrushProperty = DependencyProperty.Register(nameof(LampBaseBrush), typeof(Brush), typeof(Lamp));
		public Brush LampedTextBrush { get => (Brush)GetValue(LampedTextBrushProperty); set => SetValue(LampedTextBrushProperty, value); }
		static public readonly DependencyProperty LampedTextBrushProperty = DependencyProperty.Register(nameof(LampedTextBrush), typeof(Brush), typeof(Lamp));
		public Brush LampedTextBaseBrush { get => (Brush)GetValue(LampedTextBaseBrushProperty); set => SetValue(LampedTextBaseBrushProperty, value); }
		static public readonly DependencyProperty LampedTextBaseBrushProperty = DependencyProperty.Register(nameof(LampedTextBaseBrush), typeof(Brush), typeof(Lamp));
		public bool IsLampON { get => (bool)GetValue(IsLampONProperty); set => SetValue(IsLampONProperty, value); }
		static public readonly DependencyProperty IsLampONProperty = DependencyProperty.Register(nameof(IsLampON), typeof(bool), typeof(Lamp));
		public Style LampBaseStyle { get => (Style)GetValue(LampBaseStyleProperty); set => SetValue(LampBaseStyleProperty, value); }
		static public readonly DependencyProperty LampBaseStyleProperty = DependencyProperty.Register(nameof(LampBaseStyle), typeof(Style), typeof(Lamp));

		static Lamp() => DefaultStyleKeyProperty.OverrideMetadata(typeof(Lamp), new FrameworkPropertyMetadata(typeof(Lamp)));
		
	}
}
