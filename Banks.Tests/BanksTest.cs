using System;
using Banks.Account;
using Banks.Entities;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        private CentralBank _centralBank;

        [SetUp]
        public void SetUp()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void CreateBankAddClientTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            Assert.Contains(firstClient, testBank.Clients);
        }

        [Test]
        public void QuestionableAccountTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", null, testBank);
            Assert.AreEqual(false, firstClient.AccountStatus);
        }

        [Test]
        public void CreateDebitAccountClientTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            Assert.AreEqual(1, firstClient.DebitAccounts.Count);
        }
        
        [Test]
        public void CreateDebitAccountBankTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            Assert.AreEqual(1, testBank.DebitAccounts.Count);
        }
        
        [Test]
        public void TopUpMoneyDebitAccountTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));

            string accountId = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                accountId = o.AccountId;
            }
            
            firstClient.TopOpMoney(250, accountId);

            DebitAccount account = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                account = o;
            }

            if (account != null) Assert.AreEqual(250, account.MoneyCount);
        }

        [Test]
        public void WithdrawMoneyDebitAccountTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));

            string accountId = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                accountId = o.AccountId;
            }
            
            firstClient.TopOpMoney(250, accountId);
            firstClient.WithdrawMoney(100, accountId);

            DebitAccount account = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                account = o;
            }

            if (account != null) Assert.AreEqual(150, account.MoneyCount);
        }
        
        [Test]
        public void WithdrawMoneyDepositAccountCatchExceptionTest()
        {
            Assert.Catch<BanksException>(() =>
            {
                Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
                BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                    "Guskov", "4016 571549", testBank);
                firstClient.CreateDepositAccount(new DepositAccount(firstClient));

                string accountId = null;
                foreach (DepositAccount o in firstClient.DepositAccounts)
                {
                    accountId = o.AccountId;
                }

                firstClient.TopOpMoney(250, accountId);
                firstClient.WithdrawMoney(100, accountId);
            });
        }
        
        [Test]
        public void WithdrawMoneyCreditAccountCatchExceptionTest()
        {
            Assert.Catch<BanksException>(() =>
            {
                Bank testBank = _centralBank.CreateBank("Tinkoff Bank", 2);
                BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                    "Guskov", "4016 571549", testBank);
                firstClient.CreateCreditAccount(new CreditAccount(firstClient));

                string accountId = null;
                foreach (CreditAccount o in firstClient.CreditAccounts)
                {
                    accountId = o.AccountId;
                }

                firstClient.TopOpMoney(250, accountId);
                firstClient.WithdrawMoney(100, accountId);
            });
        }
    }
}