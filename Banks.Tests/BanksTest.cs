using Banks.Entities;
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
    }
}