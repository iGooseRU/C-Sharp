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
        public double PercentageOnBalance { get; set; }
        public double CreditPercentage { get; set; }
        public double DepositPercentage { get; set; }

        public void GetClientsList()
        {
            foreach (var client in Clients)
            {
                Console.WriteLine("Surname: " + client.SecondName + ' ' + "Phone number: " + client.PhoneNumber);
            }
        }

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