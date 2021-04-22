using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TR.caMonPageMod.TypeBDispW
{
	/// <summary>
	/// Interaction logic for PageTR01.xaml
	/// </summary>
	public partial class PageTR01 : UserControl
	{
		PageTR01DataClass MyData { get; } = new();
		static CircleMeterSettings SPDMeter160 { get; } = new CircleMeterSettings()
		{
			Radius = 300,
			StartAngle = -30,
			EndAngle = 210,
			StartValue = 0,
			EndValue = 160,
			MarkLStep = 20,
			MarkMStep = 10,
			MarkSStep = 2,
			MarkLVisibility = Visibility.Visible,
			MarkMVisibility = Visibility.Visible,
			MarkSVisibility = Visibility.Visible,
		};

		public PageTR01()
		{
			DataContext = MyData;

			InitializeComponent();
			MyData.SpeedMeterSetting = SPDMeter160;
			Background = Brushes.AntiqueWhite;
		}
	}

	public class PageTR01DataClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(in string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		private CircleMeterSettings _SpeedMeterSetting = null;
		public CircleMeterSettings SpeedMeterSetting { get => _SpeedMeterSetting; set { _SpeedMeterSetting = value; OnPropertyChanged(nameof(SpeedMeterSetting)); } }
	}
}
