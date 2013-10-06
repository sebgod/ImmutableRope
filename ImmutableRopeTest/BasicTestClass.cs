using ImmutableRope.Unicode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace ImmutableRopeTest
{
    public abstract class BasicTestClass
    {
        internal static char ASCIIChar;
        internal static string asciiString;
        internal static string asciiStringL;
        internal static string asciiStringU;
        internal static string AstralCharSurrogatePair;
        internal static TaggedCodePoint AstralCodePoint;
        internal static char HighSurrogate;
        internal static char LowSurrogate;
        internal static Int32 CodePointInt32;

        public static void SetupTestChars(TestContext context)
        {
            ASCIIChar = 'a';
            AstralCharSurrogatePair = "𝌲";
            AstralCharSurrogatePair.Length.Should().Be(2);
            AstralCodePoint = new TaggedCodePoint(AstralCharSurrogatePair, 0);

            asciiString = "ABCslxheabcdef%6377";
            asciiStringL = asciiString.ToLowerInvariant();
            asciiStringU = asciiString.ToUpperInvariant();

            HighSurrogate = AstralCharSurrogatePair[0];
            Assert.IsTrue(char.IsHighSurrogate(HighSurrogate));

            LowSurrogate = AstralCharSurrogatePair[1];
            Assert.IsTrue(char.IsLowSurrogate(LowSurrogate));

            CodePointInt32 = char.ConvertToUtf32(AstralCharSurrogatePair, 0);
            CodePointInt32.Should().BeGreaterThan(char.MaxValue);
        }

    }
}
