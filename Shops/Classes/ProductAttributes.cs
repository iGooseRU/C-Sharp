namespace Shops.Classes
{
    public class ProductAttributes
    {
        public ProductAttributes(int productPrice, int productAmount)
        {
            ProductPrice = productPrice;
            ProductAmount = productAmount;
        }

        public int ProductPrice { get; set; }
        public int ProductAmount { get; set; }
    }
}