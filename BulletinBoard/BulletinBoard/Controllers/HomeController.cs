using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BulletinBoard.Models;

namespace BulletinBoard.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            log.Debug("Run Index()");
            return View(repository.Advertisements);
        }

        ILog log = LogManager.GetLogger(typeof(HomeController));
    }
}
