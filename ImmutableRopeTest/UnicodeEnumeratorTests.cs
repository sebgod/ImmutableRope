using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope.Unicode;
using FluentAssertions;

namespace ImmutableRopeTest
{
    [TestClass]
    public class UnicodeEnumeratorTests : BasicTestClass
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext content)
        {
            SetupTestChars(content);
        }

        [TestMethod]
        public void TestIterator()
        {
            var asciCharArray = AsciiString.ToCharArray();

            var index = 0;
            foreach (char @char in new UnicodeEnumerator(AsciiString)) 
            {
                Assert.AreEqual(asciCharArray[index++], @char);    
            }       
        }

        [TestMethod]
        public void TestNonGenericIterator()
        {
            var nonGeneric = new UnicodeEnumerator(AstralCharSurrogatePair).As<System.Collections.IEnumerable>().GetEnumerator();
            Assert.IsTrue(nonGeneric.MoveNext());
            Assert.AreEqual(AstralCodePoint, nonGeneric.Current);
        }
    }
}
