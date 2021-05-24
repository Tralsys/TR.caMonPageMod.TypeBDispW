using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace TR.caMonPageMod.TypeBDispW
{
	public interface IReadOnlyCircleMeterSettings
	{
		int StartValue { get; }
		int EndValue { get; }
		double StartAngle { get; }
		double EndAngle { get; }
		double MarkLStep { get; }
		double MarkMStep { get; }
		double MarkSStep { get; }
		Visibility MarkLVisibility { get; }
		Visibility MarkMVisibility { get; }
		Visibility MarkSVisibility { get; }
	}
	public interface IWriteOnlyCircleMeterSettings
	{
		int StartValue { set; }
		int EndValue { set; }
		double StartAngle { set; }
		double EndAngle { set; }
		double MarkLStep { set; }
		double MarkMStep { set; }
		double MarkSStep { set; }
		Visibility MarkLVisibility { set; }
		Visibility MarkMVisibility { set; }
		Visibility MarkSVisibility { set; }
	}
	public class CircleMeterSettings : INotifyPropertyChanged, IReadOnlyCircleMeterSettings, IWriteOnlyCircleMeterSettings
	{
		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged(in string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public int StartValue { get => _StartValue; set { _StartValue = value; OnPropertyChanged(nameof(StartValue)); } }
		private int _StartValue = 0;

		public int EndValue { get => _EndValue; set { _EndValue = value; OnPropertyChanged(nameof(EndValue)); } }
		private int _EndValue = 0;

		public double StartAngle { get => _StartAngle; set { _StartAngle = value; OnPropertyChanged(nameof(StartAngle)); } }
		private double _StartAngle = 0;

		public double EndAngle { get => _EndAngle; set { _EndAngle = value; OnPropertyChanged(nameof(EndAngle)); } }
		private double _EndAngle = 0;

		public double MarkLStep { get => _MarkLStep; set { _MarkLStep = value; OnPropertyChanged(nameof(MarkLStep)); } }
		private double _MarkLStep = 0;

		public double MarkMStep { get => _MarkMStep; set { _MarkMStep = value; OnPropertyChanged(nameof(MarkMStep)); } }
		private double _MarkMStep = 0;

		public double MarkSStep { get => _MarkSStep; set { _MarkSStep = value; OnPropertyChanged(nameof(MarkSStep)); } }
		private double _MarkSStep = 0;

		public Visibility MarkLVisibility { get => _MarkLVisibility; set { _MarkLVisibility = value; OnPropertyChanged(nameof(MarkLVisibility)); } }
		private Visibility _MarkLVisibility = Visibility.Visible;

		public Visibility MarkMVisibility { get => _MarkMVisibility; set { _MarkMVisibility = value; OnPropertyChanged(nameof(MarkMVisibility)); } }
		private Visibility _MarkMVisibility = Visibility.Visible;

		public Visibility MarkSVisibility { get => _MarkSVisibility; set { _MarkSVisibility = value; OnPropertyChanged(nameof(MarkSVisibility)); } }
		private Visibility _MarkSVisibility = Visibility.Visible;


	}
	public class CircleMeter : Control
	{
		public CircleMeterSettings MeterSettings { get => (CircleMeterSettings)GetValue(MeterSettingsProperty); set => SetValue(MeterSettingsProperty, value); }
		static public readonly DependencyProperty MeterSettingsProperty = DependencyProperty.Register(nameof(MeterSettings), typeof(CircleMeterSettings), typeof(CircleMeter));

		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleMeter));

		public Style MarkLStyle { get => (Style)GetValue(MarkLStyleProperty); set => SetValue(MarkLStyleProperty, value); }
		static public readonly DependencyProperty MarkLStyleProperty = DependencyProperty.Register(nameof(MarkLStyle), typeof(Style), typeof(CircleMeter));
		public Style MarkMStyle { get => (Style)GetValue(MarkMStyleProperty); set => SetValue(MarkMStyleProperty, value); }
		static public readonly DependencyProperty MarkMStyleProperty = DependencyProperty.Register(nameof(MarkMStyle), typeof(Style), typeof(CircleMeter));
		public Style MarkSStyle { get => (Style)GetValue(MarkSStyleProperty); set => SetValue(MarkSStyleProperty, value); }
		static public readonly DependencyProperty MarkSStyleProperty = DependencyProperty.Register(nameof(MarkSStyle), typeof(Style), typeof(CircleMeter));
		public Style NeedleStyle { get => (Style)GetValue(NeedleStyleProperty); set => SetValue(NeedleStyleProperty, value); }
		static public readonly DependencyProperty NeedleStyleProperty = DependencyProperty.Register(nameof(NeedleStyle), typeof(Style), typeof(CircleMeter));
		public Style Needle2Style { get => (Style)GetValue(Needle2StyleProperty); set => SetValue(Needle2StyleProperty, value); }
		static public readonly DependencyProperty Needle2StyleProperty = DependencyProperty.Register(nameof(Needle2Style), typeof(Style), typeof(CircleMeter));
		public Style CenterPinStyle { get => (Style)GetValue(CenterPinStyleProperty); set => SetValue(CenterPinStyleProperty, value); }
		static public readonly DependencyProperty CenterPinStyleProperty = DependencyProperty.Register(nameof(CenterPinStyle), typeof(Style), typeof(CircleMeter));

		public double CurrentValue { get => (double)GetValue(CurrentValueProperty); set => SetValue(CurrentValueProperty, value); }
		static public readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register(nameof(CurrentValue), typeof(double), typeof(CircleMeter));
		public double CurrentValue2 { get => (double)GetValue(CurrentValue2Property); set => SetValue(CurrentValue2Property, value); }
		static public readonly DependencyProperty CurrentValue2Property = DependencyProperty.Register(nameof(CurrentValue2), typeof(double), typeof(CircleMeter));

		public BitmapEffect NeedleBitmapEffect { get => (BitmapEffect)GetValue(NeedleBitmapEffectProperty); set => SetValue(NeedleBitmapEffectProperty, value); }
		static public readonly DependencyProperty NeedleBitmapEffectProperty = DependencyProperty.Register(nameof(NeedleBitmapEffect), typeof(BitmapEffect), typeof(CircleMeter));
		public BitmapEffect Needle2BitmapEffect { get => (BitmapEffect)GetValue(Needle2BitmapEffectProperty); set => SetValue(Needle2BitmapEffectProperty, value); }
		static public readonly DependencyProperty Needle2BitmapEffectProperty = DependencyProperty.Register(nameof(Needle2BitmapEffect), typeof(BitmapEffect), typeof(CircleMeter));


		static CircleMeter() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMeter), new FrameworkPropertyMetadata(typeof(CircleMeter)));
		
	}
}
