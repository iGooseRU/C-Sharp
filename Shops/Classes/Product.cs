namespace Shops.Classes
{
    public class Product
    {
        public Product(string productName, int productId)
        {
            ProductName = productName;
            ProductId = productId;
        }

        public string ProductName { get; }
        public int ProductId { get; }
    }
}