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
            new Rope(AsciiString).ToString().Should().Be(AsciiString);
        }

        [TestMethod]
        public void TestImplicitCastFromRopeToString()
        {
            string implicitCast = new Rope(AsciiString);
            implicitCast.Should().Be(AsciiString);
        }

        [TestMethod]
        public void TestImplicitCastFromStringToRope()
        {
            Rope implicitCast = AsciiString;
            implicitCast.ToString().Should().Be(AsciiString);
        }

        [TestMethod]
        public void TestAstralChar()
        {
            Assert.AreEqual(AstralCharSurrogatePair, new Rope(AstralCharSurrogatePair).ToString());
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
            // disabled Because Code will never readed the end of the procedure
// ReSharper disable ObjectCreationAsStatement
            new Rope(AstralCharSurrogatePair.Substring(0, 1));
// ReSharper restore ObjectCreationAsStatement
        }

        [TestMethod]
        public void TestInvalidCastFromAstralCharToSysChar()
        {
            ((Action)InvalidCastFromAstralCharToSysCharAction).ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void TestNonGenericIterator()
        {
            var nonGeneric = new Rope(AstralCharSurrogatePair).As<System.Collections.IEnumerable>().GetEnumerator();
            Assert.IsTrue(nonGeneric.MoveNext());
            Assert.AreEqual(AstralCodePoint, nonGeneric.Current);    
        }
    }
}
