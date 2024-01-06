using Kursova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Interfaces
{
    public interface IDataService
    {
        void AddUser(BaseUser user);
        void AddPurchase(Purchase purchase);
        List<Product> GetProducts();
        DataBase GetData();

    }
}
