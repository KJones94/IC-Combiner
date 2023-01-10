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
			var rankGroups = allCreatures.GroupBy(c => c.Rank);
			var rankDamageTotals = new double[5];
			foreach (var group in rankGroups)
			{
				var rankIdx = group.Key - 1;
				rankDamageTotals[rankIdx] = group.Sum(c => c.MeleeDamage);
				rankDamageTotals[rankIdx] /= group.Count();
			}
		}
	}
}