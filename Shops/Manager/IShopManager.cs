using Shops.Classes;

namespace Shops.Manager
{
    public interface IShopManager
    {
        Shop AddShop(string shopName, string address);
        Product RegisterProduct(string productName);
        void AddProductToShop(Shop shop, Product product, int productPrice, int productAmount);
        void ChangeProductPrice(Shop shop, Product product, int newPrice);
        void BuyProduct(Shop shopName, Product product, Customer customer, int productToBuyAmount);
        Shop FindShopWithCheapestPriceForLot(Product product, int productToBuyAmount);
    }
}