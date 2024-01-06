using Kursova.Interfaces;
using Kursova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Services
{
    public class UserService : IUserService
    {
        private DataBase _data { get; }

        public UserService(DataBase data)
        {
            _data = data;
        }

        public BaseUser GetUserByName(string username)
        {
            return _data.Users.First(x => x.Name == username);
        }

        public List<Purchase> GetUserPurchases(string userName)
        {
            return _data.PurchaseHistory.Where(x => x.CustomerName == userName).ToList();
        }

        public bool IsUserCreated(string userName)
        {
            return _data.Users.Any(x => x.Name == userName);
        }
    }
}
