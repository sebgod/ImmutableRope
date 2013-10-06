using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace ImmutableRopeTest
{
    [TestClass]
    public class ConversionTests : BasicTestClass
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext content)
        {
            SetupTestChars(content);
        }

        [TestMethod]
        public void TestRopeToString()
        {
            new Rope(asciiString).ToString().Should().Be(asciiString);
        }

        [TestMethod]
        public void TestImplicitCastFromRopeToString()
        {
            Assert.AreEqual<Rope>(new Rope(asciiString), asciiString);
        }

        [TestMethod]
        public void TestImplicitCastFromStringToRope()
        {
            Rope implicitCast = asciiString;
            implicitCast.ToString().Should().Be(asciiString);
        }

        [TestMethod]
        public void TestAstralChar()
        {
            const string testString = "𝌲";
            Assert.AreEqual(testString, new Rope(testString).ToString());
        }

        [TestMethod]
        public void TestAstralCharsOnly()
        {
            const string taiXuanJing = @"
𝌀 𝌁 𝌂 𝌃 𝌄 𝌅 𝌆 𝌇 𝌈 𝌉 𝌊 𝌋 𝌌 𝌍 𝌎 𝌏 
𝌐 𝌑 𝌒 𝌓 𝌔 𝌕 𝌖 𝌗 𝌘 𝌙 𝌚 𝌛 𝌜 𝌝 𝌞 𝌟 
𝌠 𝌡 𝌢 𝌣 𝌤 𝌥 𝌦 𝌧 𝌨 𝌩 𝌪 𝌫 𝌬 𝌭 𝌮 𝌯 
𝌰 𝌱 𝌲 𝌳 𝌴 𝌵 𝌶 𝌷 𝌸 𝌹 𝌺 𝌻 𝌼 𝌽 𝌾 𝌿 
𝍀 𝍁 𝍂 𝍃 𝍄 𝍅 𝍆 𝍇 𝍈 𝍉 𝍊 𝍋 𝍌 𝍍 𝍎 𝍏 
𝍐 𝍑 𝍒 𝍓 𝍔 𝍕 𝍖 
";
            Assert.AreEqual(taiXuanJing, new Rope(taiXuanJing).ToString());
        }

        [ExcludeFromCodeCoverage]
        void InvalidCastFromAstralCharToSysCharAction()
        {
            const string testAstralChar = "𝌲";
            var brokenString = testAstralChar.Substring(0, 1);
            new Rope(brokenString);
        }

        [TestMethod]
        public void TestInvalidCastFromAstralCharToSysChar()
        {
            ((Action)InvalidCastFromAstralCharToSysCharAction).ShouldThrow<InvalidOperationException>();
        }
    }
}
