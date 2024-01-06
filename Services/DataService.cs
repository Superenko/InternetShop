using Kursova.Interfaces;
using Kursova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    public class DataService : IDataService
    {
        private DataBase _data;

        public DataService(DataBase data)
        {
            _data = data;
        }

        public DataBase GetData()
        {
            return _data;
        }

        public void AddUser(BaseUser user)
        {
            _data.Users.Add(user);
        }

        public void AddPurchase(Purchase purchase)
        {
            _data.PurchaseHistory.Add(purchase);
        }

        public List<Product> GetProducts()
        {
            return _data.Products;
        }
    }
}
