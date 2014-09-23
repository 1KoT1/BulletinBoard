using BulletinBoard.Controllers;
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
            [Values(0, uint.MaxValue, 5)]uint minPrice,
            [Values(0, uint.MaxValue, 4537)]uint maxPrice)
        {
            var controller = new AdvertisementsController();

            var result = controller.List(sort, minPrice, maxPrice);

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<AdvertisementsListPage>((result as ViewResult).Model);
        }
    }
}
