namespace Banks.Builder
{
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder
        {
            set => _builder = value;
        }

        public void RegisterClient()
        {
            _builder.RegisterPhoneNumber();
            _builder.RegisterClientName();
            _builder.RegisterClientPassportData();
        }
    }
}