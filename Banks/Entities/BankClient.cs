using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Tools;

namespace Banks.Entities
{
    public class BankClient
    {
        private const string CreditPrefix = "CRE";
        private const string DepositPrefix = "DEP";
        private const string DebitPrefix = "DEB";
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PassportNumber { get; set; }
        public Bank ClientsBank { get; set; }
        public bool AccountStatus { get; set; } // if 0 => questionable
        public List<IAccount> Accounts { get; set; }
        public LastOperation LastOperation { get; set; }

        public void CreateDebitAccount(IAccount account)
        {
            AddDebitAccount(account);
        }

        public void CreateDepositAccount(IAccount account, DateTime depositTerm)
        {
            AddDepositAccount(account);
        }

        public void CreateCreditAccount(IAccount account, int creditLimit)
        {
            AddCreditAccount(account);
        }

        public void TopOpMoney(double moneyAmount, string accountId)
        {
            foreach (var o in Accounts)
            {
                if (accountId == o.GetAccountId())
                {
                    o.TopUpMoney(moneyAmount);
                }
            }
        }

        public void WithdrawMoney(int moneyAmount, string accountId)
        {
            foreach (var o in Accounts)
            {
                if (accountId == o.GetAccountId())
                    o.MoneyWithdraw(moneyAmount);
            }
        }

        public void GetListCreditAccounts()
        {
            foreach (var o in Accounts)
            {
                string id = string.Empty;
                id = o.GetAccountId();
                if (id[2] == 'E')
                    o.PrintAccountInfo();
            }
        }

        public void GetListDebitAccounts()
        {
            foreach (var o in Accounts)
            {
                string id = string.Empty;
                id = o.GetAccountId();
                if (id[2] == 'B')
                    o.PrintAccountInfo();
            }
        }

        public void GetListDepositAccounts()
        {
            foreach (var o in Accounts)
            {
                string id = string.Empty;
                id = o.GetAccountId();
                if (id[2] == 'P')
                    o.PrintAccountInfo();
            }
        }

        public void GetListOfAccounts(BankClient client)
        {
            Console.WriteLine("__________AccountsInfo__________");
            GetListCreditAccounts();
            GetListDebitAccounts();
            GetListDepositAccounts();
        }

        public void GetClientsInfo(BankClient client) // вынести
        {
            Console.WriteLine("__________INFORMATION ABOUT " + client.FirstName + ' ' + client.SecondName + "__________");
            Console.WriteLine("Name: " + FirstName);
            Console.WriteLine("Surname: " + SecondName);
            Console.WriteLine("Phone number: " + PhoneNumber);
            Console.WriteLine("Passport data: " + (PassportNumber ?? "Not available"));
            Console.WriteLine("Bank: " + ClientsBank.BankName);
            Console.WriteLine("Account status: " + (AccountStatus ? "not questionable" : "questionable"));
            GetListOfAccounts(client);
        }

        public AccountTypeFlag AccountTypeHandler(string accountId)
        {
            if (accountId[0] == CreditPrefix[0] && accountId[1] == CreditPrefix[1] &&
                accountId[2] == CreditPrefix[2])
            {
                return AccountTypeFlag.Credit;
            }

            if (accountId[0] == DepositPrefix[0] && accountId[1] == DepositPrefix[1] &&
                accountId[2] == DepositPrefix[2])
            {
                return AccountTypeFlag.Deposit;
            }

            if (accountId[0] == DebitPrefix[0] && accountId[1] == DebitPrefix[1] &&
                accountId[2] == DebitPrefix[2])
            {
                return AccountTypeFlag.Debit;
            }

            return AccountTypeFlag.Unknown;
        }

        public void AddCreditAccount(IAccount account)
        {
            Accounts.Add(account);
        }

        public void AddDebitAccount(IAccount account)
        {
            Accounts.Add(account);
        }

        public void AddDepositAccount(IAccount account)
        {
            Accounts.Add(account);
        }
    }
}