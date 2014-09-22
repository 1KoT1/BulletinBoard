using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoard.ViewModels
{
    public class AdvertisementsFilter
    {
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; }

        public AdvertisementsFilter(uint minPrice = 0, uint maxPrice = Int32.MaxValue)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}