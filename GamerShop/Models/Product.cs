using GamerShop.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamerShop.Core.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductType ProductType { get; set; }
        public int CountInStorage { get; set; }
        public Product() { }

        public Product(string productName, decimal price, ProductType productType, int countInStorage)
        {
            ProductName = productName;
            Price = price;
            ProductType = productType;
            CountInStorage = countInStorage;
        }

        public Product(int id, string productName, decimal price, ProductType productType, int countInStorage)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            ProductType = productType;
            CountInStorage = countInStorage;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Product: {ProductType} - {ProductName}, Price: {Price}, Count in storage: {CountInStorage}";
        }
    }
}
