using JetBrains.Annotations;
using MayaBinTable.Common;
using MayaBinTable.Encoding;

namespace MayaBinTable.Tests
{
    [TestFixture, TestSubject(typeof(MayaTable))]
    public class MayaTableTests
    {
        [Test]
        public void GetOffsetTest()
        {
            var expected = 37;
            var actual = MayaTable.GetOffset(8);
            
            Assert.That(actual,Is.EqualTo(expected));
        }

        [Test]
        public void GetEntryTest()
        {
            var expected = "gah";
            var actual = MayaTable.GetEntry(37);
            
            Assert.That(actual,Is.EqualTo(expected));
        }
    }
}