using BulletinBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoard.ViewModels
{
    public class AdvertisementsListPage
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public Sort Sort { get; set; }
        public AdvertisementsFilter Filter { get; set; }
        public AdvertisementsListPage(IEnumerable<Advertisement> advertisements, Sort sort, AdvertisementsFilter filter)
        {
            Advertisements = advertisements;
            Sort = sort;
            Filter = filter;
        }
    }
    public enum Sort
    {
        Name,
        Price,
        PublishDate
    }
}