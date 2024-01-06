using Kursova.Interfaces;
using Kursova.Models;
using Kursova.Services;
using Kursova.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShop1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<BaseUser> users = Serializer.DeserializeUsers();
            DataBase data = new DataBase
            {
                Users = users
            }; 
            IDataService dataService = new DataService(data);
            IUserService userService = new UserService(data);

            Console.WriteLine("=== Welcome to the Internet Shop ===");


            BaseUser currentUser = LoginOrRegister(dataService, userService);


            while (true)
            {
                Console.WriteLine("\n=== Main Menu ===");
                Console.WriteLine("1. Display Products");
                Console.WriteLine("2. View Purchase History");
                Console.WriteLine("3. Add Money to Balance");
                Console.WriteLine("4. Make a Purchase");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayProducts(dataService.GetProducts());
                        break;
                    case "2":
                        ViewPurchaseHistory(currentUser, data);
                        break;
                    case "3":
                        AddMoneyToBalance(currentUser, dataService);
                        break;
                    case "4":
                        MakePurchase(currentUser, dataService);
                        break;
                    case "5":
                        Serializer.SerializeUsers(data.Users); 
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static BaseUser LoginOrRegister(IDataService dataService, IUserService userService)
        {
            while (true)
            {
                Console.WriteLine("\n=== Увійти чи Зареєструватися ===");
                Console.WriteLine("1. Увійти");
                Console.WriteLine("2. Зареєструватися");
                Console.Write("Введіть свій вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BaseUser loggedInUser = Login(userService);
                        if (loggedInUser != null)
                        {
                            return loggedInUser;
                        }
                        break;
                    case "2":
                        return Register(dataService);
                    default:
                        Console.WriteLine("Невірний вибір. Будь ласка, спробуйте ще раз.");
                        break;
                }
            }
        }

        static BaseUser Login(IUserService userService)
        {
            Console.Write("Введіть ваше ім'я користувача: ");
            string username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Ім'я користувача не може бути порожнім. Будь ласка, спробуйте ще раз.");
                return null;
            }

            Console.Write("Введіть ваш пароль: ");
            string password = GetHiddenInput();
            Console.WriteLine(); 

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Пароль не може бути порожнім. Будь ласка, спробуйте ще раз.");
                return null;
            }

            
            Console.WriteLine($"Введений пароль: {password}");

            try
            {
                BaseUser user = userService.GetUserByName(username);

                if (user != null)
                {
                    

                    if (Hasher.CompareHashValues(user.Password, password))
                    {
                        Console.WriteLine("Успішний вхід!");
                        return user; 
                    }
                    else
                    {
                        Console.WriteLine("Невірне ім'я користувача чи пароль! Будь ласка, спробуйте ще раз.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("Користувача не знайдено. Будь ласка, спробуйте ще раз.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка отримання інформації про користувача: {ex.Message}");
                return null;
            }
        }


        private static string GetHiddenInput()
{
    StringBuilder input = new StringBuilder();
    while (true)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Enter)
        {
            break;
        }
        else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
        {
            input.Remove(input.Length - 1, 1);
            Console.Write("\b \b"); 
        }
        else if (!char.IsControl(key.KeyChar))
        {
            input.Append(key.KeyChar);
            Console.Write("*"); 
        }
    }
    return input.ToString();
}



        static BaseUser LoggedInUser;


        static BaseUser Register(IDataService dataService)
        {
            Console.Write("Enter a new username: ");
            string username = Console.ReadLine();
            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            User newUser = new User(username, Hasher.HashSHA256(password));
            dataService.AddUser(newUser);
            Serializer.SerializeUsers(dataService.GetData().Users);

            Console.WriteLine($"Registration successful! Welcome, {newUser.Name}!");
            return newUser;
        }

        static void DisplayProducts(List<Product> products)
        {
            Console.WriteLine("\n=== Products ===");
            foreach (Product product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price}, Available: {product.IsAvailable}");
            }
        }

        static void ViewPurchaseHistory(BaseUser user, DataBase data)
        {
            List<Purchase> userPurchases = data.PurchaseHistory.Where(p => p.CustomerName == user.Name).ToList();

            Console.WriteLine("\n=== Purchase History ===");
            foreach (Purchase purchase in userPurchases)
            {
                Console.WriteLine($"ID: {purchase.PurchaseID}, Product: {purchase.ProductName}, Quantity: {purchase.ProductQuantity}, Price: {purchase.Price}");
            }
        }

        static void AddMoneyToBalance(BaseUser user, IDataService dataService)
        {
            Console.Write("Enter the amount to add to your balance: ");

            if (int.TryParse(Console.ReadLine(), out int amount))
            {
                user.MakeDeposit(amount);
                dataService.AddUser(user);

                Serializer.SerializeUsers(dataService.GetData().Users);

                Console.WriteLine($"Balance updated. New balance: {user.Balance}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid amount.");
            }
        }

        static void MakePurchase(BaseUser user, IDataService dataService)
        {
            DisplayProducts(dataService.GetProducts());

            Console.Write("Enter the name of the product you want to purchase: ");
            string productName = Console.ReadLine();

            Console.Write("Enter the quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                Product selectedProduct = dataService.GetProducts().FirstOrDefault(p => p.Name == productName);

                if (selectedProduct != null && selectedProduct.IsAvailable && quantity <= selectedProduct.Quantity)
                {
                    int totalPrice = selectedProduct.Price * quantity;

                    if (user.Balance >= totalPrice)
                    {
                        user.MakePurchase(selectedProduct, quantity);
                        dataService.AddPurchase(new Purchase(user.Name, productName, quantity, totalPrice));
                        dataService.AddUser(user);

                        Console.WriteLine("Purchase successful!");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds. Please add money to your balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid product or quantity. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid quantity.");
            }
        }
    }
}