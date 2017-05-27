using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nordea;
using Nordea.Controllers;
using Nordea.Models.Enum;
using Nordea.Models.ViewModels;

namespace Nordea.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexShowXML()
        {
            var controller = new HomeController();
            var input = new IndexViewModel {ResultType = ResultType.XML, Text = "Mary had a little lamb."};
            string expected =
                "<?xml \"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\r\n<text>\r\n    <sentence>\r\n        <word>a</word>\r\n        <word>had</word>\r\n        <word>lamb</word>\r\n        <word>little</word>\r\n        <word>Mary</word>\r\n    </sentence>\r\n</text>";


            ViewResult result = controller.Index(input) as ViewResult;


            Assert.AreEqual(expected, result.ViewBag.Result);
        }

        [TestMethod]
        public void IndexShowCSV()
        {
            var controller = new HomeController();
            var input = new IndexViewModel {ResultType = ResultType.CSV, Text = "Mary had a little lamb."};
            string expected =
                ",Word 1,Word 2,Word 3,Word 4,Word 5\r\nSentence 1, a, had, lamb, little, Mary\r\n";

            ViewResult result = controller.Index(input) as ViewResult;

            Assert.AreEqual(expected, result.ViewBag.Result);
        }
    }
}
