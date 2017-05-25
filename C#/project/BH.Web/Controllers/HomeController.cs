using BH.Application.SystemManage;
using BH.Code;
using BH.Domain.Entity;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace BH.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
