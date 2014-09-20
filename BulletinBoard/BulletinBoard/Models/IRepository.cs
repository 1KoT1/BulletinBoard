using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Models
{
    interface IRepository
    {
        IEnumerable<Advertisement> Advertisements { get; }

        /// <summary> Создайте объект Advertisement или его наследника и передайте в данный метод,
        /// чтобы сохранить его в хранилище.
        /// Свойство Id создаваемого объекта должно быть равно  0.
        /// Метод вернёт ссылку на переданный объект, заполнив его свойство Id.
        /// </summary>
        /// <param name="menuitem">Создаваемый объект.</param>
        /// <returns>Ссылка на переданный объект с заполненым свойством Id</returns>
        /// <exception cref="CreatedObjectIsNotEmptyException">Свойство Id переданного объекта не равно 0.</exception>
        Advertisement CreateAdvertisement(Advertisement advertisement);

        /// <summary> Удаляет указанный объект из хранилища.
        /// </summary>
        /// <param name="menuItem">Ссылка на объект в хранилище, который следует удалить</param>
        /// <exception cref="RepositoryHasNotThisItemException">Указанный объект не содержится в хранилище</exception>
        void RemoveAdvertisementAndSaveAllChanges(Advertisement advertisement);
        void SaveAllChanges();
    }
        
    class CreatedObjectIsNotEmptyException : Exception
    {
        public CreatedObjectIsNotEmptyException() :
            base("Свойство Id создаваемого элемента должно быть равно  0.")
        { }
    }

    class RepositoryHasNotThisItemException : Exception
    {
        public RepositoryHasNotThisItemException() :
            base("Данный элемент не содержится в хранилище. Вероятно объект данных создавался минуя предназначенный для этого метод хранилища")
        { }
    }
}
