using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Builder;

namespace Banks.Entities
{
    public class BankClient
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PassportNumber { get; set; }
        public Bank ClientsBank { get; set; }
        public bool AccountStatus { get; set; } // if 0 => questionable
        public List<IAccount> Accounts { get; set; }
    }
}