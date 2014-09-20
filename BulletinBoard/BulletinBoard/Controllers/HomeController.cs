using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BulletinBoard.Models;
using Ninject;

namespace BulletinBoard.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        private IRepository repository { get; set; }
        //
        // GET: /Home/

        public HomeController()
        {
            repository = DependencyResolver.Current.GetService<IRepository>();
        }
        public ActionResult Index()
        {
            log.Debug("Run Index()");
            return View(repository.Advertisements);
        }

        ILog log = LogManager.GetLogger(typeof(HomeController));
    }
}
