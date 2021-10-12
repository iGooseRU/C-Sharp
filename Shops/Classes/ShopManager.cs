using System.Collections.Generic;
using Shops.Manager;
using Shops.Tools;

namespace Shops.Classes
{
    public class ShopManager : IShopManager
    {
        private int _vendorCode = 100000;
        public ShopManager()
        {
            Shops = new List<Shop>();
            RegisteredProduct = new List<Product>();
        }

        public List<Shop> Shops { get; }
        public List<Product> RegisteredProduct { get; }

        public Product RegisterProduct(string productName)
        {
            var product = new Product(productName, ++_vendorCode);
            RegisteredProduct.Add(product);
            return product;
        }

        public Shop AddShop(string shopName, int id, string address)
        {
            var shop = new Shop(shopName, id, address);
            Shops.Add(shop);

            return shop;
        }

        public void AddProductToShop(Shop shop, Product product, int productPrice, int productAmount)
        {
            shop.AddProduct(product, productPrice, productAmount);
        }

        public void ChangeProductPrice(Shop shop, Product product, int newPrice)
        {
            ProductAttributes changingPriceProduct = FindProductAttributes(product.ProductName, shop);
            changingPriceProduct.ProductPrice = newPrice;
        }

        public ProductAttributes FindProductAttributes(string name, Shop shop)
        {
            foreach ((Product product, ProductAttributes foundProduct) in shop.Products)
            {
                if (product.ProductName == name)
                {
                    return foundProduct;
                }
            }

            return null;
        }

        public void BuyProduct(Shop shopName, Product product, Customer customer, int productToBuyAmount)
        {
            ProductAttributes productOnSale = FindProductAttributes(product.ProductName, shopName);
            if (productOnSale == null)
            {
                throw new ShopException("This product does not exist");
            }

            if (productToBuyAmount < productOnSale.ProductAmount)
            {
                if (productOnSale.ProductPrice * productToBuyAmount < customer.AmountOfMoney)
                {
                    productOnSale.ProductAmount -= productToBuyAmount;
                    customer.AmountOfMoney -= productOnSale.ProductPrice * productToBuyAmount;
                }
                else
                {
                    throw new ShopException("Not enough money to buy product");
                }
            }
            else
            {
                throw new ShopException("Not enough products");
            }
        }

        public Shop FindShopWithCheapestPriceForLot(Product product, int productToBuyAmount)
        {
            int maxProductPrice = int.MaxValue;
            Shop shopWithCheapestPrice = null;
            foreach (Shop shop in Shops)
            {
                ProductAttributes foundProduct = FindProductAttributes(product.ProductName, shop);
                if (foundProduct == null)
                {
                    throw new ShopException("This product does not exist");
                }

                if (productToBuyAmount < foundProduct.ProductAmount)
                {
                    if (foundProduct.ProductPrice >= maxProductPrice) continue;
                    maxProductPrice = foundProduct.ProductPrice;
                    shopWithCheapestPrice = shop;
                }
                else
                {
                    throw new ShopException("Not enough products");
                }
            }

            if (shopWithCheapestPrice == null)
            {
                throw new ShopException("Error. Cannot find a shop");
            }

            return shopWithCheapestPrice;
        }
    }
}