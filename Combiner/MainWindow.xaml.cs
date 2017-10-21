using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Combiner
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//SQLiteConnection.CreateFile("MyDatabase.sqlite");
			//_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
			//_dbConnection.Open();



			LuaHandler lua = new LuaHandler();

			Stock s1 = StockFactory.Instance.CreateStock("coyote", lua);
			Stock s2 = StockFactory.Instance.CreateStock("humpback", lua);
			CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript();

		}
	}
}
