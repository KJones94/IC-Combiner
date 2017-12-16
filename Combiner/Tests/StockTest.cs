using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combiner.Tests
{
    [TestFixture]
    class StockTest
    {
        private Dictionary<Limb, bool> CreateBodyParts(bool[] values)
        {
            Limb[] limbs = (Limb[])Enum.GetValues(typeof(Limb));
            return limbs.Zip(values, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
        }

        [Test]
        public void SetupBodyParts()
        {
            LuaHandler lua = new LuaHandler();
            Stock testStock;
			bool[] testBodyParts;

			// Bird
			testStock = StockFactory.Instance.CreateStock("eagle", lua);
			testBodyParts = new bool[] { false, true, false, true, true, true, true, true, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			//Quadruped
			testStock = StockFactory.Instance.CreateStock("coyote", lua);
			testBodyParts = new bool[] { false, true, true, true, true, true, true, false, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Arachnid
			testStock = StockFactory.Instance.CreateStock("scorpion", lua);
			testBodyParts = new bool[] { false, true, true, true, true, true, true, false, true };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Snake
			testStock = StockFactory.Instance.CreateStock("rattlesnake", lua);
			testBodyParts = new bool[] { false, true, false, false, true, true, true, false, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Insect
			testStock = StockFactory.Instance.CreateStock("dragonfly", lua);
			testBodyParts = new bool[] { false, true, true, true, true, true, true, true, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Fish
			testStock = StockFactory.Instance.CreateStock("archerfish", lua);
			testBodyParts = new bool[] { false, true, false, false, true, true, true, false, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			/** EDGE CASES **/

			// Siphonophore
			testStock = StockFactory.Instance.CreateStock("siphonophore", lua);
			testBodyParts = new bool[] { false, true, false, false, true, true, true, false, true };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Humpback
			testStock = StockFactory.Instance.CreateStock("humpback", lua);
			testBodyParts = new bool[] { false, true, true, false, true, true, true, false, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

			// Ground Insects (Clawless Arachnids)
			testStock = StockFactory.Instance.CreateStock("ant", lua);
			testBodyParts = new bool[] { false, true, true, true, true, true, true, false, false };
			CollectionAssert.AreEquivalent(testStock.BodyParts, CreateBodyParts(testBodyParts));

		}

		// How to test doubles?
		[Test]
		public void CalcLimbHitpoints()
		{
			LuaHandler lua = new LuaHandler();
			Stock chimp = StockFactory.Instance.CreateStock("chimpanzee", lua);
			Stock bull = StockFactory.Instance.CreateStock("bull", lua);

			Assert.AreEqual(chimp.CalcLimbHitpoints(bull, Limb.FrontLegs), 47, 1.0);
			Assert.AreEqual(chimp.CalcLimbHitpoints(bull, Limb.BackLegs), 47, 1.0);
			Assert.AreEqual(chimp.CalcLimbHitpoints(bull, Limb.Head), 23, 1.0);
			Assert.AreEqual(chimp.CalcLimbHitpoints(bull, Limb.Torso), 117, 1.0);
			Assert.AreEqual(bull.CalcLimbHitpoints(chimp, Limb.FrontLegs), 44, 1.0);
			Assert.AreEqual(bull.CalcLimbHitpoints(chimp, Limb.BackLegs), 44, 1.0);
			Assert.AreEqual(bull.CalcLimbHitpoints(chimp, Limb.Head), 22, 1.0);
			Assert.AreEqual(bull.CalcLimbHitpoints(chimp, Limb.Torso), 111, 1.0);
		}

		[Test]
		public void CalcLimbDamage()
		{
			LuaHandler lua = new LuaHandler();
			Stock chimp = StockFactory.Instance.CreateStock("chimpanzee", lua);
			Stock bull = StockFactory.Instance.CreateStock("bull", lua);

			Assert.AreEqual(chimp.CalcLimbMeleeDamage(bull, Limb.FrontLegs), 4, 1.0);
			Assert.AreEqual(chimp.CalcLimbMeleeDamage(bull, Limb.Head), 3, 1.0);
			Assert.AreEqual(bull.CalcLimbMeleeDamage(chimp, Limb.Head), 10, 1.0);
		}
    }
}
