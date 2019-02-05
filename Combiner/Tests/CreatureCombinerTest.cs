namespace Combiner.Tests
{
	using NUnit.Framework;

	[TestFixture]
	class CreatureCombinerTest
	{
		[Test]
		public void GenerateBodyParts()
		{
			// bird + quad
			// bird torso -> wings
			// quad torso <-> front legs

			// bird + arachnid
			// bird torso -> wings
			// arachnid torso -> claws, front legs

			// bird + snake
			// bird torso -> wings

			// bird + insect
			// insect torso -> front legs

			// bird + fish
			// bird torso -> back legs, wings



			// quad + arachnid
			// arachnid torso -> claws (if clawed)

			// quad + snake
			// quad torso -> front legs, back legs

			// quad + insect
			// insect torso -> wings

			// quad + fish
			// quad torso -> front legs, back legs



			// arachnid + snake
			// arachnid torso -> front legs, back legs, claws (if clawed)

			// arachnid + insect
			// arachnid torso -> claws (if clawed)
			// insect torso -> wings

			// arachnid + fish
			// arachnid torso -> front legs, back legs, claws (if clawed)



			// snake + insect
			// insect torso -> front legs, back legs, wings

			// snake + fish
			// nothing



			// insect + fish
			// insect torso -> front legs, back legs, wings
		}
	}
}
