﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulletinBoard.Models
{
    public class Contacts
    {
        [Key]
        public int IdContacts { get; set; }
        public string Address { get; set; }
    }
}