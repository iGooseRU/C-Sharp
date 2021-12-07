using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class CreditAccount : IAccount
    {
        private const int StartMoneyCount = 0;

        public CreditAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Credit;

            var cnt = new Counter();
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = false;
            MoneyCount = StartMoneyCount;
        }

        public string AccountId { get; set; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public int MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int CreditLimit { get; set; }

        public void TopUpMoney(int moneyAmount)
        {
            if (moneyAmount <= 0)
                throw new BanksException("Money amount < 0");

            MoneyCount += moneyAmount;
        }

        public void CreateAccount()
        {
            Client.CreditAccounts.Add(this);
            Client.ClientsBank.CreditAccounts.Add(this);
        }
    }
}
