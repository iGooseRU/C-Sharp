using System.Collections.Generic;

namespace Shops.Classes
{
    public class Shop
    {
        public Shop(string shopName, int id, string address)
        {
            ShopName = shopName;
            Id = id;
            Address = address;
            Products = new List<(Product, ProductAttributes)>();
        }

        public string ShopName { get; }
        public int Id { get; }
        public string Address { get; }
        public List<(Product, ProductAttributes)> Products { get; }

        public void AddProduct(Product product, int productPrice, int productCount)
        {
            var productAttributes = new ProductAttributes(productPrice, productCount);
            Products.Add((product, productAttributes));
        }
    }
}