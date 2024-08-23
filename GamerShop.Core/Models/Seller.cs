namespace GamerShop.Core.Models
{
    public class Seller : User
    {
        List<Product> ProductsForSale;
        public Seller()
        {
            ProductsForSale = new List<Product>();
        }

        public Seller(string firstName, string lastName, string email, string phoneNumber) : base(firstName, lastName, email, phoneNumber)
        { }

        public Seller(int id, string firstName, string lastName, string email, string phoneNumber) : base(id, firstName, lastName, email, phoneNumber)
        { }

        public async Task SellProduct(Product product)
        {
            ProductsForSale.Remove(product);
        }
    }
}