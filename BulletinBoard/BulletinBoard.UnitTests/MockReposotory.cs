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
