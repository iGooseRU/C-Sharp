using Banks.Entities;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest
    {
        private CentralBank _centraBank;

        [SetUp]
        public void SetUp()
        {
            _centraBank = new CentralBank();
        }

        [Test]
        public void Test()
        {
           var testBank = _centraBank.CreateBank("Tinkoff Bank");
           testBank.CreateClient();
        }
    }
}