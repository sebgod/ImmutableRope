using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope;
using System.Globalization;
using FluentAssertions;

namespace ImmutableRopeTest
{
    [TestClass]
    public class EqualityTests
    {
        const string asciiString = "abcdef%6377";
        const string asciiStringL = "abcdef";
        const string asciiStringU = "ABCDEF";

        [TestMethod]
        public void TestIdentity()
        {
            var rope = new Rope(asciiString);
            var identity = rope;
            Assert.AreEqual<Rope>(rope, identity);
        }

        [TestMethod]
        public void TestIdentityOp()
        {
            var rope = new Rope(asciiString);
            var identity = rope;
            Assert.IsTrue(rope == identity);
        }

        [TestMethod]
        public void TestEqual()
        {
            var a = new Rope(asciiString);
            var b = new Rope(asciiString);

            Assert.AreEqual<Rope>(a, b);
        }

        [TestMethod]
        public void TestNotEqual()
        {
            var ropeL = new Rope(asciiStringL);
            var ropeU = new Rope(asciiStringU);

            ropeL.Should().NotBe(ropeU);
        }

        [TestMethod]
        public void TestNotEqualWithDifferentType()
        {
            new Rope(asciiString).Should().NotBe('c');
        }

        [TestMethod]
        public void TestSymmetrySame()
        {
            var c1 = new Rope(asciiString);
            var c2 = new Rope(asciiString);

            Assert.IsTrue(c1 == c2 & c2 == c1);
        }

        [TestMethod]
        public void TestSymmetryDifferent()
        {
            var c1 = new Rope(asciiStringL);
            var c2 = new Rope(asciiStringU);

            Assert.IsTrue(c1 != c2 & c2 != c1);
        }

        [TestMethod]
        public void TestValueNotEqualNullOpPositive()
        {
            var rope = new Rope(asciiString);
            var nullRope = null as Rope;

            Assert.IsTrue(rope != nullRope);
        }

        [TestMethod]
        public void TestValueNotEqualNullOpNegative()
        {
            var rope = new Rope(asciiString);
            var nullRope = null as Rope;

            Assert.IsFalse(rope == nullRope);
        }

        [TestMethod]
        public void TestNullNotEqualValueOpPositive()
        {
            var nullRope = null as Rope;
            var rope = new Rope(asciiString);

            Assert.IsTrue(nullRope != rope);
        }

        [TestMethod]
        public void TestNullNotEqualValueOpNegative()
        {
            var nullRope = null as Rope;
            var rope = new Rope(asciiString);

            Assert.IsFalse(nullRope == rope);
        }
        
        [TestMethod]
        public void TestEqualOpPositive()
        {
            var a = new Rope(asciiString);
            var b = new Rope(asciiString);

            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void TestEqualOpNegative()
        {
            var a = new Rope(asciiString);
            var b = new Rope(asciiString);

            Assert.IsFalse(a != b);
        }


        [TestMethod]
        public void TestNotEqualOpPositive()
        {
            var ropeL = new Rope(asciiStringL);
            var ropeU = new Rope(asciiStringU);

            Assert.IsTrue(ropeL != ropeU);
        }

        [TestMethod]
        public void TestNotEqualOpNegative()
        {
            var ropeL = new Rope(asciiStringL);
            var ropeU = new Rope(asciiStringU);

            Assert.IsFalse(ropeL == ropeU);
        }

    }
}
