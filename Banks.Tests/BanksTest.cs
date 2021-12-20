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
            string bankName = "Tinkoff Bank";
            Bank testBank = _centralBank.CreateBank(bankName);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            Assert.Contains(firstClient, testBank.Clients);
        }
        
        [Test]
        public void QuestionableAccountTest()
        {
            string bankName = "Tinkoff Bank";
            _centralBank.CreateBank(bankName);
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", null, bankName);
            Assert.AreEqual(false, firstClient.AccountStatus);
        }

        [Test]
        public void TopUpMoneyTest()
        {
            string bankName = "Tinkoff Bank";
        
            _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            double moneyAmount = 0;
            string accountId = null;
            foreach (IAccount o in firstClient.Accounts)
            {
                accountId = o.GetAccountId();
            }
            
            firstClient.TopOpMoney(250, accountId);
        
            foreach (IAccount o in firstClient.Accounts)
            {
                moneyAmount = o.GetMoneyAmount();
            }
            
            Assert.AreEqual(250, moneyAmount);
        }
        
        [Test]
        public void WithdrawMoneyTest()
        {
            string bankName = "Tinkoff Bank";
        
            _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
        
            string accountId = null;
            double moneyAmount = 0;

            foreach (IAccount o in firstClient.Accounts)
            {
                accountId = o.GetAccountId();
            }
            
            firstClient.TopOpMoney(250, accountId);
            firstClient.WithdrawMoney(100, accountId);
        
            foreach (IAccount o in firstClient.Accounts)
            {
                moneyAmount = o.GetMoneyAmount();
            }
            
            Assert.AreEqual(150, moneyAmount);
        }
        
        [Test]
        public void WithdrawMoneyCreditAccountCatchExceptionTest()
        {
            Assert.Catch<BanksException>(() =>
            {
                string bankName = "Tinkoff Bank";
        
                _centralBank.CreateBank("Tinkoff Bank");
                BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                    "Guskov", "4016 571549", bankName);
                firstClient.CreateCreditAccount(new CreditAccount(firstClient), 5000);
        
                string accountId = null;
                foreach (IAccount o in firstClient.Accounts)
                {
                    accountId = o.GetAccountId();
                }
        
                firstClient.TopOpMoney(250, accountId);
                firstClient.WithdrawMoney(100, accountId);
            });
        }
        
        [Test]
        public void TransactionTest()
        {
            string bankName = "Tinkoff Bank";
        
            _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            BankClient secondClient = _centralBank.CreateClient("+79218653232", "Name",
                "Surname", "4016 571555", bankName);
            secondClient.CreateDebitAccount(new DebitAccount(secondClient));
        
            string firstClientAccountId = null;
            foreach (IAccount o in firstClient.Accounts)
            {
                firstClientAccountId = o.GetAccountId();
            }
            
            string secondClientAccountId = null;
            foreach (IAccount o in secondClient.Accounts)
            {
                secondClientAccountId = o.GetAccountId();
            }
            
            firstClient.TopOpMoney(1000, firstClientAccountId);
            _centralBank.MakeMoneyTransfer(300, firstClient, secondClient, firstClientAccountId, secondClientAccountId);

            double moneyAmountFirstClient = 0;
            foreach (IAccount o in firstClient.Accounts)
            {
                moneyAmountFirstClient = o.GetMoneyAmount();
            }
            
            Assert.AreEqual(700, moneyAmountFirstClient);
        }
        
        [Test]
        public void CancelTransactionTest()
        {
            string bankName = "Tinkoff Bank";
        
            const int moneyTopUp = 1000;
            _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
            BankClient secondClient = _centralBank.CreateClient("+79218653232", "Name",
                "Surname", "4016 571555", bankName);
            secondClient.CreateDebitAccount(new DebitAccount(secondClient));
        
            string firstClientAccountId = null;
            foreach (IAccount o in firstClient.Accounts)
            {
                firstClientAccountId = o.GetAccountId();
            }
            
            string secondClientAccountId = null;
            foreach (IAccount o in secondClient.Accounts)
            {
                secondClientAccountId = o.GetAccountId();
            }
            
            firstClient.TopOpMoney(moneyTopUp, firstClientAccountId);
            _centralBank.MakeMoneyTransfer(300, firstClient, secondClient, firstClientAccountId, secondClientAccountId);
            
            _centralBank.CancelLastOperation(firstClient);
        
            double moneyAmountFirstClient = 0;
            foreach (IAccount o in firstClient.Accounts)
            {
                moneyAmountFirstClient = o.GetMoneyAmount();
            }
            
            Assert.AreEqual(moneyTopUp, moneyAmountFirstClient);
        }
        
        [Test]
        public void SkipOneMonthTest()
        {
            string bankName = "Tinkoff Bank";
        
            _centralBank.CreateBank("Tinkoff Bank");
            BankClient firstClient = _centralBank.CreateClient("+79218653265", "Egor",
                "Guskov", "4016 571549", bankName);
            firstClient.CreateDebitAccount(new DebitAccount(firstClient));
        
            string accountId = null;
            double moneyAmount = 0;
            foreach (IAccount o in firstClient.Accounts)
            {
                accountId = o.GetAccountId();
            }
            
            firstClient.TopOpMoney(100, accountId);
            
            foreach (IAccount o in firstClient.Accounts)
            {
                o.SkipMonth(); // +1 % to money amount
                moneyAmount = o.GetMoneyAmount();
            }
            
            Assert.AreEqual(101, moneyAmount);
        }
    }
}