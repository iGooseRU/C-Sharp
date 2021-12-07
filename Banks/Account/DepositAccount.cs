using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class DepositAccount : IAccount
    {
        private const int StartMoneyCount = 0;

        public DepositAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Deposit;

            var cnt = new Counter();
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = false;
            MoneyCount = StartMoneyCount;
            ExtraMoney = StartMoneyCount;
        }

        public string AccountId { get; set; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public int MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int ExtraMoney { get; set; }
        public DateTime DepositTerm { get; set; }

        public void CreateAccount()
        {
            Client.DepositAccounts.Add(this);
            Client.ClientsBank.DepositAccounts.Add(this);
        }

        public void TopUpMoney(int moneyAmount)
        {
            if (moneyAmount <= 0)
                throw new BanksException("Money amount < 0");

            MoneyCount += moneyAmount;
        }

        public void MoneyWithdraw(int moneyAmount)
        {
            // add check logic
            if (!WithdrawAvailable)
                throw new BanksException("You can not to withdraw money before the end of the term");

            if (moneyAmount >= MoneyCount)
                    throw new BanksException("Not enough money");

            MoneyCount -= moneyAmount;
        }
    }
}