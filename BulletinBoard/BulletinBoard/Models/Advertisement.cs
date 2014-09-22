﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulletinBoard.Models
{
    public class Advertisement
    {
        [Key]
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public DateTime PublishDate { get; set; }

        [Required]
        public virtual Contacts Contacts { get; set; }

        public Advertisement()
        { }
        public Advertisement(string name, string description, uint price, DateTime publishDate, Contacts contacts, int id = 0)
        {
            IdAdvertisement = id;
            Name = name;
            Description = description;
            PublishDate = publishDate;
            Contacts = contacts;
        }
    }
}