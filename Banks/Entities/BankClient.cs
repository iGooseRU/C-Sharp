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
        public List<DebitAccount> DebitAccounts { get; set; }
        public List<DepositAccount> DepositAccounts { get; set; }
        public List<CreditAccount> CreditAccounts { get; set; }

        public void CreateDebitAccount(IAccount account)
        {
            account.CreateAccount();
        }

        public void CreateDepositAccount(IAccount account)
        {
            account.CreateAccount();
        }

        public void CreateCreditAccount(IAccount account)
        {
            account.CreateAccount();
        }

        public void TopOpMoney(int moneyAmount, string accountId)
        {
            AccountTypeFlag type = AccountTypeHandler(accountId);

            switch (type)
            {
                default:
                    throw new BanksException("Can not to make operation");

                case AccountTypeFlag.Unknown:
                    throw new BanksException("Invalid account prefix name");

                case AccountTypeFlag.Credit:
                    foreach (CreditAccount o in CreditAccounts)
                    {
                        if (accountId == o.AccountId)
                            o.TopUpMoney(moneyAmount);
                    }

                    break;

                case AccountTypeFlag.Deposit:
                    foreach (DepositAccount o in DepositAccounts)
                    {
                        if (accountId == o.AccountId)
                            o.TopUpMoney(moneyAmount);
                    }

                    break;

                case AccountTypeFlag.Debit:
                    foreach (DebitAccount o in DebitAccounts)
                    {
                        if (accountId == o.AccountId)
                            o.TopUpMoney(moneyAmount);
                    }

                    break;
            }
        }

        public void WithdrawMoney(int moneyAmount, string accountId)
        {
            AccountTypeFlag type = AccountTypeHandler(accountId);

            switch (type)
            {
                default:
                    throw new BanksException("Can not to make operation");

                case AccountTypeFlag.Unknown:
                    throw new BanksException("Invalid account prefix name");

                case AccountTypeFlag.Credit:
                    throw new BanksException("You can not to withdraw money from credit account");

                case AccountTypeFlag.Deposit:
                    foreach (DepositAccount o in DepositAccounts)
                    {
                        if (accountId == o.AccountId)
                            o.MoneyWithdraw(moneyAmount);
                    }

                    break;

                case AccountTypeFlag.Debit:
                    foreach (DebitAccount o in DebitAccounts)
                    {
                        if (accountId == o.AccountId)
                            o.MoneyWithdraw(moneyAmount);
                    }

                    break;
            }
        }

        public void GetMoneyAmount(string accountId)
        {
        }

        private AccountTypeFlag AccountTypeHandler(string accountId)
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

        private void DepositTermRequest()
        {
        }
    }
}