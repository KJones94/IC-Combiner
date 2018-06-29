using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Combiner
{
	/// <summary>
	/// Interaction logic for NewMainWindow.xaml
	/// </summary>
	public partial class NewMainWindow : Window
	{
		public NewMainWindow()
		{
			InitializeComponent();
		}

		private void MenuItem_GuideClick(object sender, RoutedEventArgs e)
		{
			GuideWindow window = new GuideWindow();
			window.Show();
		}

		private void MenuItem_ReportClick(object sender, RoutedEventArgs e)
		{
			ReportWindow window = new ReportWindow();
			window.Show();
		}

		private void MenuItem_AboutClick(object sender, RoutedEventArgs e)
		{
			AboutWindow window = new AboutWindow();
			window.Show();
		}
	}
}
