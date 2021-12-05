using System;
using System.Collections.Generic;
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
        }

        public string BankName { get; }
        public int LicenseNum { get; }
        public List<BankClient> Clients { get; }

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