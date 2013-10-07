using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope;
using System.Globalization;
using FluentAssertions;
using ImmutableRope.Unicode;
using System.Collections.Generic;

namespace ImmutableRopeTest
{
    [TestClass]
    public class EqualityTests : BasicTestClass
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext content)
        {
            SetupTestChars(content);
        }

        [TestMethod]
        public void TestIdentity()
        {
            var rope = new Rope(AsciiString);
            var identity = rope;
            Assert.AreEqual<Rope>(rope, identity);
        }

        [TestMethod]
        public void TestIdentityOp()
        {
            var rope = new Rope(AsciiString);
            var identity = rope;
            Assert.IsTrue(rope == identity);
        }

        [TestMethod]
        public void TestEqual()
        {
            var a = new Rope(AsciiString);
            var b = new Rope(AsciiString);

            Assert.AreEqual<Rope>(a, b);
        }

        [TestMethod]
        public void TestNotEqual()
        {
            var ropeL = new Rope(AsciiStringL);
            var ropeU = new Rope(AsciiStringU);

            Assert.AreNotEqual(ropeL, ropeU);
        }

        [TestMethod]
        public void TestNotEqualWithDifferentType()
        {
            Assert.AreNotEqual(new Rope(AsciiString), new object());
        }

        [TestMethod]
        public void TestSymmetrySame()
        {
            var c1 = new Rope(AsciiString);
            var c2 = new Rope(AsciiString);

            Assert.IsTrue(c1 == c2 & c2 == c1);
        }

        [TestMethod]
        public void TestSymmetryDifferent()
        {
            var c1 = new Rope(AsciiStringL);
            var c2 = new Rope(AsciiStringU);

            Assert.IsTrue(c1 != c2 & c2 != c1);
        }

        [TestMethod]
        public void TestValueNotEqualNullOpPositive()
        {
            var rope = new Rope(AsciiString);
            var nullRope = null as Rope;

            Assert.IsTrue(rope != nullRope);
        }

        [TestMethod]
        public void TestValueNotEqualNullOpNegative()
        {
            var rope = new Rope(AsciiString);
            var nullRope = null as Rope;

            Assert.IsFalse(rope == nullRope);
        }

        [TestMethod]
        public void TestNullNotEqualValueOpPositive()
        {
            var nullRope = null as Rope;
            var rope = new Rope(AsciiString);

            Assert.IsTrue(nullRope != rope);
        }

        [TestMethod]
        public void TestNullNotEqualValueOpNegative()
        {
            var nullRope = null as Rope;
            var rope = new Rope(AsciiString);

            Assert.IsFalse(nullRope == rope);
        }
        
        [TestMethod]
        public void TestEqualOpPositive()
        {
            var a = new Rope(AsciiString);
            var b = new Rope(AsciiString);

            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void TestEqualOpNegative()
        {
            var a = new Rope(AsciiString);
            var b = new Rope(AsciiString);

            Assert.IsFalse(a != b);
        }


        [TestMethod]
        public void TestNotEqualOpPositive()
        {
            var ropeL = new Rope(AsciiStringL);
            var ropeU = new Rope(AsciiStringU);

            Assert.IsTrue(ropeL != ropeU);
        }

        [TestMethod]
        public void TestNotEqualOpNegative()
        {
            var ropeL = new Rope(AsciiStringL);
            var ropeU = new Rope(AsciiStringU);

            Assert.IsFalse(ropeL == ropeU);
        }

        [TestMethod]
        public void TestRoundTripToEnumerator()
        {
            var ropeOriginal = new Rope(AsciiString);
            var codePointList = new CodePointList(ropeOriginal);
            var ropeCopy = new Rope(codePointList);

            Assert.AreEqual(ropeOriginal, ropeCopy);
        }

        [TestMethod]
        public void TestHashSet()
        {
            var hashSet = new HashSet<Rope>()
            {
                new Rope(AsciiStringL),
                new Rope(AsciiStringU)
            };
            
            Assert.IsFalse(hashSet.Add(AsciiStringL));

            hashSet.Count.Should().Be(2);
        }
    }
}
