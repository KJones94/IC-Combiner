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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Combiner
{
	/// <summary>
	/// Interaction logic for CreatureBodyPartView.xaml
	/// </summary>
	public partial class CreatureBodyPartView : UserControl
	{
		public CreatureBodyPartView()
		{
			InitializeComponent();
		}

		public Side ChosenSide
		{
			get { return (Side)GetValue(ChosenSideProperty); }
			set { SetValue(ChosenSideProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Side.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ChosenSideProperty =
			DependencyProperty.Register("ChosenSide", typeof(Side), typeof(CreatureBodyPartView), new PropertyMetadata(Side.Empty));



		public string LimbText
		{
			get { return (string)GetValue(LimbTextProperty); }
			set { SetValue(LimbTextProperty, value); }
		}

		// Using a DependencyProperty as the backing store for LimbText.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty LimbTextProperty =
			DependencyProperty.Register("LimbText", typeof(string), typeof(CreatureBodyPartView), new PropertyMetadata(string.Empty));


	}
}
