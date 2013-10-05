using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope;

namespace ImmutableRopeTest
{
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void TestImplicitOp()
        {
            const string testString = "Rope";
            Rope rope = testString;

            Assert.AreEqual(testString, (string)rope);
        }
    }
}
