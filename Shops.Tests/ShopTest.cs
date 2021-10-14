using NUnit.Framework;
using Shops.Classes;

namespace Shops.Tests
{
    public class Tests
    {
        private ShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }
        
        [Test]
        public void CreateShopAddProductToSystemTransferProductFromSystemToShopTest()
        {
            Shop testShop = _shopManager.AddShop("Dixy", "Kronversky 23");
            Product testProduct = _shopManager.RegisterProduct("Coca-Cola");
            _shopManager.AddProductToShop(testShop, testProduct, 55, 20);
            ProductAttributes foundProduct = _shopManager.FindProductAttributes(testProduct.ProductName, testShop);
            Assert.AreEqual(foundProduct.ProductAmount, 20);
        }

        [Test]
        public void ChangePriceOfProductInTheShopTest()
        {
            const int amountOfProducts = 120;
            const int newPrice = 45;
            Shop testShop = _shopManager.AddShop("Dixy", "Kronversky 23");
            Product testProduct = _shopManager.RegisterProduct("Twix");
            _shopManager.AddProductToShop(testShop,testProduct,40,amountOfProducts);
            _shopManager.ChangeProductPrice(testShop,testProduct,newPrice);
            ProductAttributes foundProduct = _shopManager.FindProductAttributes(testProduct.ProductName, testShop);
            Assert.AreEqual(foundProduct.ProductPrice, newPrice);
        }

        [Test]
        public void BuySingleProductTest()
        {
            const int moneyBeforeBuying = 150;
            const int amountOfProducts = 120;
            const int productToBuyCount = 1;
            Shop testShop = _shopManager.AddShop("Dixy", "Kronversky 23");
            Product testProduct = _shopManager.RegisterProduct("Twix");
            _shopManager.AddProductToShop(testShop,testProduct,40,amountOfProducts);
            var testCustomer = new Customer("Egor", moneyBeforeBuying);
            _shopManager.BuyProduct(testShop,testProduct,testCustomer,productToBuyCount);
            Assert.AreEqual(testCustomer.AmountOfMoney, 110);
        }

        [Test]
        public void FindShopWithCheapestPriceForALotTest()
        {
            Shop shop1 = _shopManager.AddShop("FamilyShop", "Leninsky 11");
            Shop shop2 = _shopManager.AddShop("Dixy", "Kronversky 23");
            Shop shop3 = _shopManager.AddShop("GroceryShop", "Nevsky 44");
            Product testProduct = _shopManager.RegisterProduct("Sprite");
            _shopManager.AddProductToShop(shop1,testProduct,100,100);
            _shopManager.AddProductToShop(shop2,testProduct,120,100);
            _shopManager.AddProductToShop(shop3,testProduct,90,100);
            Shop foundShop = _shopManager.FindShopWithCheapestPriceForLot(testProduct, 10);
            ProductAttributes foundProduct = _shopManager.FindProductAttributes(testProduct.ProductName, foundShop);
            Assert.AreEqual(foundProduct.ProductPrice, 90);
        }

        [Test]
        public void BuyALotOfProducts()
        {
            const int moneyBeforeBuying = 10000;
            const int amountOfProductsBeforeBuying = 120;
            const int productToBuyAmount = 100;
            Shop testShop = _shopManager.AddShop("Dixy", "Kronversky 23");
            Product testProduct = _shopManager.RegisterProduct("Twix");
            _shopManager.AddProductToShop(testShop,testProduct,40,amountOfProductsBeforeBuying);
            var testCustomer = new Customer("Egor", moneyBeforeBuying);
            ProductAttributes buyingProduct = _shopManager.FindProductAttributes(testProduct.ProductName, testShop);
            _shopManager.BuyProduct(testShop,testProduct,testCustomer,productToBuyAmount);
            Assert.AreEqual(testCustomer.AmountOfMoney, moneyBeforeBuying - buyingProduct.ProductPrice * productToBuyAmount);   // check amount of money
            Assert.AreEqual(buyingProduct.ProductAmount, amountOfProductsBeforeBuying - productToBuyAmount);                    // check amount of product in the shop
        }
        
    }
}