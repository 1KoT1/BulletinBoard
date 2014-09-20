using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulletinBoard.Models
{
    public class Advertisement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Contacts Contacts { get; set; }
    }
}