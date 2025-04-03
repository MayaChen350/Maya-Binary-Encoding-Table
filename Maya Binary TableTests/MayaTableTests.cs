using MayaBinaryTable;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MayaBinaryTable.Tests
{
	[TestClass()]
	public class MayaTableTests
	{
		[TestMethod()]
		public void MayaTableTest()
		{
			MayaTable table = MayaTable.Get();
			Assert.IsNotNull(table);
		}

		[TestMethod()]
		public void GetBytesFromExactStringTest()
		{
			MayaTable table = MayaTable.Get();

			try
			{
				table.GetBytesFromExactString("abcdef");
				Assert.Fail("An exception was expected");
			}
			catch { }

			Assert.AreEqual(57, table.GetBytesFromExactString("\0x2555").Combined);

			Assert.AreEqual((short)1, table.GetBytesFromExactString("a").Combined);
			Assert.IsNull(table.GetBytesFromExactString("a").FirstByte);

			Assert.AreEqual((short)14, table.GetBytesFromExactString("anyway").Combined);
			Assert.AreEqual((short)18, table.GetBytesFromExactString("ANYWAY").Combined);
			Assert.IsNull(table.GetBytesFromExactString("anyway").FirstByte);

			// Space
			Assert.AreEqual((short)53, table.GetBytesFromExactString(" ").Combined);

			// Enter
			Assert.AreEqual((short)54, table.GetBytesFromExactString("\n").Combined);

			// Tab
			Assert.AreEqual((short)55, table.GetBytesFromExactString("	").Combined);

			// Two bytes tests
			Assert.AreEqual((short)169, table.GetBytesFromExactString("%").Combined);
			Assert.IsNotNull(table.GetBytesFromExactString("%").FirstByte);

			Assert.AreEqual((short)172, table.GetBytesFromExactString("MAYA").Combined);
			Assert.IsNotNull(table.GetBytesFromExactString("maya").FirstByte);

		}

		[TestMethod()]
		public void HasMatchesWithFilterTest()
		{
			MayaTable table = MayaTable.Get();

			string[] testTable1 = { "vince" };
			string[] testTable2 = { "v", "vince" };

			Assert.IsTrue(MayaTable.HasMatchesWithFilter("v", testTable1));
			Assert.IsFalse(MayaTable.HasMatchesWithFilter("v", testTable2));
			
		}
	}
}