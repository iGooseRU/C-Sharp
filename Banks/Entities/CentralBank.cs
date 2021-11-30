using System.Collections.Generic;

namespace Banks.Entities
{
    public class CentralBank
    {
        public CentralBank()
        {
            Banks = new List<Bank>();
        }

        public List<Bank> Banks { get; }

        public Bank CreateBank(string name)
        {
            var bank = new Bank(name);
            Banks.Add(bank);

            return bank;
        }
    }
}