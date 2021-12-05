using Banks.Entities;

namespace Banks.Builder
{
    public interface IBuilder
    {
        void RegisterPhoneNumber(string phoneNumber);
        void RegisterClientName(string firstName, string secondName);
        void RegisterClientPassportData(string passportData);
        void CreateAccountListAddBank(Bank bank);
    }
}