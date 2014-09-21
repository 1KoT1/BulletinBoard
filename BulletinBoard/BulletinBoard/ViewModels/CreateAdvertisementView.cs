using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BulletinBoard.ViewModels
{
    public class CreateAdvertisementView
    {

        [Required(ErrorMessage = "Введите заголовок объявления.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите текст объявления.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Укажите цену товара.")]
        public int Price { get; set; }
        
        [Required(ErrorMessage = "Укажите свои контактые данные.")]
        public string Contacts { get; set; }
    }
}