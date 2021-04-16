using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TypeBDispW
{
	/// <summary>
	/// Interaction logic for PageTR01.xaml
	/// </summary>
	public partial class PageTR01 : UserControl
	{
		PageTR01DataClass MyData { get; } = new();
		public PageTR01()
		{
			DataContext = MyData;

			InitializeComponent();
			SetSPDMeter160();
		}

		private void SetSPDMeter160()
		{
			var s = MyData.SpeedMeterSetting;//参照記述の省略用
			s.Radius = 300;
			s.StartAngle = -30;
			s.EndAngle = 210;
			s.StartValue = 0;
			s.EndValue = 160;
			s.MarkLStep = 20;
			s.MarkMStep = 10;
			s.MarkSStep = 2;
			s.MarkLVisibility = Visibility.Visible;
			s.MarkMVisibility = Visibility.Visible;
			s.MarkSVisibility = Visibility.Visible;
			MyData.SpeedMeterSetting = s;
		}
	}

	public class PageTR01DataClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(in string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		private MeterSetting _SpeedMeterSetting = new();
		public MeterSetting SpeedMeterSetting { get => _SpeedMeterSetting; set { _SpeedMeterSetting = value; OnPropertyChanged(nameof(SpeedMeterSetting)); } }
	}

	public class MeterSetting : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(in string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		public double Radius { get => _Radius; set { _Radius = value; OnPropertyChanged(nameof(Radius)); } }
		private double _Radius = 0;

		public double StartAngle { get => _StartAngle; set { _StartAngle = value; OnPropertyChanged(nameof(StartAngle)); } }
		private double _StartAngle = 0;

		public double EndAngle { get => _EndAngle; set { _EndAngle = value; OnPropertyChanged(nameof(EndAngle)); } }
		private double _EndAngle = 0;

		public int StartValue { get => _StartValue; set { _StartValue = value; OnPropertyChanged(nameof(StartValue)); } }
		private int _StartValue = 0;

		public int EndValue { get => _EndValue; set { _EndValue = value; OnPropertyChanged(nameof(EndValue)); } }
		private int _EndValue = 0;

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
}
