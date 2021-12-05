using Banks.Entities;

namespace Banks.Builder
{
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set => _builder = value;
        }

        public void RegisterClient(string phoneNumber, string firstName, string secondName, string passportData, Bank bank)
        {
            _builder.RegisterPhoneNumber(phoneNumber);
            _builder.RegisterClientName(firstName, secondName);
            _builder.RegisterClientPassportData(passportData);
            _builder.CreateAccountListAddBank(bank);
        }
    }
}