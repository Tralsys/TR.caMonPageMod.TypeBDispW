using BIDSData_toBind;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using TR.BIDSSMemLib;

namespace TR.caMonPageMod.TypeBDispW
{
	/// <summary>
	/// Interaction logic for PageTR01.xaml
	/// </summary>
	public partial class PageTR01 : UserControl
	{
		PageTR01DataClass MyData { get; } = new();

		static Dictionary<int, CircleMeterSettings> SPDMeterSettings { get; } = new()
		{
			{
				90,
				new()
				{
					StartAngle = -180.0 / 7.0,
					EndAngle = 180 + (180.0 / 7.0),
					StartValue = 0,
					EndValue = 90,
					TextStep = 20,
					MarkLStep = 20,
					MarkMStep = 10,
					MarkSStep = 2,
					MarkLVisibility = Visibility.Visible,
					MarkMVisibility = Visibility.Visible,
					MarkSVisibility = Visibility.Visible,
				}
			},
			{
				120,
				new()
				{
					StartAngle = -30,
					EndAngle = 210,
					StartValue = 0,
					EndValue = 120,
					TextStep = 20,
					MarkLStep = 20,
					MarkMStep = 10,
					MarkSStep = 2,
					MarkLVisibility = Visibility.Visible,
					MarkMVisibility = Visibility.Visible,
					MarkSVisibility = Visibility.Visible,
				}
			},
			{
				140,
				new()
				{
					StartAngle = -36,
					EndAngle = 216,
					StartValue = 0,
					EndValue = 140,
					TextStep = 20,
					MarkLStep = 10,
					MarkMStep = 5,
					MarkSStep = 1,
					MarkLVisibility = Visibility.Visible,
					MarkMVisibility = Visibility.Visible,
					MarkSVisibility = Visibility.Visible,
				}
			},
			{
				160,
				new()
				{
					StartAngle = -30,
					EndAngle = 210,
					StartValue = 0,
					EndValue = 160,
					TextStep = 20,
					MarkLStep = 20,
					MarkMStep = 10,
					MarkSStep = 2,
					MarkLVisibility = Visibility.Visible,
					MarkMVisibility = Visibility.Visible,
					MarkSVisibility = Visibility.Visible,
				}
			}
		};

		public PageTR01()
		{
			DataContext = MyData;

			InitializeComponent();
			MyData.SpeedMeterSetting = SPDMeterSettings.GetValueOrDefault(160);
			Background = Brushes.AntiqueWhite;

			Loaded += (s,e)=> SMemLib.SMC_BSMDChanged += SMemLib_SMC_BSMDChanged;
			Unloaded+=(s,e)=> SMemLib.SMC_BSMDChanged -= SMemLib_SMC_BSMDChanged;
		}

		private void SMemLib_SMC_BSMDChanged(object sender, ValueChangedEventArgs<BIDSSharedMemoryData> e) => MyData.BSMD.BSMD = e.NewValue;
	}

	public class PageTR01DataClass : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(in string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

		private CircleMeterSettings _SpeedMeterSetting = null;
		public CircleMeterSettings SpeedMeterSetting { get => _SpeedMeterSetting; set { _SpeedMeterSetting = value; OnPropertyChanged(nameof(SpeedMeterSetting)); } }

		public BSMD_toBind BSMD { get; } = new();
	}
}
