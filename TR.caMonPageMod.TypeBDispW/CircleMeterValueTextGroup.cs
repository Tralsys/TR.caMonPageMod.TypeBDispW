using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

		static CircleMeterValueTextGroup() => DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleMeterValueTextGroup), new FrameworkPropertyMetadata(typeof(CircleMeterValueTextGroup)));

		static void TextCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as CircleMeterValueTextGroup)?.TextCountChanger();

		List<CircleMeterValueText> ValueTexts { get; } = new();//あとでTemplateのGridのChildrenに関連付ける
		void TextCountChanger()
		{
			if (Step <= 0)
			{
				ValueTexts.Clear();
				return;
			}

			double MarksToPutCount = (double)(EndValue - StartValue) / Step;
			if ((MarksToPutCount % 1) != 0)//小数点以下に数値が存在=>割り切れなかった
				MarksToPutCount += 1;//EndValueにも数値Textを配置する
			else if (MarksToPutCount <= 0)
			{
				ValueTexts.Clear();
				return;
			}

			if (MarksToPutCount < ValueTexts.Count)
			{
				//要らないTextを削除する
			}
			else if (MarksToPutCount > ValueTexts.Count)
			{
				//不足しているTextsを追加する
				//Bindingも設定
			}

			for(int i = 0; i < MarksToPutCount; i++)//EndValueはloopの外で設定
				ValueTexts[i].TextValue = StartValue + (Step * i);
			ValueTexts.Last().TextValue = EndValue;//EndValueを設定
		}
	}
}
