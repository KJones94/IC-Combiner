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
    }
}
