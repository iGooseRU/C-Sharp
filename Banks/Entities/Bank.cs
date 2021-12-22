using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Builder;

namespace Banks.Entities
{
    public class Bank
    {
        // for deposit
        private const double DepositPerc = 4;

        // for credit
        private const double CreditPerc = 3;

        // for debit
        private const double PercOnBalance = 1;

        public Bank(string bankName)
        {
            BankName = bankName;

            PercentageOnBalance = PercOnBalance;
            CreditPercentage = CreditPerc;
            DepositPercentage = DepositPerc;

            Clients = new List<BankClient>();
            Accounts = new List<IAccount>();
        }

        public string BankName { get; }
        public List<BankClient> Clients { get; }
        public List<IAccount> Accounts { get; set; }
        public double PercentageOnBalance { get; set; }
        public double CreditPercentage { get; set; }
        public double DepositPercentage { get; set; }

        public List<BankClient> GetClientsList()
        {
            return Clients;
        }

        public void AddClient(BankClient client)
        {
            Clients.Add(client);
        }

        public void AddAccount(IAccount account)
        {
            Accounts.Add(account);
        }
    }
}