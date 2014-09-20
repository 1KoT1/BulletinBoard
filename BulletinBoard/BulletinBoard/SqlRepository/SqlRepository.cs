using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BulletinBoard.Models;

namespace BulletinBoard.SqlRepository
{
    public class SqlRepository : IRepository
    {
        BuletinBoardDbContext db = new BuletinBoardDbContext();
        public IEnumerable<Advertisement> Advertisements
        {
            get { return db.Advertisements; }
        }

        public Advertisement CreateAdvertisement(Advertisement advertisement)
        {
            if (advertisement.IdAdvertisement != 0)
            {
                throw new CreatedObjectIsNotEmptyException();
            }

            db.Advertisements.Add(advertisement);
            db.SaveChanges();
            return advertisement;
        }

        public void RemoveAdvertisement(Advertisement advertisement)
        {
            if (Advertisements.All(adv => adv.IdAdvertisement != advertisement.IdAdvertisement))
            {
                throw new RepositoryHasNotThisItemException();
            }

            db.Advertisements.Remove(advertisement);
            db.SaveChanges();
        }

        public void UpdateAdvertisement(Advertisement advertisement)
        {
            if (Advertisements.All(adv => adv.IdAdvertisement != advertisement.IdAdvertisement))
            {
                throw new RepositoryHasNotThisItemException();
            }

            db.SaveChanges();
        }
    }
}