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

            var cnt = Counter.Source;
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = false;
            MoneyCount = StartMoneyCount;

            CurrentMonth = DateTime.Now.Month;
        }

        public string AccountId { get; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public double MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int CreditLimit { get; set; }
        public int CurrentMonth { get; set; }

        public CreditAccount GetThisAccount()
        {
            return this;
        }

        public double GetMoneyAmount()
        {
            return MoneyCount;
        }

        public bool GetAccountStatus()
        {
            return WithdrawAvailable;
        }

        public void TopUpMoney(double moneyAmount)
        {
            if (moneyAmount <= 0)
                throw new BanksException("Money amount < 0");

            MoneyCount += moneyAmount;
        }

        public void MoneyWithdraw(int moneyAmount)
        {
            throw new BanksException("You can't withdraw money from credit account!");
        }

        public void SkipMonth()
        {
            ++CurrentMonth;
            PercentHandler();
        }

        public string GetAccountId()
        {
            return AccountId;
        }

        public void GetAccountInfo()
        {
            Console.WriteLine("Account Id: " + AccountId + ' ' + "Money amount: " + MoneyCount);
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
