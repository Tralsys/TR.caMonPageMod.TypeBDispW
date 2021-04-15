using System;
using System.Globalization;
using System.Windows.Data;

namespace TR.caMonPageMod.TypeBDispW
{
	public class ValueMultiplConverter : IValueConverter
	{
		public double MultiplValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value * MultiplValue;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (double)value / MultiplValue;
	}
}
