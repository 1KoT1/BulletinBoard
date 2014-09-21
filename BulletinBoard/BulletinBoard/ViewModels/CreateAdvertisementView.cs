using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoard.ViewModels
{
    public class CreateAdvertisementView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Contacts { get; set; }
    }
}