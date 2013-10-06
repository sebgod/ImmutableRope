using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImmutableRope;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace ImmutableRopeTest
{
    [TestClass]
    public class ConversionTests
    {
        [TestMethod]
        public void TestRopeToString()
        {
            const string testString = "Rope";

            Assert.AreEqual(testString,  new Rope(testString).ToString());
        }

        [TestMethod]
        public void TestImplicitCastFromRopeToString()
        {
            const string testString = "Rope";
            Assert.AreEqual<string>(testString, new Rope(testString));
        }

        [TestMethod]
        public void TestImplicitCastFromStringToRope()
        {
            const string testString = "Rope";
            Rope implicitCast = testString;
            Assert.AreEqual<string>(testString, implicitCast.ToString());
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
