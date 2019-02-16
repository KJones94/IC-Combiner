using System.Collections.Generic;
using System.ComponentModel;

namespace Combiner
{
	public class SelectedCreatureVM : BaseViewModel
	{

		public SelectedCreatureVM(CreatureDataVM creatureDataVM)
		{
			creatureDataVM.PropertyChanged += OnSelectedCreatureChanged;
		}

		private void OnSelectedCreatureChanged(object o, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "SelectedCreature")
			{
				Creature selectedCreature = (o as CreatureDataVM).SelectedCreature;
				if (selectedCreature != null)
				{
					Left = selectedCreature.Left;
					Right = selectedCreature.Right;
					BodyParts = ConvertBodyParts(selectedCreature.BodyParts);
				} 
			}
		}

		private Dictionary<Limb, Side> ConvertBodyParts(Dictionary<string, string> oldBodyParts)
		{
			Dictionary<Limb, Side> newBodyParts = new Dictionary<Limb, Side>();

			newBodyParts.Add(Limb.Head, StringToSide(oldBodyParts[Limb.Head.ToString()]));
			newBodyParts.Add(Limb.FrontLegs, StringToSide(oldBodyParts[Limb.FrontLegs.ToString()]));
			newBodyParts.Add(Limb.Torso, StringToSide(oldBodyParts[Limb.Torso.ToString()]));
			newBodyParts.Add(Limb.BackLegs, StringToSide(oldBodyParts[Limb.BackLegs.ToString()]));
			newBodyParts.Add(Limb.Tail, StringToSide(oldBodyParts[Limb.Tail.ToString()]));
			newBodyParts.Add(Limb.Wings, StringToSide(oldBodyParts[Limb.Wings.ToString()]));
			newBodyParts.Add(Limb.Claws, StringToSide(oldBodyParts[Limb.Claws.ToString()]));

			return newBodyParts;
		}

		private Side StringToSide(string s)
		{
			if (s == "L")
			{
				return Side.Left;
			}
			if (s == "R")
			{
				return Side.Right;
			}
			return Side.Empty;
		}

		private Creature m_SelectedCreature;
		public Creature SelectedCreature
		{
			get
			{
				return m_SelectedCreature;
			}
			set
			{
				if (m_SelectedCreature != value)
				{
					m_SelectedCreature = value;
					OnPropertyChanged(nameof(SelectedCreature));
				}
			}
		}

		private string m_Left = "Left";
		public string Left
		{
			get { return m_Left; }
			set
			{
				if (m_Left != value)
				{
					m_Left = value;
					OnPropertyChanged(nameof(Left));
				}
			}
		}

		private string m_Right = "Right";
		public string Right
		{
			get { return m_Right; }
			set
			{
				if (m_Right != value)
				{
					m_Right = value;
					OnPropertyChanged(nameof(Right));
				}
			}
		}

		private Dictionary<Limb, Side> m_DefaultBodyParts = new Dictionary<Limb, Side>()
		{
			{ Limb.Head, Side.Empty },
			{ Limb.FrontLegs, Side.Empty },
			{ Limb.Torso, Side.Empty },
			{ Limb.BackLegs, Side.Empty },
			{ Limb.Tail, Side.Empty },
			{ Limb.Wings, Side.Empty },
			{ Limb.Claws, Side.Empty }
		};

		private Dictionary<Limb, Side> m_BodyParts;
		public Dictionary<Limb, Side> BodyParts
		{
			get
			{
				return m_BodyParts
					?? (m_BodyParts = m_DefaultBodyParts);
			}
			set
			{
				if (m_BodyParts != value)
				{
					m_BodyParts = value;
					OnPropertyChanged(nameof(BodyParts));
				}
			}
		}
	}
}
