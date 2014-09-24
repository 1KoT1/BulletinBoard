using BulletinBoard.Controllers;
using BulletinBoard.Models;
using BulletinBoard.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulletinBoard.UnitTests
{
    [TestFixture]
    public class AdvertisementsControllerTest
    {
        [Test]
        public void List_GetView_ItsOkViewModelIsAdvertisementsListPagew(
            [Values( Sort.Name, Sort.Price, Sort.PublishDate)]Sort sort,
            [Values((uint)0, uint.MaxValue, (uint)3)]uint minPrice,
            [Values((uint)0, uint.MaxValue, (uint)6437)]uint maxPrice)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort, minPrice, maxPrice);

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<AdvertisementsListPage>((result as ViewResult).Model);
        }

        [Test]
        public void List_GetView_ItsOkViewContanesListOfAdvertisements(
            [Values(Sort.Name, Sort.Price, Sort.PublishDate)]Sort sort)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            if (!advertisements.Any())
            {
                Assert.Fail("Действаие List должно вернуть список объявлений.");
            }
        }

        [Test]
        public void List_GetViewDefault_ItsOkViewContanesListOfAdvertisementsOrderedByPublishDate()
        {
            var controller = new AdvertisementsController();

            var result = controller.List();

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            Advertisement backAdvertisment = null;
            foreach (var adv in advertisements)
            {
                if(backAdvertisment != null && adv.PublishDate < backAdvertisment.PublishDate)
                {
                    Assert.Fail("Список объявлений должен быть отсортирован по дате публикации.");
                }
                backAdvertisment = adv;
            }
        }

        [Test]
        public void List_GetViewOrderedByPublishDate_ItsOkViewContanesListOfAdvertisementsOrderedByPublishDate(
            [Values(Sort.PublishDate)]Sort sort
            )
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            Advertisement backAdvertisment = null;
            foreach (var adv in advertisements)
            {
                if (backAdvertisment != null && adv.PublishDate < backAdvertisment.PublishDate)
                {
                    Assert.Fail("Список объявлений должен быть отсортирован по дате публикации.");
                }
                backAdvertisment = adv;
            }
        }

        [Test]
        public void List_GetViewOrderedByName_ItsOkViewContanesListOfAdvertisementsOrderedBName(
            [Values(Sort.Name)]Sort sort
            )
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            Advertisement backAdvertisment = null;
            foreach (var adv in advertisements)
            {
                if (backAdvertisment != null && adv.Name.CompareTo(backAdvertisment.Name) == -1)
                {
                    Assert.Fail("Список объявлений должен быть отсортирован по заголовку.");
                }
                backAdvertisment = adv;
            }
        }

        [Test]
        public void List_GetViewOrderedByPrice_ItsOkViewContanesListOfAdvertisementsOrderedByPrice(
            [Values(Sort.Price)]Sort sort
            )
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            Advertisement backAdvertisment = null;
            foreach (var adv in advertisements)
            {
                if (backAdvertisment != null && adv.Price < backAdvertisment.Price)
                {
                    Assert.Fail("Список объявлений должен быть отсортирован по цене.");
                }
                backAdvertisment = adv;
            }
        }

        [Test, Sequential]
        public void List_GetViewFilteredByPrice_ItsOkViewContanesListOfAdvertisementsFilteredByPrice(
            [Values(Sort.Name,     Sort.Price, Sort.PublishDate, Sort.Name)]Sort sort,
            [Values((uint)0,       (uint)0,    (uint)635,        (uint)635)]uint minPrice,
            [Values(uint.MaxValue, (uint)9065, uint.MaxValue,    (uint)9065)]uint maxPrice)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort, minPrice, maxPrice);
            
            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            if (advertisements.Any(adv => adv.Price < minPrice))
            {
                Assert.Fail("Цены отфильтрованых объявлений должны быть болше минимума.");
            }
            if (advertisements.Any(adv => adv.Price > maxPrice))
            {
                Assert.Fail("Цены отфильтрованых объявлений должны быть меньше максимума.");
            }
        }

        [Test, Sequential]
        public void List_GetViewFilteredByPrice_ItsOkViewContanesEmptyListOfAdvertisements(
            [Values(Sort.Name, Sort.Price, Sort.PublishDate, Sort.Name,     Sort.Name)]Sort sort,
            [Values((uint)0,   (uint)635,  (uint)444,        uint.MaxValue, uint.MaxValue)]uint minPrice,
            [Values((uint)0,   (uint)134,  (uint)0,          uint.MinValue, uint.MaxValue)]uint maxPrice)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort, minPrice, maxPrice);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            if (advertisements.Any())
            {
                Assert.Fail("Отфильтрованный список должен быть пустым.");
            }
        }

        [Test]
        public void Lisr_GetViewFilterByPriceAndOrderedByName_ItsOkViewContanesListOfAdvertisementsFilteredByPriceAndOrderedByName(
            [Values(Sort.Name)]Sort sort,
            [Values((uint)252)]uint minPrice,
            [Values((uint)1515)]uint maxPrice)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort, minPrice, maxPrice);

            var advertisements = ((result as ViewResult).Model as AdvertisementsListPage).Advertisements;
            if (advertisements.Any(adv => adv.Price < minPrice))
            {
                Assert.Fail("Цены отфильтрованых объявлений должны быть болше минимума.");
            }
            if (advertisements.Any(adv => adv.Price > maxPrice))
            {
                Assert.Fail("Цены отфильтрованых объявлений должны быть меньше максимума.");
            }
            Advertisement backAdvertisment = null;
            foreach (var adv in advertisements)
            {
                if (backAdvertisment != null && adv.Name.CompareTo(backAdvertisment.Name) == -1)
                {
                    Assert.Fail("Список объявлений должен быть отсортирован по заголовку.");
                }
                backAdvertisment = adv;
            }
        }

        [Test]
        public void Create_GetView_ItsOkViewModelIsCreateAdvertisementView()
        {
            var controller = new AdvertisementsController();

            var result = controller.Create();

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<CreateAdvertisementView>((result as ViewResult).Model);
        }
    }
}
