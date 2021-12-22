using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class DepositAccount : IAccount
    {
        private const double StartMoneyCount = 0;

        public DepositAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Deposit;

            var cnt = Counter.Source;
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = false;
            MoneyCount = StartMoneyCount;
            CurrentMonth = DateTime.Now.Month;
        }

        public string AccountId { get; }
        public bool WithdrawAvailable { get; set; }
        public BankClient Client { get; }
        public double MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public DateTime DepositTerm { get; set; }
        public int CurrentMonth { get; set; }

        public void MakeOperation(int moneyAmount, BankClient recieveClient, string recieveAccountId)
        {
            if (GetAccountStatus())
            {
                MoneyWithdraw(moneyAmount);
                foreach (var o in recieveClient.Accounts)
                {
                    if (recieveAccountId == o.GetAccountId())
                    {
                        o.TopUpMoney(moneyAmount);
                        Client.LastOperation = new LastOperation(moneyAmount, Client, recieveClient, AccountId, recieveAccountId);
                    }
                }
            }
            else
            {
                throw new BanksException("Your deposit term haven't ended yet");
            }
        }

        public void CancelOperation()
        {
            int returnMoneyAmount = Client.LastOperation.MoneyAmount;
            TopUpMoney(returnMoneyAmount);
        }

        public DepositAccount GetThisAccount()
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
            if (!WithdrawAvailable)
                throw new BanksException("You can not to withdraw money before the end of the term");

            if (moneyAmount >= MoneyCount)
                    throw new BanksException("Not enough money");

            MoneyCount -= moneyAmount;
        }

        public string GetAccountId()
        {
            return AccountId;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine("____Deposit accounts:____");
            Console.WriteLine("Account Id: " + AccountId + ' ' + "Money amount: " + MoneyCount);
        }

        public void SkipMonth()
        {
            ++CurrentMonth;
            PercentHandler();

            if (DepositTerm.Month == CurrentMonth)
            {
                WithdrawAvailable = true;
            }
        }

        public void PercentHandler()
        {
            MoneyCount += (MoneyCount / 100) * ClientsBank.DepositPercentage;
        }
    }
}