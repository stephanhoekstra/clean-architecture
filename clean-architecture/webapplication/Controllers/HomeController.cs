using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webapplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //this is a sample application, sorry no homepage. let's get straight to the action. 
            return RedirectToAction("Create","Order");
        }
    }
}