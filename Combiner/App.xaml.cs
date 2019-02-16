using System.Windows;

namespace Combiner
{
	using Ninject;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var dependencyResolver = DependencyResolver.Instance;
			var mainWindow = dependencyResolver.Get<MainWindow>();
			mainWindow.Show();
		}
	}
}
