using System.Collections.Generic;
using Banks.Account;

namespace Banks.Entities
{
    public class BankClient
    {
        public BankClient()
        {
        }

        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PassportNumber { get; set; }
        public Bank ClientsBank { get; set; }
        public bool AccountStatus { get; set; } // if 0 => questionable
        public List<IAccount> Accounts { get; set; }
    }
}