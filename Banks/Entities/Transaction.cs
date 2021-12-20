using Banks.Account;
using Banks.Tools;

namespace Banks.Entities
{
    public class Transaction
    {
        public Transaction(CentralBank centralBank, int moneyAmount, BankClient sendClient, BankClient recieveClient, string sendAccountId, string recieveAccountId)
        {
            CentralBank = centralBank;
            MoneyAmount = moneyAmount;
            SendClient = sendClient;
            RecieveClient = recieveClient;
            SendAccountId = sendAccountId;
            RecieveAccountId = recieveAccountId;
        }

        public CentralBank CentralBank { get; }
        public BankClient SendClient { get; set; }
        public BankClient RecieveClient { get; set; }
        public string SendAccountId { get; set; }
        public string RecieveAccountId { get; set; }
        public int MoneyAmount { get; set; }

        public void MakeOperation()
        {
             AccountTypeFlag sendAccountType = SendClient.AccountTypeHandler(SendAccountId);

             switch (sendAccountType)
            {
                default:
                    throw new BanksException("Can not to complete operation. Try later");
                case AccountTypeFlag.Unknown:
                    throw new BanksException("Can not to find sens account");

                case AccountTypeFlag.Debit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            if (account.GetMoneyAmount() - MoneyAmount > 0)
                            {
                                account.MoneyWithdraw(MoneyAmount);
                                o.LastOperation = this;
                            }
                            else
                            {
                                throw new BanksException("You dont have enough money");
                            }
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.TopUpMoney(MoneyAmount);
                        }
                    }

                    break;

                case AccountTypeFlag.Deposit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            if (account.GetAccountStatus())
                            {
                                if (account.GetMoneyAmount() - MoneyAmount > 0)
                                {
                                    account.MoneyWithdraw(MoneyAmount);
                                    o.LastOperation = this;
                                }
                                else
                                {
                                    throw new BanksException("You dont have enough money");
                                }
                            }
                            else
                            {
                                throw new BanksException("you can not to make operation, " +
                                                         "because term of your deposit doesn't end");
                            }
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.TopUpMoney(MoneyAmount);
                        }
                    }

                    break;

                case AccountTypeFlag.Credit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.MoneyWithdraw(MoneyAmount);
                        }
                    }

                    break;
            }
        }

        public void CancelOperation()
        {
            AccountTypeFlag sendAccountType = SendClient.AccountTypeHandler(SendAccountId);

            switch (sendAccountType)
            {
                default:
                    throw new BanksException("Can not to complete operation. Try later");
                case AccountTypeFlag.Unknown:
                    throw new BanksException("Can not to find sens account");

                case AccountTypeFlag.Debit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.TopUpMoney(MoneyAmount);
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyWithdraw(MoneyAmount);
                        }
                    }

                    break;

                case AccountTypeFlag.Deposit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.TopUpMoney(MoneyAmount);
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyWithdraw(MoneyAmount);
                        }
                    }

                    break;

                case AccountTypeFlag.Credit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.TopUpMoney(MoneyAmount);
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            var account = o.Accounts.Find(m => m.GetAccountId() == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyWithdraw(MoneyAmount);
                        }
                    }

                    break;
            }
        }
    }
}