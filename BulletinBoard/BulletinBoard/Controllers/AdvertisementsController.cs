using BulletinBoard.Models;
using BulletinBoard.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulletinBoard.Controllers
{
    public class AdvertisementsController : BaseController
    {
        //
        // GET: /Advertisements/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Save(CreateAdvertisementView newAdvertisement)
        {
            log.Debug("Run Save(Advertisement newAdvertisement)");
            repository.CreateAdvertisement(new Advertisement(newAdvertisement.Name, newAdvertisement.Description, Convert.ToUInt32(newAdvertisement.Price), DateTime.Now, new Contacts(newAdvertisement.Contacts)));
            return View();
        }
        public ActionResult Create()
        {
            log.Debug("Run Create()");
            return View(new CreateAdvertisementView());
        }

        ILog log = LogManager.GetLogger(typeof(AdvertisementsController));
    }
}
