using Microsoft.VisualStudio.TestTools.UnitTesting;
using Combiner.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner.Tests.Tests
{
    [TestClass()]
    public class CreatureTestTests
    {
        [TestMethod()]
		public void CalculateAverage()
		{
			var db = new Database();
			var mod = db.GetAllMods().First();
			var allCreatures = db.GetAllCreatures(mod);
			var rankGroups = allCreatures.GroupBy(c => c.Rank).OrderBy(c => c.Key);
			var rankDamageTotals = new Dictionary<int, Dictionary<string, double>>();
			foreach (var group in rankGroups)
			{
				var rankIdx = group.Key;
				rankDamageTotals.Add(group.Key, new Dictionary<string, double>());
				rankDamageTotals[rankIdx]["MeleeDamage"] = group.Sum(c => c.MeleeDamage);
				rankDamageTotals[rankIdx]["MeleeDamage"] /= group.Count();
				rankDamageTotals[rankIdx]["EHP"] = group.Sum(c => c.EffectiveHitpoints);
				rankDamageTotals[rankIdx]["EHP"] /= group.Count();
				rankDamageTotals[rankIdx]["SuiCo"] = group.Sum(c => c.SuicideCoefficient);
				rankDamageTotals[rankIdx]["SuiCo"] /= group.Count();
			}
		}
	}
}