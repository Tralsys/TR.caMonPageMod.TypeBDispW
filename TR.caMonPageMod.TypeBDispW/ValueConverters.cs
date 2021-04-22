using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TR.caMonPageMod.TypeBDispW
{
	[ValueConversion(typeof(double), typeof(double))]
	public class ValueMultiplConverter : IValueConverter
	{
		public double MultiplValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value * MultiplValue;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (double)value / MultiplValue;
	}

	[ValueConversion(typeof(Thickness), typeof(Thickness))]
	public class TakeSelectedSideThicknessConverter : IValueConverter
	{
		[Flags]
		public enum Side
		{
			None = 0,
			Left = 0b000001,
			Top = 0b00000010,
			Right = 0b00100,
			Bottom = 0b1000
		}

		public Side SideSelect { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> value is Thickness t ?
				new Thickness(
					SideSelect.HasFlag(Side.Left) ? t.Left : 0,
					SideSelect.HasFlag(Side.Top) ? t.Top : 0,
					SideSelect.HasFlag(Side.Right) ? t.Right : 0,
					SideSelect.HasFlag(Side.Bottom) ? t.Bottom : 0
					)
			: new Thickness(0);


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

	[ValueConversion(typeof(double), typeof(Thickness))]
	public class DoubleToThicknessConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new Thickness((double)value);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((Thickness)value).Left;
	}

	[ValueConversion(typeof(double), typeof(int))]
	public class DoubleToIntConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Math.Floor((double)value);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (double)value;
	}

	//ref : https://oita.oika.me/2018/04/15/pilevalueconverter/
	[ContentProperty(nameof(Converters))]
	public class ValueConverterGroup : IValueConverter
	{
		public List<IValueConverter> Converters { get; } = new();

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Converters.Count <= 0)
				return value;
			foreach (var cnv in Converters)
				value = cnv.Convert(value, targetType, parameter, culture);
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Converters.Count <= 0)
				return value;
			foreach (var cnv in Converters)
				value = cnv.ConvertBack(value, targetType, parameter, culture);
			return value;
		}
	}

	[ValueConversion(typeof(object), typeof(double))]
	public class ObjectToStringConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> value.ToString();

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}

	[ValueConversion(typeof(double), typeof(string))]
	public class DoubleToStringConverter : ObjectToStringConverter
	{
		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> double.TryParse(value as string, out double val) ? val : base.ConvertBack(value, targetType, parameter, culture);
	}

	[ValueConversion(typeof(double), typeof(double))]
	public class DoubleSignInvertConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
			=> -(double)value;
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> -(double)value;
	}
}
