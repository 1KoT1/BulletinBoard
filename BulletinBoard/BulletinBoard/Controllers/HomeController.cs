using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            log.Debug("Run Index()");
            return View();
        }

        ILog log = LogManager.GetLogger(typeof(HomeController));
    }
}
