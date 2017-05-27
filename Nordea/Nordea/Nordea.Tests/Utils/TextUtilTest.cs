using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nordea.Models.Utils;
using Nordea.Utils;
using Nordea.Utils.Interfaces;

namespace Nordea.Tests.Utils
{

    [TestClass]
    public class TextUtilTest
    {

        [TestMethod]
        public void TextDetailsToXmlFromNormalSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input = "Mary had a little lamb.";
            var expected =
                "<?xml \"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<text>\r\n    <sentence>\r\n        <word>a</word>\r\n        <word>had</word>\r\n        <word>lamb</word>\r\n        <word>little</word>\r\n        <word>Mary</word>\r\n    </sentence>\r\n</text>";

            var result = textUtil.TextDetailsToXml(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextDetailsToCSVFromNormalSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input = "Mary had a little lamb.";
            var expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5\r\nSentence 1, a, had, lamb, little, Mary\r\n";

            var result = textUtil.TextDetailsToCSV(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextDetailsToXMLFromWrongSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input = "Mary had a little lamb";
            var expected =
                "<?xml \"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<text />";

            var result = textUtil.TextDetailsToXml(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextDetailsToCSVFromWrongSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input = "Mary had a little lamb";
            var expected = string.Empty;

            var result = textUtil.TextDetailsToCSV(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextDetailsToCSVFromSpecialCharactersInSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input = "Mary's lamb, black-blue.";
            var expected = ",Word 1,Word 2,Word 3\r\nSentence 1, black-blue, lamb, Mary's\r\n";

            var result = textUtil.TextDetailsToCSV(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TextDetailsToCSVFromManyWhiteSpacesInSentence()
        {
            ITextUtil textUtil = new TextUtil();
            var input =
                "\"  Mary   had a little  lamb  . \r\n\r\n\r\n  Peter   called for the wolf   ,  and Aesop came .\r\n Cinderella  likes shoes.\"\r\n";
            var expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5,Word 6,Word 7,Word 8\r\nSentence 1, a, had, lamb, little, Mary\r\nSentence 2, Aesop, and, called, came, for, Peter, the, wolf\r\nSentence 3, Cinderella, likes, shoes\r\n";

            var result = textUtil.TextDetailsToCSV(textUtil.SeparateTextDetails(input));

            Assert.AreEqual(expected, result);
        }

    }
}