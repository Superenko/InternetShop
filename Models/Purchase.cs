namespace Kursova.Models
{
    public class Purchase
    {
        private static int s_idGenerator = 0;

        public int PurchaseID { get; }
        public string CustomerName { get; }
        public string ProductName { get; }
        public int ProductQuantity { get; }
        public int Price { get; }

        public Purchase(string customerName, string productName, int productQuantity, int price)
        {
            PurchaseID = ++s_idGenerator;
            CustomerName = customerName;
            ProductName = productName;
            ProductQuantity = productQuantity;
            Price = price;
        }
    }
}
