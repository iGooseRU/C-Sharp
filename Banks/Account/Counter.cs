using Banks.Entities;

namespace Banks.Account
{
    public class Counter
    {
        private const string CreditPrefix = "CRE";
        private const string DepositPrefix = "DEP";
        private const string DebitPrefix = "DEB";

        private static int creditId = 100000;
        private static int depositId = 100000;
        private static int debitId = 100000;

        public string GetId(AccountTypeFlag accountType)
        {
            switch (accountType)
            {
                case AccountTypeFlag.Credit:
                    ++creditId;
                    string creditRes = CreditPrefix + creditId.ToString();
                    return creditRes;

                case AccountTypeFlag.Deposit:
                    ++depositId;
                    string depositRes = DepositPrefix + depositId.ToString();
                    return depositRes;

                case AccountTypeFlag.Debit:
                    ++debitId;
                    string debitRes = DebitPrefix + debitId.ToString();
                    return debitRes;
            }

            return null;
        }
    }
}