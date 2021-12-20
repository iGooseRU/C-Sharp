﻿using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Account
{
    public class DebitAccount : IAccount
    {
        private const double StartMoneyCount = 0;
        public DebitAccount(BankClient client)
        {
            Client = client;
            ClientsBank = client.ClientsBank;
            AccountType = AccountTypeFlag.Debit;

            var cnt = Counter.Source;
            AccountId = cnt.GetId(AccountType);

            WithdrawAvailable = true;
            MoneyCount = StartMoneyCount;

            CurrentMonth = DateTime.Now.Month;
        }

        public string AccountId { get; }
        public bool WithdrawAvailable { get; }
        public BankClient Client { get; }
        public double MoneyCount { get; set; }
        public AccountTypeFlag AccountType { get; }
        public Bank ClientsBank { get; }
        public int CurrentMonth { get; set; }

        public DebitAccount GetThisAccount()
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
            MoneyCount -= moneyAmount;
        }

        public string GetAccountId()
        {
            return AccountId;
        }

        public void GetAccountInfo()
        {
            Console.WriteLine("Account Id: " + AccountId + ' ' + "Money amount: " + MoneyCount);
        }

        public void SkipMonth()
        {
            ++CurrentMonth;
            PercentHandler();
        }

        public void PercentHandler()
        {
            var cnt = (MoneyCount / 100) * ClientsBank.PercentageOnBalance;
            MoneyCount += cnt;
        }
    }
}