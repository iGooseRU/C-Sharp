namespace Shops.Classes
{
    public class ShopAttributes
    {
        public ShopAttributes(int id, string address)
        {
            Id = id;
            Address = address;
        }

        public int Id { get; }
        public string Address { get; }
    }
}