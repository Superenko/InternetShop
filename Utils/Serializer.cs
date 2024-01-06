using Kursova.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Kursova.Utils
{
    public static class Serializer
    {
        private static readonly DataBase s_data = new();

        public static void SerializeUsers(List<BaseUser> users)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Newtonsoft.Json.Formatting.Indented };
            File.WriteAllText("accountData.json", JsonConvert.SerializeObject(users, settings));
        }

        public static List<BaseUser> DeserializeUsers()
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Newtonsoft.Json.Formatting.Indented };
            return File.Exists("accountData.json") ? JsonConvert.DeserializeObject<List<BaseUser>>(File.ReadAllText("accountData.json"), settings) : s_data.Users;

        }

        public static void SerializePurchases(List<Purchase> purchases)
        {
            File.WriteAllText("purchaseData.json", JsonConvert.SerializeObject(purchases, Newtonsoft.Json.Formatting.Indented));

        }

        public static List<Purchase> DeserializePurchases()
        {
            return File.Exists("purchaseData.json") ? JsonConvert.DeserializeObject<List<Purchase>>(File.ReadAllText("purchaseData.json")) : s_data.PurchaseHistory;
        }

        public static void SerializeProducts(List<Product> products)
        {
            File.WriteAllText("productsData.json", JsonConvert.SerializeObject(products, Newtonsoft.Json.Formatting.Indented));
        }

        public static List<Product> DeserializeProducts()
        {
            return File.Exists("productsData.json") ? JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText("productsData.json")) : s_data.Products;
        }
    }
}
