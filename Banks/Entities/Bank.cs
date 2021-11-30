using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Builder;

namespace Banks.Entities
{
    public class Bank
    {
        public Bank(string bankName)
        {
            BankName = bankName;
            LicenseNum = GenerateLicenseNum();
            Clients = new List<BankClient>();
            Accounts = new List<IAccount>();
        }

        public string BankName { get; }
        public int LicenseNum { get; }
        public List<BankClient> Clients { get; }
        public List<IAccount> Accounts { get; }

        public BankClient CreateClient()
        {
            var director = new Director();
            var builder = new BankClientBuilder();
            director.Builder = builder;

            director.RegisterClient();
            BankClient client = builder.GetClient();
            client.ClientsBank = this;
            Clients.Add(client);

            return client;
        }

        private static int GenerateLicenseNum()
        {
            var cnt = new Random();
            int value = cnt.Next(100000, 999999);

            return value;
        }
    }
}