using System;
using System.Windows;
using System.Windows.Controls;

namespace TR.caMonPageMod.TypeBDispW
{
	public class HorizontalBarChart : Control
	{
		/// <summary>開始値(左端)</summary>
		public double StartValue { get => (double)GetValue(StartValueProperty); set => SetValue(StartValueProperty, value); }
		/// <summary>開始値の依存関係プロパティ</summary>
		static public readonly DependencyProperty StartValueProperty = DependencyProperty.Register(nameof(StartValue), typeof(double), typeof(HorizontalBarChart), new(ScalePropertyChanged));

		/// <summary>終了値(右端)</summary>
		public double EndValue { get => (double)GetValue(EndValueProperty); set => SetValue(EndValueProperty, value); }
		/// <summary>終了値の依存関係プロパティ</summary>
		static public readonly DependencyProperty EndValueProperty = DependencyProperty.Register(nameof(EndValue), typeof(double), typeof(HorizontalBarChart), new(ScalePropertyChanged));

		/// <summary>現在値</summary>
		public double CurrentValue { get => (double)GetValue(CurrentValueProperty); set => SetValue(CurrentValueProperty, value); }
		/// <summary>現在値の依存関係プロパティ</summary>
		static public readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register(nameof(CurrentValue), typeof(double), typeof(HorizontalBarChart), new(ScalePropertyChanged));

		/// <summary>四角形の最大幅</summary>
		public double RectangleMaxWidth { get => (double)GetValue(RectangleMaxWidthProperty); set => SetValue(RectangleMaxWidthProperty, value); }
		/// <summary>四角形の最大幅の依存関係プロパティ</summary>
		static public readonly DependencyProperty RectangleMaxWidthProperty = DependencyProperty.Register(nameof(RectangleMaxWidth), typeof(double), typeof(HorizontalBarChart), new(ScalePropertyChanged));

		/// <summary>現在の四角形の幅</summary>
		public double CurrentRectangleWidth { get => (double)GetValue(CurrentRectangleWidthProperty); private set => SetValue(CurrentRectangleWidthPropertyKey, value); }
		/// <summary>現在の四角形の幅の依存関係プロパティ</summary>
		static private readonly DependencyPropertyKey CurrentRectangleWidthPropertyKey = DependencyProperty.RegisterReadOnly(nameof(CurrentRectangleWidth), typeof(double), typeof(HorizontalBarChart), new());
		/// <summary>現在の四角形の幅の依存関係プロパティ</summary>
		static public readonly DependencyProperty CurrentRectangleWidthProperty = CurrentRectangleWidthPropertyKey.DependencyProperty;

		static HorizontalBarChart() => DefaultStyleKeyProperty.OverrideMetadata(typeof(HorizontalBarChart), new FrameworkPropertyMetadata(typeof(HorizontalBarChart)));

		static void ScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => (d as HorizontalBarChart)?.ChangeBarWidth();
		void ChangeBarWidth() => CurrentRectangleWidth = Math.Min(Math.Max((CurrentValue - StartValue) / (EndValue - StartValue), 0), 1) * RectangleMaxWidth;
	}
}
