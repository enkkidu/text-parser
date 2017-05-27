using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nordea.Controllers;
using Nordea.Models.Enum;
using Nordea.Models.ViewModels;
using Rhino.Mocks;
using Nordea.Service.Interfaces;

namespace Nordea.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Should_Return_XML_When_Correct_Text()
        {
            var textService = MockRepository.GenerateStub<ITextService>();
            var input = new IndexViewModel { ResultType = ResultType.XML, Text = "Mary had a little lamb." };
            var expected =
                "<?xml \"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<text>\r\n    <sentence>\r\n        <word>a</word>\r\n        <word>had</word>\r\n        <word>lamb</word>\r\n        <word>little</word>\r\n        <word>Mary</word>\r\n    </sentence>\r\n</text>";
            textService.Expect(x => x.Parse(input.Text, input.ResultType)).Return(expected);

            var sut = new HomeController(textService);
            
            ViewResult result = sut.Index(input) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewBag.Result);
        }

        [TestMethod]
        public void Should_Return_CSV_When_Correct_Text()
        {
            var textService = MockRepository.GenerateStub<ITextService>();
            var input = new IndexViewModel { ResultType = ResultType.CSV, Text = "Mary had a little lamb." };
            var expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5\r\nSentence 1, a, had, lamb, little, Mary\r\n";
            textService.Expect(x => x.Parse(input.Text, input.ResultType)).Return(expected);

            var sut = new HomeController(textService);

            ViewResult result = sut.Index(input) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.ViewBag.Result);
        }

        [TestMethod]
        public void Should_Add_ModelState_Error_When_No_Sentence_In_Text()
        {
            var textService = MockRepository.GenerateStub<ITextService>();
            var input = new IndexViewModel { ResultType = ResultType.XML, Text = "Mary had a little lamb" };
            var expectedExceptionMessage =
                "No sentence found.Remember the sentence needs to end with. ! ?";
            textService.Expect(x => x.Parse(input.Text, input.ResultType)).Throw(new Exception(expectedExceptionMessage));

            var sut = new HomeController(textService);

            ViewResult result = sut.Index(input) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData.ModelState["Error"]);
            Assert.IsNotNull(result.ViewData.ModelState["Error"].Errors);
            Assert.AreEqual(result.ViewData.ModelState["Error"].Errors.Count,1);
            Assert.AreEqual(result.ViewData.ModelState["Error"].Errors[0].ErrorMessage, expectedExceptionMessage);

        }
    }
}
