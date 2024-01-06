using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Models
{
    public class DataBase
    {
        public List<BaseUser> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Purchase> PurchaseHistory { get; set; }
        public DataBase()
        {
            Users = new List<BaseUser>();
            Products = new List<Product>
            {
                new Product{Name = "М'яка іграшка", Quantity = 25, Price = 20 },
                new Product{Name = "Іграшковий автомобіль", Quantity = 50, Price = 10},
                new Product{Name = "Конструктор", Quantity = 10, Price = 100},
                new Product{Name = "Іграшковий робот на радіоуправлінні", Quantity = 15, Price = 75},
                new Product{Name = "Настільна гра", Quantity = 20, Price = 60}
            };
            PurchaseHistory = new List<Purchase>();
        }
    }

}