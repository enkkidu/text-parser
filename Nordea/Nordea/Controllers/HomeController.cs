using System.Web.Mvc;
using Nordea.Models.ViewModels;
using System;
using Nordea.Service.Interfaces;

namespace Nordea.Controllers
{
    public class HomeController : Controller
    {
        private ITextService textService;

        public HomeController(ITextService textService)
        {
            this.textService = textService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.Result = textService.Parse(model.Text, model.ResultType);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Error", ex.Message);
                }
            }
            return View(model);
        }
    }
}