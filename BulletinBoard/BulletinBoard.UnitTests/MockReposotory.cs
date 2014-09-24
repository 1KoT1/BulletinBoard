using BulletinBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.UnitTests
{
    public class MockRepository : IRepository
    {
        public MockRepository()
        {
            advertisements.Add(new Advertisement("Объявление № 1",
                                                 "Описания объявления № 1.",
                                                 1800,
                                                 new DateTime(2014, 5, 27), 
                                                 new Contacts("т. 63467377337")));
            
            advertisements.Add(new Advertisement("Объявление № 2",
                                                 "Описания объявления № 2.",
                                                 350,
                                                 new DateTime(2014, 9, 4),
                                                 new Contacts("т. 5466544545")));

            advertisements.Add(new Advertisement("Объявление № 3",
                                                 "Описания объявления № 3.",
                                                 11000,
                                                 new DateTime(2014, 3, 20),
                                                 new Contacts("т. 73437735")));
        }

        private List<Advertisement> advertisements = new List<Advertisement>();
        public IEnumerable<Advertisement> Advertisements
        {
            get { return advertisements; }
        }

        public Advertisement CreateAdvertisement(Advertisement advertisement)
        {
            if (advertisement.IdAdvertisement != 0)
            {
                throw new CreatedObjectIsNotEmptyException();
            }

            advertisement.IdAdvertisement = advertisements.Count;
            advertisements.Add(advertisement);
            return advertisement;
        }

        public void RemoveAdvertisementAndSaveAllChanges(Advertisement advertisement)
        {
            if (Advertisements.All(adv => adv.IdAdvertisement != advertisement.IdAdvertisement))
            {
                throw new RepositoryHasNotThisItemException();
            }

            advertisements.Remove(advertisement);
        }

        public void SaveAllChanges()
        {
        }
    }
}
