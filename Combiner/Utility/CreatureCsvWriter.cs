using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Combiner
{
	public class CreatureCsvWriter
	{
		BodyPartsConverter bodyPartsConverter = new BodyPartsConverter();
		BoolToStringConverter boolToStringConverter = new BoolToStringConverter();
		ContainsAbilitiesConverter containsAbilitiesConverter = new ContainsAbilitiesConverter();
		DoubleToStringConverter doubleToStringConverter = new DoubleToStringConverter();
		RangeSpecialConverter rangeSpecialConverter = new RangeSpecialConverter();
		RangeTypeConverter rangeTypeConverter = new RangeTypeConverter();
		StockNameConverter stockNameConverter = new StockNameConverter();

		public void WriteFile(IEnumerable<Creature> creatures)
		{
			using (StreamWriter writer = new StreamWriter(File.Create("./Creatures.csv")))
			{
				writer.WriteLine(HeaderRow());

				foreach (Creature creature in creatures)
				{
					writer.WriteLine(BuildRow(creature));
				}
			}
		}

		private string HeaderRow()
		{
			StringBuilder builder = new StringBuilder();

			AppendValue(builder, "Left", true);
			AppendValue(builder, "Right", false);
			AppendValue(builder, "Body Parts", false);
			AppendValue(builder, "Rank", false);
			AppendValue(builder, "Coal", false);
			AppendValue(builder, "Elec", false);
			AppendValue(builder, "Power", false);
			AppendValue(builder, "E.HP", false);
			AppendValue(builder, "Hitpoints", false);
			AppendValue(builder, "Armour", false);
			AppendValue(builder, "Sight Radius", false);
			AppendValue(builder, "Size", false);
			AppendValue(builder, "Land Speed", false);
			AppendValue(builder, "Water Speed", false);
			AppendValue(builder, "Air Speed", false);
			AppendValue(builder, "Melee Damage", false);
			AppendValue(builder, "Range Damage 1", false);
			AppendValue(builder, "Artillery Type 1", false);
			AppendValue(builder, "Range Type 1", false);
			AppendValue(builder, "Range Damage 2", false);
			AppendValue(builder, "Artillery Type 2", false);
			AppendValue(builder, "Range Type 2", false);
			AppendValue(builder, "Horns", false);
			AppendValue(builder, "Barrier Destroy", false);
			AppendValue(builder, "Poison", false);
			AppendValue(builder, "Abilties", false);

			return builder.ToString();
		}

		private string BuildRow(Creature creature)
		{
			StringBuilder builder = new StringBuilder();
			AppendValue(builder, stockNameConverter.Convert(creature.Left), true);
			AppendValue(builder, stockNameConverter.Convert(creature.Right), false);
			AppendValue(builder, bodyPartsConverter.Convert(creature.BodyParts), false);
			AppendValue(builder, creature.Rank.ToString(), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Coal), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Electricity), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Power), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.EffectiveHitpoints), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Hitpoints), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Armour), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.SightRadius), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.Size), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.LandSpeed), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.WaterSpeed), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.AirSpeed), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.MeleeDamage), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.RangeDamage1), false);
			AppendValue(builder, rangeSpecialConverter.Convert(creature.RangeSpecial1), false);
			AppendValue(builder, rangeTypeConverter.Convert(creature.RangeType1), false);
			AppendValue(builder, doubleToStringConverter.Convert(creature.RangeDamage2), false);
			AppendValue(builder, rangeSpecialConverter.Convert(creature.RangeSpecial2), false);
			AppendValue(builder, rangeTypeConverter.Convert(creature.RangeType2), false);
			AppendValue(builder, boolToStringConverter.Convert(creature.HasHorns), false);
			AppendValue(builder, boolToStringConverter.Convert(creature.HasBarrierDestroy), false);
			AppendValue(builder, boolToStringConverter.Convert(creature.HasPoison), false);
			AppendValue(builder, containsAbilitiesConverter.Convert(creature.Abilities), false);
			return builder.ToString();
		}

		private void AppendValue(StringBuilder builder, object obj, bool firstColumn)
		{
			AppendValue(builder, obj.ToString(), firstColumn);
		}

		private void AppendValue(StringBuilder builder, string value, bool firstColumn)
		{
			if (!firstColumn)
			{
				builder.Append(',');
			}

			if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
			{
				builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
			}
			else
			{
				builder.Append(value);
			}
		}
	}
}
