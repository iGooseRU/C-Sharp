using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Builder;

namespace Banks.Entities
{
    public class Bank
    {
        public Bank(string bankName, int percentageOnBalance)
        {
            BankName = bankName;
            PercentageOnBalance = percentageOnBalance;

            LicenseNum = GenerateLicenseNum();
            Clients = new List<BankClient>();
            DebitAccounts = new List<DebitAccount>();
            DepositAccounts = new List<DepositAccount>();
            CreditAccounts = new List<CreditAccount>();
        }

        public string BankName { get; }
        public int LicenseNum { get; }
        public List<BankClient> Clients { get; }
        public List<DebitAccount> DebitAccounts { get; set; }
        public List<DepositAccount> DepositAccounts { get; set; }
        public List<CreditAccount> CreditAccounts { get; set; }
        public int PercentageOnBalance { get; set; }
        public void AddClient(BankClient client)
        {
            Clients.Add(client);
        }

        private static int GenerateLicenseNum()
        {
            var cnt = new Random();
            int value = cnt.Next(100000, 999999);

            return value;
        }
    }
}