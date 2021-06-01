using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace TR.caMonPageMod.TypeBDispW
{
	public class CircleMeterValueTextGroup : Control
	{
		public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }
		static public readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(CircleMeterValueTextGroup));

		public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }
		static public readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(CircleMeterValueTextGroup));

		public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }
		static public readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(double), typeof(CircleMeterValueTextGroup));

		public int StartValue { get => (int)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(int), typeof(CircleMeterValueTextGroup), new(TextCountChanged));

		public int EndValue { get => (int)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(int), typeof(CircleMeterValueTextGroup), new(TextCountChanged));
		public int Step { get => (int)GetValue(StepProperty); set => SetValue(StepProperty, value); }
		static public readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof(Step), typeof(int), typeof(CircleMeterValueTextGroup), new(TextCountChanged));

		public Style TextStyle { get => (Style)GetValue(TextStyleProperty); set => SetValue(TextStyleProperty, value); }
		static public readonly DependencyProperty TextStyleProperty = DependencyProperty.Register(nameof(TextStyle), typeof(Style), typeof(CircleMeterValueTextGroup));

		Grid BaseGrid { get; set; }

		static CircleMeterValueTextGroup() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMeterValueTextGroup), new FrameworkPropertyMetadata(typeof(CircleMeterValueTextGroup)));
		public CircleMeterValueTextGroup()
		{
			Loaded += (_, _) =>
			{
				BaseGrid = Template.FindName(nameof(BaseGrid), this) as Grid;
				TextCountChanger();
			};
		}
		static void TextCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as CircleMeterValueTextGroup)?.TextCountChanger();

		void TextCountChanger()
		{
			if (BaseGrid is null)
				return;

			if (Step <= 0)
			{
				BaseGrid.Children.Clear();
				return;
			}

			/* eg.
			 * start:0, end:10, step:2
			 *   MarksToPutCount=5
			 *   PutMark=>0, 2, 4, 6, 8, 10 (6 pieces)
			 * 
			 * start:0, end:11, step:3
			 *   MarksToPutCount=3.6666
			 *   PutMark=>0, 3, 6, 9, 11 (5 pieces)
			 */
			double MarksToPutCount = (double)(EndValue - StartValue) / Step;
			if (MarksToPutCount <= 0)
			{
				BaseGrid.Children.Clear();
				return;
			}

			int MarksToPutCountInt = (int)Math.Ceiling(MarksToPutCount) + 1;
			if (BaseGrid.Children.Count != MarksToPutCount)
			{
				Binding GetBinding(string name) => new(name) { Source = this };
				/* eg.
				 * 5 > 2 =>> Remove 2,3,4
					 * 2 < 5 =>> Add 3 instance
				*/
				int Diff = Math.Abs(BaseGrid.Children.Count - MarksToPutCountInt);
				if (BaseGrid.Children.Count > MarksToPutCountInt)
					BaseGrid.Children.RemoveRange(MarksToPutCountInt, Diff);
				else
					for (int i = 0; i < Diff; i++)
					{
						CircleMeterValueText elem = new();
						elem.SetBinding(CircleMeterValueText.StartAngleProperty, GetBinding(nameof(StartAngle)));
						elem.SetBinding(CircleMeterValueText.EndAngleProperty, GetBinding(nameof(EndAngle)));
						elem.SetBinding(CircleMeterValueText.RadiusProperty, GetBinding(nameof(Radius)));
						elem.SetBinding(CircleMeterValueText.StartValueProperty, GetBinding(nameof(StartValue)));
						elem.SetBinding(CircleMeterValueText.EndValueProperty, GetBinding(nameof(EndValue)));
						elem.SetBinding(CircleMeterValueText.TextStyleProperty, GetBinding(nameof(TextStyle)));
						elem.FontSize = 42;
						elem.VerticalAlignment = VerticalAlignment.Center;
						elem.HorizontalAlignment = HorizontalAlignment.Left;
						BaseGrid.Children.Add(elem);
					}
			}

			for (int i = 0; i < MarksToPutCount; i++)//EndValueはloopの外で設定
				(BaseGrid.Children[i] as CircleMeterValueText).TextValue = StartValue + (Step * i);
			(BaseGrid.Children[BaseGrid.Children.Count - 1] as CircleMeterValueText).TextValue = EndValue;//EndValueを設定
		}
	}
}
