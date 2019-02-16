using System.Windows;

namespace Combiner
{
	/// <summary>
	/// Interaction logic for NewMainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(MainVM mainVM)
		{
			InitializeComponent();
			DataContext = mainVM;
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
