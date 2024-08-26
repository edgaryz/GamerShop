using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerShop.Core.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

        public Order() { }

        public Order(Product product, User user, DateTime orderDate, int quantity)
        {
            Product = product;
            User = user;
            OrderDate = orderDate;
            Quantity = quantity;
        }

        public Order(int orderId, Product product, User user, DateTime orderDate, int quantity)
        {
            OrderId = orderId;
            Product = product;
            User = user;
            OrderDate = orderDate;
            Quantity = quantity;
        }

        public override string ToString()
        {
            string userType = "";
            userType = User.GetType().Name;
            if (userType == "Buyer")
            {
                return $"Order ID: {OrderId} - {User.FirstName} {User.LastName} bought {Product.Id} {Product.ProductName} {Quantity} copies.";
            }
            else
            {
                return $"Order ID: {OrderId} - {User.FirstName} {User.LastName} sold {Product.Id} {Product.ProductName} {Quantity} copies.";
            }
        }
    }
}