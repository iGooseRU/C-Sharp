namespace Shops.Classes
{
    public class Customer
    {
        public Customer(string name, int amountOfMoney)
        {
            Name = name;
            AmountOfMoney = amountOfMoney;
        }

        public string Name { get; }
        public int AmountOfMoney { get; set; }
    }
}