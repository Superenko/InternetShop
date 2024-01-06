using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursova.Models
{
    public class VipUser : BaseUser
    {
        public new float Balance { get; set; }
        private float _discount = 0.9F;
        public VipUser(string name, string password) : base(name, password) { }
        public override void MakePurchase(Product product, int quantity)
        {
            product.Quantity -= quantity;
            float finalPrice = product.Price * quantity * _discount;
            Balance -= finalPrice;
        }
    }

}
