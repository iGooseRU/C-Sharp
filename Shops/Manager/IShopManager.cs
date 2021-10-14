using Shops.Classes;

namespace Shops.Manager
{
    public interface IShopManager
    {
        public Shop AddShop(string shopName, string address);
        public Product RegisterProduct(string productName);
        public void AddProductToShop(Shop shop, Product product, int productPrice, int productAmount);
        public void ChangeProductPrice(Shop shop, Product product, int newPrice);
        public void BuyProduct(Shop shopName, Product product, Customer customer, int productToBuyAmount);
        public Shop FindShopWithCheapestPriceForLot(Product product, int productToBuyAmount);
    }
}