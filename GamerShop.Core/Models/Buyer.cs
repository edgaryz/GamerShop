namespace GamerShop.Core.Models
{
    public class Buyer : User
    {
        List<Product> ProductsCart;
        public Buyer()
        {
            ProductsCart = new List<Product>();
        }

        public Buyer(string firstName, string lastName, string email, string phoneNumber) : base(firstName, lastName, email, phoneNumber)
        { }

        public Buyer(int id, string firstName, string lastName, string email, string phoneNumber) : base(id, firstName, lastName, email, phoneNumber)
        { }

        public async Task BuyProduct(Product product)
        {
            ProductsCart.Add(product);
        }
    }
}