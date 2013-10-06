using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope.Unicode;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

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

        private IEnumerable<TaggedCodePoint> CodePointIterator(uint numberOfChars)
        {
            for (uint codePoint = 0; codePoint < numberOfChars; codePoint++)
                yield return codePoint;
        }

        [TestMethod]
        public void TestConstructFromIterator()
        {
            const int chars = 100;
            const int skip = 10;
            var fromIterator = new CodePointList(CodePointIterator(chars));

            fromIterator.Skip(skip).First().Should().Be(CodePointIterator(chars).Skip(skip).First());
        }

        [TestMethod]
        public void TestAstralCharCount()
        {
            new CodePointList(AstralCharSurrogatePair).Count.Should().Be(1);
        }

        [TestMethod]
        public void TestNonGenericIterator()
        {
            var nonGeneric = new CodePointList(AstralCharSurrogatePair).As<System.Collections.IEnumerable>().GetEnumerator();
            Assert.IsTrue(nonGeneric.MoveNext());
            Assert.AreEqual(AstralCodePoint, nonGeneric.Current);                
        }
    }
}
