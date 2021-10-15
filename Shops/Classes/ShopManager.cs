using System.Collections.Generic;
using Shops.Manager;
using Shops.Tools;

namespace Shops.Classes
{
    public class ShopManager : IShopManager
    {
        private int _productId = 100000;
        private int _shopId = 100000;
        public ShopManager()
        {
            Shops = new List<Shop>();
            RegisteredProduct = new List<Product>();
        }

        public List<Shop> Shops { get; }
        public List<Product> RegisteredProduct { get; }

        public Product RegisterProduct(string productName)
        {
            var product = new Product(productName, ++_productId);
            RegisteredProduct.Add(product);
            return product;
        }

        public Shop AddShop(string shopName, string address)
        {
            var shop = new Shop(shopName, ++_shopId, address);
            Shops.Add(shop);

            return shop;
        }

        public void AddProductToShop(Shop shop, Product product, int productPrice, int productAmount)
        {
            shop.AddProduct(product, productPrice, productAmount);
        }

        public void ChangeProductPrice(Shop shop, Product product, int newPrice)
        {
            ProductWithAttributes changingPriceProduct = FindProductAttributes(product.ProductName, shop);
            changingPriceProduct.ProductPrice = newPrice;
        }

        public ProductWithAttributes FindProductAttributes(string name, Shop shop)
        {
            foreach (ProductWithAttributes product in shop.Products)
            {
                if (product.ProductName == name)
                {
                    return product;
                }
            }

            return null;
        }

        public void BuyProduct(Shop shopName, Product product, Customer customer, int productToBuyAmount)
        {
            ProductWithAttributes productOnSale = FindProductAttributes(product.ProductName, shopName);
            if (productOnSale == null)
            {
                throw new ShopException("This product does not exist");
            }

            if (productToBuyAmount < productOnSale.ProductAmount)
            {
                int finalPrice = productOnSale.ProductPrice * productToBuyAmount;
                if (finalPrice <= customer.AmountOfMoney)
                {
                    productOnSale.ProductAmount -= productToBuyAmount;
                    customer.AmountOfMoney -= finalPrice;
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
                ProductWithAttributes foundProduct = FindProductAttributes(product.ProductName, shop);
                if (foundProduct == null)
                {
                    throw new ShopException("This product does not exist");
                }

                if (productToBuyAmount < foundProduct.ProductAmount)
                {
                    if (foundProduct.ProductPrice < maxProductPrice)
                    {
                        maxProductPrice = foundProduct.ProductPrice;
                        shopWithCheapestPrice = shop;
                    }
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