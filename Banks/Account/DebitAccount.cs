using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class DebitAccount : IAccount
    {
        private const int StartMoneyCount = 0;
        public DebitAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Debit;

            var cnt = new Counter();
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = true;
            MoneyCount = StartMoneyCount;
            ExtraMoney = StartMoneyCount;

            SettlementPeriod = DateTime.Now.Date;

            CalculatePercents();
        }

        public string AccountId { get; set; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public int MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int ExtraMoney { get; set; }
        public DateTime SettlementPeriod { get; set; }

        public void CreateAccount()
        {
            Client.DebitAccounts.Add(this);
            Client.ClientsBank.DebitAccounts.Add(this);
        }

        public void TopUpMoney(int moneyAmount)
        {
            if (moneyAmount <= 0)
                throw new BanksException("Money amount < 0");

            MoneyCount += moneyAmount;
        }

        public void MoneyWithdraw(int moneyAmount)
        {
            if (moneyAmount >= MoneyCount)
                throw new BanksException("Not enough money");

            MoneyCount -= moneyAmount;
        }

        public void CalculatePercents()
        {
            while (SettlementPeriod == DateTime.Now.Date)
            {
                ExtraMoney = (MoneyCount / 100) * ClientsBank.PercentageOnBalance;
                MoneyCount += ExtraMoney;
                break;
            }
        }
    }
}