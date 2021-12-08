using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class CreditAccount : IAccount
    {
        private const double StartMoneyCount = 0;

        public CreditAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Credit;

            var cnt = new Counter();
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = false;
            MoneyCount = StartMoneyCount;

            CurrentMonth = DateTime.Now.Month;
        }

        public string AccountId { get; set; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public double MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int CreditLimit { get; set; }
        public int CurrentMonth { get; set; }

        public void TopUpMoney(double moneyAmount)
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

        public void SkipMonth()
        {
            ++CurrentMonth;
            PercentHandler();
        }

        public void PercentHandler()
        {
            if (MoneyCount < 0)
            {
                MoneyCount -= (MoneyCount / 100) * ClientsBank.CreditPercentage;
            }
        }
    }
}
