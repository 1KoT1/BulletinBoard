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
            log.Debug("Run Index()");
            return View(repository.Advertisements);
        }
        public ActionResult Save(CreateAdvertisementView newAdvertisement)
        {
            log.Debug("Run Save(Advertisement newAdvertisement)");
            if (ValidateNewAdvertisement(newAdvertisement))
            {
                repository.CreateAdvertisement(new Advertisement(newAdvertisement.Name, newAdvertisement.Description, Int32.Parse(newAdvertisement.Price), DateTime.Now, new Contacts(newAdvertisement.Contacts)));
                return RedirectToAction("Index", "Advertisements");
            }
            else
            {
                return View("Create", newAdvertisement);
            }
        }

        private bool ValidateNewAdvertisement(CreateAdvertisementView newAdvertisement)
        {
            bool newAdvertisementValid = true;

            newAdvertisementValid &= CheckRuleAndSetErrMessage(() => !String.IsNullOrWhiteSpace(newAdvertisement.Name), "Name", "Ведите заголовок объявления.");

            newAdvertisementValid &= CheckRuleAndSetErrMessage(() => !String.IsNullOrWhiteSpace(newAdvertisement.Description), "Description", "Ведите описание объявления.");

            long price = 0;
            bool priceIsNumber = CheckRuleAndSetErrMessage(() => long.TryParse(newAdvertisement.Price, out price), "Price", "Цена задаётся неотрицательным числом.");
            newAdvertisementValid &= priceIsNumber;
            if (priceIsNumber)
            {
                newAdvertisementValid &= CheckRuleAndSetErrMessage(() => price >= 0, "Price", "Цена должна быть неотрицательна.");
                var maxPrice = Int32.MaxValue;
                newAdvertisementValid &= CheckRuleAndSetErrMessage(() => price < maxPrice, "Price", String.Format("Система не может обработать цены выше {0}", maxPrice));
            }

            newAdvertisementValid &= CheckRuleAndSetErrMessage(() => !String.IsNullOrWhiteSpace(newAdvertisement.Contacts), "Contacts", "Укажите способ забрать товар или связаться с вами.");

            return newAdvertisementValid;
        }
        public ActionResult Create()
        {
            log.Debug("Run Create()");
            return View(new CreateAdvertisementView());
        }

        ILog log = LogManager.GetLogger(typeof(AdvertisementsController));
    }
}
