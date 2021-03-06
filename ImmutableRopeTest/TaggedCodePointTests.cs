﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope.Unicode;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace ImmutableRopeTest
{
    [TestClass]
    public class TaggedCodePointTests : BasicTestClass
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext content)
        {
            SetupTestChars(content);
        }

        [TestMethod]
        public void TestBMPCharValue()
        {
            Assert.AreEqual(AsciiChar, new TaggedCodePoint(AsciiChar).Value);
        }

        [TestMethod]
        public void TestBMPCodePointToString()
        {
            new TaggedCodePoint(AsciiChar).ToString().Should().Be(new string(AsciiChar, 1));
        }

        [TestMethod]
        public void TestAstralCodePointToString()
        {
            new TaggedCodePoint(AstralCharSurrogatePair).ToString().Should().Be(AstralCharSurrogatePair);
        }

        [TestMethod]
        public void TestSurrogatePairConstructor()
        {
            Assert.AreEqual(AstralCharSurrogatePair, new TaggedCodePoint(HighSurrogate, LowSurrogate).ToString());
        }

        [TestMethod]
        public void TestBMPCharCastToSysChar()
        {
            Assert.AreEqual(AsciiChar, (char)new TaggedCodePoint(AsciiChar));
        }

        [TestMethod]
        public void TestImplicitCastFromChar()
        {
            Assert.AreEqual(AsciiChar, new TaggedCodePoint(AsciiChar));
        }

        [TestMethod]
        public void TestCodePointCastToInt32()
        {
            Assert.AreEqual(CodePointInt32, (int)new TaggedCodePoint(AstralCharSurrogatePair));
        }

        [ExcludeFromCodeCoverage]
        void InvalidCastFromAstralCharToSysCharAction()
        {
            var _ = (char)new TaggedCodePoint(AstralCharSurrogatePair);
        }

        [TestMethod]
        public void TestInvalidCastFromAstralCharToSysChar()
        {
            ((Action)InvalidCastFromAstralCharToSysCharAction).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void TestCodePointCastToUInt32()
        {
            Assert.AreEqual((uint)CodePointInt32, (uint)new TaggedCodePoint(AstralCharSurrogatePair));
        }

        [TestMethod]
        public void TestImplicitCastFromInt32()
        {
            Assert.AreEqual<TaggedCodePoint>(CodePointInt32, new TaggedCodePoint(AstralCharSurrogatePair));
        }

        [TestMethod]
        public void TestImplicitCastFromUInt32()
        {
            Assert.AreEqual<TaggedCodePoint>(CodePointInt32, new TaggedCodePoint(AstralCharSurrogatePair));
        }

        [ExcludeFromCodeCoverage]
        void IllegalCodePointInConstructorAction()
        {
            var _ = (char)new TaggedCodePoint(UInt32.MaxValue);
        }

        [TestMethod]
        public void TestIllegalCodePointInConstructor()
        {
            ((Action)IllegalCodePointInConstructorAction).ShouldThrow<ArgumentOutOfRangeException>();
        }
        
        [ExcludeFromCodeCoverage]
        void SurrogateInSysCharConstructorAction()
        {
            var _ = new TaggedCodePoint(LowSurrogate);
        }

        [TestMethod]
        public void TestSurrogateInSysCharConstructor()
        {
            ((Action)SurrogateInSysCharConstructorAction).ShouldThrow<ArgumentOutOfRangeException>();
        }


        [ExcludeFromCodeCoverage]
        void IllegalCastFromNegativeCodePointAction()
        {
            var _ = (TaggedCodePoint)(Int32.MinValue);
        }

        [TestMethod]
        public void TestIllegalCastFromNegativeCodePoint()
        {
            ((Action)IllegalCastFromNegativeCodePointAction).ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}
