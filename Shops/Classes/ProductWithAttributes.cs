namespace Shops.Classes
{
    public class ProductWithAttributes
    {
        public ProductWithAttributes(Product product, ProductAttributes productAttributes)
        {
            ProductName = product.ProductName;
            ProductId = product.ProductId;
            ProductPrice = productAttributes.ProductPrice;
            ProductAmount = productAttributes.ProductAmount;
        }

        public string ProductName { get; }
        public int ProductId { get; }
        public int ProductPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}