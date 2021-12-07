using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Builder
{
    public class BankClientBuilder : IBuilder
    {
        private BankClient _client = new BankClient();

        public BankClientBuilder()
        {
            Reset();
        }

        public void RegisterPhoneNumber(string phoneNumber)
        {
            _client.PhoneNumber = phoneNumber;
        }

        public void RegisterClientName(string firstName, string secondName)
        {
            if (firstName == null)
                throw new BanksException("You must enter your name! ");
            _client.FirstName = firstName;

            if (secondName == null)
                throw new BanksException("You must enter your surname! ");
            _client.SecondName = secondName;
        }

        public void RegisterClientPassportData(string passportData)
        {
            if (passportData == "skip" || passportData == null)
            {
                Console.WriteLine("Your account is questionable!");
                _client.PassportNumber = null;
                _client.AccountStatus = false;
            }
            else
            {
                _client.PassportNumber = passportData;
                _client.AccountStatus = true;
            }
        }

        public void CreateAccountsListAddBank(Bank bank)
        {
            _client.DepositAccounts = new List<DepositAccount>();
            _client.DebitAccounts = new List<DebitAccount>();
            _client.CreditAccounts = new List<CreditAccount>();
            _client.ClientsBank = bank;
        }

        public BankClient GetClient()
        {
            BankClient result = _client;
            this.Reset();

            return result;
        }

        private void Reset()
        {
            _client = new BankClient();
        }
    }
}