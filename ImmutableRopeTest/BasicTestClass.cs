using ImmutableRope.Unicode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FluentAssertions;

namespace ImmutableRopeTest
{
    public abstract class BasicTestClass
    {
        internal static char AsciiChar;
        internal static string AsciiString;
        internal static string AsciiStringL;
        internal static string AsciiStringU;
        internal static string AstralCharSurrogatePair;
        internal static TaggedCodePoint AstralCodePoint;
        internal static char HighSurrogate;
        internal static char LowSurrogate;
        internal static Int32 CodePointInt32;

        public static void SetupTestChars(TestContext context)
        {
            AsciiChar = 'a';
            AstralCharSurrogatePair = "𝌲";
            AstralCharSurrogatePair.Length.Should().Be(2);
            AstralCodePoint = new TaggedCodePoint(AstralCharSurrogatePair, 0);

            AsciiString = "ABCslxheabcdef%6377";
            AsciiStringL = AsciiString.ToLowerInvariant();
            AsciiStringU = AsciiString.ToUpperInvariant();

            HighSurrogate = AstralCharSurrogatePair[0];
            Assert.IsTrue(char.IsHighSurrogate(HighSurrogate));

            LowSurrogate = AstralCharSurrogatePair[1];
            Assert.IsTrue(char.IsLowSurrogate(LowSurrogate));

            CodePointInt32 = char.ConvertToUtf32(AstralCharSurrogatePair, 0);
            CodePointInt32.Should().BeGreaterThan(char.MaxValue);
        }

    }
}
