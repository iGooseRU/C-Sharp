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
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            Assert.Contains(firstClient, testBank.Clients);
        }

        [Test]
        public void QuestionableAccountTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", null, testBank);
            Assert.AreEqual(false, firstClient.AccountStatus);
        }

        [Test]
        public void CreateDebitAccountClientTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            Assert.AreEqual(1, firstClient.DebitAccounts.Count);
        }
        
        [Test]
        public void CreateDebitAccountBankTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            Assert.AreEqual(1, testBank.DebitAccounts.Count);
        }
        
        [Test]
        public void TopUpMoneyDebitAccountTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
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
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
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
        public void WithdrawMoneyCreditAccountCatchExceptionTest()
        {
            Assert.Catch<BanksException>(() =>
            {
                Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
                BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                    "Guskov", "4016 571549", testBank);
                firstClient.CreateCreditAccount(new CreditAccount(firstClient), 5000);

                string accountId = null;
                foreach (CreditAccount o in firstClient.CreditAccounts)
                {
                    accountId = o.AccountId;
                }

                firstClient.TopOpMoney(250, accountId);
                firstClient.WithdrawMoney(100, accountId);
            });
        }

        [Test]
        public void TransactionTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            BankClient secondClient = _centralBank.CreateClient("+79218653232", "Name",
                "Surname", "4016 571555", testBank);
            secondClient.CreateDebitAccount(new DebitAccount(secondClient));

            string firstClientAccountId = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                firstClientAccountId = o.AccountId;
            }
            
            string secondClientAccountId = null;
            foreach (DebitAccount o in secondClient.DebitAccounts)
            {
                secondClientAccountId = o.AccountId;
            }
            
            firstClient.TopOpMoney(1000, firstClientAccountId);
            _centralBank.MakeMoneyTransfer(300, firstClient, secondClient, firstClientAccountId, secondClientAccountId);
            
            DebitAccount firstClientAccount = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                firstClientAccount = o;
            }
            
            Assert.AreEqual(700, firstClientAccount.MoneyCount);
        }
        
        [Test]
        public void CancelTransactionTest()
        {
            const int MoneyTopUp = 1000;
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            BankClient secondClient = _centralBank.CreateClient("+79218653232", "Name",
                "Surname", "4016 571555", testBank);
            secondClient.CreateDebitAccount(new DebitAccount(secondClient));

            string firstClientAccountId = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                firstClientAccountId = o.AccountId;
            }
            
            string secondClientAccountId = null;
            foreach (DebitAccount o in secondClient.DebitAccounts)
            {
                secondClientAccountId = o.AccountId;
            }
            
            firstClient.TopOpMoney(MoneyTopUp, firstClientAccountId);
            _centralBank.MakeMoneyTransfer(300, firstClient, secondClient, firstClientAccountId, secondClientAccountId);
            
            DebitAccount firstClientAccount = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                firstClientAccount = o;
            }
            
            _centralBank.CancelLastOperation(firstClient);
            
            Assert.AreEqual(MoneyTopUp, firstClientAccount.MoneyCount);
        }

        [Test]
        public void SkipOneMonthTest()
        {
            Bank testBank = _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", testBank);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));

            string accountId = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                accountId = o.AccountId;
            }
            
            firstClient.TopOpMoney(100, accountId);

            DebitAccount account = null;
            foreach (DebitAccount o in firstClient.DebitAccounts)
            {
                account = o;
            }
            
            account.SkipMonth(); // +1 % to money amount
            Assert.AreEqual(101, account.MoneyCount);

        }
    }
}