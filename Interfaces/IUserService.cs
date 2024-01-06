using Kursova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Interfaces
{
    public interface IUserService
    {
        BaseUser GetUserByName(string username);
        List<Purchase> GetUserPurchases(string username);
        bool IsUserCreated(string name);
    }
}
