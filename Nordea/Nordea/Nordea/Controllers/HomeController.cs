using System.Linq;
using System.Web.Mvc;
using Nordea.Models.Enum;
using Nordea.Models.ViewModels;
using Nordea.Utils;
using Nordea.Utils.Interfaces;

namespace Nordea.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexViewModel model) {
            if (ModelState.IsValid) {
                ITextUtil textUtil = new TextUtil();
                var textDetails = textUtil.SeparateTextDetails(model.Text);
                if (!textDetails.Sentences.Any())
                {
                    ModelState.AddModelError("Error", "No sentence found. Remember the sentence needs to end with . ! ?");
                }
                
                switch (model.ResultType)
                {
                    case  ResultType.CSV:
                        ViewBag.Result = textUtil.TextDetailsToCSV(textDetails);

                        break;
                    case ResultType.XML:
                        ViewBag.Result = textUtil.TextDetailsToXml(textDetails);
                        break;
                    default:
                        break;
                }
            }
            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}