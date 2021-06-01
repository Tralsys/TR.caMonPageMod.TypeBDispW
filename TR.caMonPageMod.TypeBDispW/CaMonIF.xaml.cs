using System;
using System.Windows.Controls;

using caMon;

namespace TR.caMonPageMod.TypeBDispW
{
	/// <summary>
	/// Interaction logic for CaMonIF.xaml
	/// </summary>
	public partial class CaMonIF : Page, IPages
	{
		public CaMonIF() => InitializeComponent();

		public Page FrontPage { get => this; }

		public event EventHandler BackToHome;
		public event EventHandler CloseApp;

		public void Dispose() { }

		private void HomeClicked(object sender, System.Windows.RoutedEventArgs e) => BackToHome?.Invoke(this, null);

		private void CloseClicked(object sender, System.Windows.RoutedEventArgs e) => CloseApp?.Invoke(this, null);
	}
}
