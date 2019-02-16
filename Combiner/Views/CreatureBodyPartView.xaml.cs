using System.Windows;
using System.Windows.Controls;

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
