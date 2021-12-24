namespace Banks.Entities
{
    public class LastOperation
    {
        public LastOperation(int moneyAmount, BankClient sendClient, BankClient recieveClient, string sendAccountId, string recieveAccountId)
        {
            MoneyAmount = moneyAmount;
            SendClient = sendClient;
            RecieveClient = recieveClient;
            SendAccountId = sendAccountId;
            RecieveAccountId = recieveAccountId;
        }

        public CentralBank CentralBank { get; set; }
        public BankClient SendClient { get; set; }
        public BankClient RecieveClient { get; set; }
        public string SendAccountId { get; set; }
        public string RecieveAccountId { get; set; }
        public int MoneyAmount { get; set; }
    }
}