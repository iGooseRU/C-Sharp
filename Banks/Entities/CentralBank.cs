using System;
using System.Collections.Generic;
using Banks;
using Banks.Builder;
using Banks.Tools;

namespace Banks.Entities
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
            AllClients = new List<BankClient>();
        }

        public List<Bank> Banks { get; }
        public List<BankClient> AllClients { get; }
        public Bank CreateBank(string name)
        {
            var bank = new Bank(name);
            Banks.Add(bank);

            return bank;
        }

        public BankClient CreateClient(string phoneNumber, string firstName, string secondName, string passportData, string bankName)
        {
            Bank bank = Banks.Find(m => m.BankName == bankName);
            if (bank == null)
                throw new BanksException("Can not to find bank. Try again.");

            var director = new Director();
            var builder = new BankClientBuilder();
            director.Builder = builder;
            director.RegisterClient(phoneNumber, firstName, secondName, passportData, bank);
            BankClient client = builder.GetClient();
            bank.AddClient(client);
            AddClientToGlobalList(client);

            return client;
        }

        public void MakeMoneyTransfer(int moneyAmount, BankClient sendClient, BankClient receiveClient, string sendAccountId, string receiveAccountId)
        {
            foreach (var o in sendClient.Accounts)
            {
                if (o.GetAccountId() == sendAccountId)
                {
                    o.MakeOperation(moneyAmount, receiveClient, sendAccountId);
                }
            }
        }

        public void CancelLastOperation(BankClient client, string accountId)
        {
            foreach (var o in client.Accounts)
            {
                if (o.GetAccountId() == accountId)
                {
                    o.CancelOperation();
                }
            }
        }

        public void AddClientToGlobalList(BankClient client)
        {
            AllClients.Add(client);
        }
    }
}