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
			Stock s2 = StockFactory.Instance.CreateStock("camel", lua);
			List<Creature> creatures = CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript(creatures[0]);

			//s1 = StockFactory.Instance.CreateStock("chameleon", lua);
			//s2 = StockFactory.Instance.CreateStock("blue whale", lua);
			//creatures = CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript(creatures[0]);
			//lua.LoadScript(creatures[5]);
			//lua.LoadScript(creatures[0]);


			//s1 = StockFactory.Instance.CreateStock("archerfish", lua);
			//s2 = StockFactory.Instance.CreateStock("horse", lua);
			//creatures = CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript(creatures[0]);
			//lua.LoadScript(creatures[5]);
			//lua.LoadScript(creatures[0]);

			//s1 = StockFactory.Instance.CreateStock("cheetah", lua);
			//s2 = StockFactory.Instance.CreateStock("behemoth", lua);
			//creatures = CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript(creatures[0]);
			//lua.LoadScript(creatures[5]);
			//lua.LoadScript(creatures[0]);

			//s1 = StockFactory.Instance.CreateStock("fire_fly", lua);
			//s2 = StockFactory.Instance.CreateStock("dolphin", lua);
			//creatures = CreatureCombiner.Combine(s1, s2);
			//lua.LoadScript(creatures[0]);
			//lua.LoadScript(creatures[5]);
			//lua.LoadScript(creatures[0]);

			s1 = StockFactory.Instance.CreateStock("dolphin", lua);
			s2 = StockFactory.Instance.CreateStock("behemoth", lua);
			creatures = CreatureCombiner.Combine(s1, s2);
			lua.LoadScript(creatures[0]);
			lua.LoadScript(creatures[5]);
			lua.LoadScript(creatures[0]);


		}
	}
}
