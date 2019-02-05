namespace Combiner.Utility
{
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	using Combiner.Converters;
	using Combiner.Models;

	public class CreatureCsvWriter
	{
		BodyPartsConverter bodyPartsConverter = new BodyPartsConverter();
		BoolToStringConverter boolToStringConverter = new BoolToStringConverter();
		ContainsAbilitiesConverter containsAbilitiesConverter = new ContainsAbilitiesConverter();
		DoubleToStringConverter doubleToStringConverter = new DoubleToStringConverter();
		RangeSpecialConverter rangeSpecialConverter = new RangeSpecialConverter();
		RangeTypeConverter rangeTypeConverter = new RangeTypeConverter();

		public void WriteFile(IEnumerable<Creature> creatures)
		{
			using (StreamWriter writer = new StreamWriter(File.Create("./Creatures.csv")))
			{
				writer.WriteLine(this.HeaderRow());

				foreach (Creature creature in creatures)
				{
					writer.WriteLine(this.BuildRow(creature));
				}
			}
		}

		private string HeaderRow()
		{
			StringBuilder builder = new StringBuilder();

			this.AppendValue(builder, "Left", true);
			this.AppendValue(builder, "Right", false);
			this.AppendValue(builder, "Body Parts", false);
			this.AppendValue(builder, "Rank", false);
			this.AppendValue(builder, "Coal", false);
			this.AppendValue(builder, "Elec", false);
			this.AppendValue(builder, "Power", false);
			this.AppendValue(builder, "E.HP", false);
			this.AppendValue(builder, "Hitpoints", false);
			this.AppendValue(builder, "Armour", false);
			this.AppendValue(builder, "Sight Radius", false);
			this.AppendValue(builder, "Size", false);
			this.AppendValue(builder, "Land Speed", false);
			this.AppendValue(builder, "Water Speed", false);
			this.AppendValue(builder, "Air Speed", false);
			this.AppendValue(builder, "Melee Damage", false);
			this.AppendValue(builder, "Range Damage 1", false);
			this.AppendValue(builder, "Artillery Type 1", false);
			this.AppendValue(builder, "Range Type 1", false);
			this.AppendValue(builder, "Range Damage 2", false);
			this.AppendValue(builder, "Artillery Type 2", false);
			this.AppendValue(builder, "Range Type 2", false);
			this.AppendValue(builder, "Horns", false);
			this.AppendValue(builder, "Barrier Destroy", false);
			this.AppendValue(builder, "Poison", false);
			this.AppendValue(builder, "Abilties", false);

			return builder.ToString();
		}

		private string BuildRow(Creature creature)
		{
			StringBuilder builder = new StringBuilder();
			this.AppendValue(builder, creature.Left, true);
			this.AppendValue(builder, creature.Right, false);
			this.AppendValue(builder, this.bodyPartsConverter.Convert(creature.BodyParts), false);
			this.AppendValue(builder, creature.Rank.ToString(), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Coal), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Electricity), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Power), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.EffectiveHitpoints), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Hitpoints), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Armour), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.SightRadius), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.Size), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.LandSpeed), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.WaterSpeed), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.AirSpeed), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.MeleeDamage), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.RangeDamage1), false);
			this.AppendValue(builder, this.rangeSpecialConverter.Convert(creature.RangeSpecial1), false);
			this.AppendValue(builder, this.rangeTypeConverter.Convert(creature.RangeType1), false);
			this.AppendValue(builder, this.doubleToStringConverter.Convert(creature.RangeDamage2), false);
			this.AppendValue(builder, this.rangeSpecialConverter.Convert(creature.RangeSpecial2), false);
			this.AppendValue(builder, this.rangeTypeConverter.Convert(creature.RangeType2), false);
			this.AppendValue(builder, this.boolToStringConverter.Convert(creature.HasHorns), false);
			this.AppendValue(builder, this.boolToStringConverter.Convert(creature.HasBarrierDestroy), false);
			this.AppendValue(builder, this.boolToStringConverter.Convert(creature.HasPoison), false);
			this.AppendValue(builder, this.containsAbilitiesConverter.Convert(creature.Abilities), false);
			return builder.ToString();
		}

		private void AppendValue(StringBuilder builder, object obj, bool firstColumn)
		{
			this.AppendValue(builder, obj.ToString(), firstColumn);
		}

		private void AppendValue(StringBuilder builder, string value, bool firstColumn)
		{
			if (!firstColumn)
			{
				builder.Append(',');
			}

			if (value.IndexOfAny(new[] { '"', ',' }) != -1)
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
