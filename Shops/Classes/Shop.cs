using System.Collections.Generic;

namespace Shops.Classes
{
    public class Shop
    {
        public Shop(string shopName, int id, string address)
        {
            ShopName = shopName;
            ShopAttributes = new ShopAttributes(id, address);
            Products = new List<ProductWithAttributes>();
        }

        public string ShopName { get; }
        public ShopAttributes ShopAttributes { get; }
        public List<ProductWithAttributes> Products { get; }

        public void AddProduct(Product product, int productPrice, int productCount)
        {
            var productAttributes = new ProductAttributes(productPrice, productCount);
            var productWithAttributes = new ProductWithAttributes(product, productAttributes);
            Products.Add(productWithAttributes);
        }
    }
}