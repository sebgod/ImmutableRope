using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope.Unicode;
using FluentAssertions;

namespace ImmutableRopeTest
{
    [TestClass]
    public class CodePointListTests : BasicTestClass
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext content)
        {
            SetupTestChars(content);
        }

        [TestMethod]
        public void TestIndexer()
        {
            new CodePointList(AstralCharSurrogatePair)[0].Should().Be(AstralCodePoint);
        }
    }
}
