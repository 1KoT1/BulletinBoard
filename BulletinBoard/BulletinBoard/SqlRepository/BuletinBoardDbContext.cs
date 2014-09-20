using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BulletinBoard.Models;

namespace BulletinBoard.SqlRepository
{
    public class BuletinBoardDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
    }
}