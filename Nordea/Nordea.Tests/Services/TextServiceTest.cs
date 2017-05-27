using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nordea.Models.Enum;
using Rhino.Mocks;
using Nordea.Utils.Interfaces;
using Nordea.Service;
using Nordea.Models.Utils;
using System.Collections.Generic;

namespace Nordea.Tests.Services
{
    [TestClass]
    public class TextServiceTest
    {
        [TestMethod]
        public void Should_Return_XML_When_Correct_Text()
        {
            var textUtil = MockRepository.GenerateStub<ITextUtil>();
            var text = "Mary had a little lamb.";
            var textDetails = new TextDetails() { Sentences = new List<Sentence> { new Sentence() } };
            var expected =
                "<?xml \"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<text>\r\n    <sentence>\r\n        <word>a</word>\r\n        <word>had</word>\r\n        <word>lamb</word>\r\n        <word>little</word>\r\n        <word>Mary</word>\r\n    </sentence>\r\n</text>";
            textUtil.Expect(x => x.SeparateTextDetails(text)).Return(textDetails);
            textUtil.Expect(x => x.TextDetailsToXml(textDetails)).Return(expected);

            var sut = new TextService(textUtil);

            var result = sut.Parse(text, ResultType.XML);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Should_Return_CSV_When_Correct_Text()
        {
            var textUtil = MockRepository.GenerateStub<ITextUtil>();
            var text = "Mary had a little lamb.";
            var textDetails = new TextDetails() { Sentences = new List<Sentence> { new Sentence() } };
            var expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5\r\nSentence 1, a, had, lamb, little, Mary\r\n";
            textUtil.Expect(x => x.SeparateTextDetails(text)).Return(textDetails);
            textUtil.Expect(x => x.TextDetailsToCSV(textDetails)).Return(expected);

            var sut = new TextService(textUtil);

            var result = sut.Parse(text, ResultType.CSV);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "No sentence found.Remember the sentence needs to end with. ! ?")]
        public void Should_ThrowException_When_No_Sentence_In_Text()
        {
            var textUtil = MockRepository.GenerateStub<ITextUtil>();
            var text = "Mary had a little lamb";
            var textDetails = new TextDetails { Sentences = new List<Sentence>{ } };
            var expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5\r\nSentence 1, a, had, lamb, little, Mary\r\n";
            textUtil.Expect(x => x.SeparateTextDetails(text)).Return(textDetails);
            textUtil.Expect(x => x.TextDetailsToXml(textDetails)).Return(expected);

            var sut = new TextService(textUtil);

            var result = sut.Parse(text, ResultType.CSV);
        }
    }
}
