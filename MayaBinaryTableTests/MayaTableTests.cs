using JetBrains.Annotations;

namespace MayaBinaryTable.Tests
{
	[TestFixture, TestSubject(typeof(MayaTable))]
	public class MayaTableTests
	{
		[Test]
		public void MayaTable_CreateTable()
		{
			MayaTable.HasMatches("a"); // it's static so it's whatever init it I guess
			Assert.Pass();
		}
		
		[Test]
		public void GetBytesFromExactStringTests()
		{
			using (Assert.EnterMultipleScope())
			{
				// Incorrect/Unknown strings
				Assert.That(MayaTable.GetBytesFromExactString("abcdef").Combined, Is.EqualTo(57)); 
				Assert.That(MayaTable.GetBytesFromExactString("\0x2555").Combined, Is.EqualTo(57));

				Assert.That(MayaTable.GetBytesFromExactString("a").Combined, Is.EqualTo(1));
				Assert.That(MayaTable.GetBytesFromExactString("a").FirstByte, Is.Null);

				Assert.That(MayaTable.GetBytesFromExactString("anyway").Combined, Is.EqualTo(14));
				Assert.That(MayaTable.GetBytesFromExactString("ANYWAY").Combined, Is.EqualTo(18));
				Assert.That(MayaTable.GetBytesFromExactString("anyway").FirstByte, Is.Null);

				// Space
				Assert.That(MayaTable.GetBytesFromExactString(" ").Combined, Is.EqualTo(53));

				// Enter
				Assert.That(MayaTable.GetBytesFromExactString("\n").Combined, Is.EqualTo(54));

				// Tab
				Assert.That(MayaTable.GetBytesFromExactString("	").Combined, Is.EqualTo(55));

				// Two bytes tests
				Assert.That(MayaTable.GetBytesFromExactString("%").Combined, Is.EqualTo(169));
				Assert.That(MayaTable.GetBytesFromExactString("%").FirstByte, Is.Not.Null);

				Assert.That(MayaTable.GetBytesFromExactString("MAYA").Combined, Is.EqualTo(172));
				Assert.That(MayaTable.GetBytesFromExactString("maya").FirstByte, Is.Not.Null);
			}
		}

		[Test]
		public void HasMatchesWithFilterTest()
		{
			string[] testTable1 = { "furina" };
			string[] testTable2 = { "f", "furina", "function" };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(MayaTable.HasMatchesWithFilter("fu", testTable1));
                Assert.That(MayaTable.HasMatchesWithFilter("fu", testTable2), Is.False);
            }

        }
	}
}