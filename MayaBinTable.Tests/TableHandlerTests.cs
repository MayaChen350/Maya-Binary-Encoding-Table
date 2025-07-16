using JetBrains.Annotations;
using MayaBinTable.Common;
using MayaBinTable.Encoding;

namespace MayaBinTable.Tests;

[TestFixture, TestSubject(typeof(TableHandler))]
public class TableHandlerTests
{
    [Test]
    public void MayaTable_CreateTable()
    {
        TableHandler.HasMatches("a"); // it's static so it's whatever init it I guess
        Assert.Pass();
    }

    [Test]
    public void GetBytesFromExactStringTests()
    {
        using (Assert.EnterMultipleScope())
        {
            // Incorrect/Unknown strings
            Assert.That(TableHandler.GetBytesFromExactString("abcdef").Combined, Is.EqualTo(57));
            Assert.That(TableHandler.GetBytesFromExactString("\0x2555").Combined, Is.EqualTo(57));

            Assert.That(TableHandler.GetBytesFromExactString("a").Combined, Is.EqualTo(1));
            Assert.That(TableHandler.GetBytesFromExactString("a").FirstByte, Is.Null);

            Assert.That(TableHandler.GetBytesFromExactString("anyway").Combined, Is.EqualTo(14));
            Assert.That(TableHandler.GetBytesFromExactString("ANYWAY").Combined, Is.EqualTo(18));
            Assert.That(TableHandler.GetBytesFromExactString("anyway").FirstByte, Is.Null);

            // Space
            Assert.That(TableHandler.GetBytesFromExactString(" ").Combined, Is.EqualTo(53));

            // Enter
            Assert.That(TableHandler.GetBytesFromExactString("\n").Combined, Is.EqualTo(54));

            // Tab
            Assert.That(TableHandler.GetBytesFromExactString("	").Combined, Is.EqualTo(55));

            // Two bytes tests
            Assert.That(TableHandler.GetBytesFromExactString("%").Combined, Is.EqualTo(169));
            Assert.That(TableHandler.GetBytesFromExactString("%").FirstByte, Is.Not.Null);

            Assert.That(TableHandler.GetBytesFromExactString("MAYA").Combined, Is.EqualTo(172));
            Assert.That(TableHandler.GetBytesFromExactString("maya").FirstByte, Is.Not.Null);
        }
    }

    [Test]
    public void HasMatchesWithFilterTest()
    {
        string[] testTable1 = { "furina" };
        string[] testTable2 = { "f", "furina", "function" };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(TableHandler.HasMatchesWithFilter("fu", testTable1));
            Assert.That(TableHandler.HasMatchesWithFilter("fu", testTable2), Is.False);
        }
    }
}