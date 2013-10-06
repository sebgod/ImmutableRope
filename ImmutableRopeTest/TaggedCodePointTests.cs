using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope.Unicode;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace ImmutableRopeTest
{
    [TestClass]
    public class TaggedCodePointTests
    {
        [TestMethod]
        public void TestBMPCharValue()
        {
            const char @char = 'a';
            Assert.AreEqual(@char, new TaggedCodePoint(@char).Value);
        }

        [TestMethod]
        public void TestBMPCharCastToSysChar()
        {
            const char @char = 'a';
            Assert.AreEqual(@char, (char)new TaggedCodePoint(@char));
        }

        [TestMethod]
        public void TestImplicitCastFromChar()
        {
            const char @char = 'a';
            Assert.AreEqual(@char, new TaggedCodePoint(@char));
        }

        [TestMethod]
        public void TestCodePointCastToInt32()
        {
            const string testString = "𝌲";
            int codePointInt32 = char.ConvertToUtf32(testString, 0);

            Assert.AreEqual(codePointInt32, (int)new TaggedCodePoint(testString));
        }

        [ExcludeFromCodeCoverage]
        void InvalidCastAction()
        {
            var _ = (char)new TaggedCodePoint("𝌲");
        }

        [TestMethod]
        public void TestInvalidCastFromAstralCharToSysChar()
        {
            var invalidCast = (Action)InvalidCastAction;
            invalidCast.ShouldThrow<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void TestCodePointCastToUInt32()
        {
            const string testString = "𝌲";
            var codePointInt32 = (uint)char.ConvertToUtf32(testString, 0);

            Assert.AreEqual(codePointInt32, (uint)new TaggedCodePoint(testString));
        }

        [TestMethod]
        public void TestImplicitCastFromInt32()
        {
            const string testString = "𝌲";
            int codePointInt32 = char.ConvertToUtf32(testString, 0);

            Assert.AreEqual<TaggedCodePoint>(codePointInt32, new TaggedCodePoint(testString));
        }

        [TestMethod]
        public void TestImplicitCastFromUInt32()
        {
            const string testString = "𝌲";
            var codePointInt32 = (uint)char.ConvertToUtf32(testString, 0);

            Assert.AreEqual<TaggedCodePoint>(codePointInt32, new TaggedCodePoint(testString));
        }
    }
}
