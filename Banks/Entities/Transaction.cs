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
                            DebitAccount account = o.DebitAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            if (account.MoneyCount - MoneyAmount > 0)
                            {
                                account.MoneyCount -= MoneyAmount;
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
                            DebitAccount account = o.DebitAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount += MoneyAmount;
                        }
                    }

                    break;

                case AccountTypeFlag.Deposit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            DepositAccount account = o.DepositAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            if (account.WithdrawAvailable)
                            {
                                if (account.MoneyCount - MoneyAmount > 0)
                                {
                                    account.MoneyCount -= MoneyAmount;
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
                            DepositAccount account = o.DepositAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount += MoneyAmount;
                        }
                    }

                    break;

                case AccountTypeFlag.Credit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            CreditAccount account = o.CreditAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            if (account.MoneyCount - MoneyAmount > account.CreditLimit)
                            {
                                account.MoneyCount -= MoneyAmount;
                                o.LastOperation = this;
                            }
                            else
                            {
                                throw new BanksException("You have been reached your credit limit. Operation canceled");
                            }
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            CreditAccount account = o.CreditAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount += MoneyAmount;
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
                            DebitAccount account = o.DebitAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.MoneyCount += MoneyAmount;
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            DebitAccount account = o.DebitAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount -= MoneyAmount;
                        }
                    }

                    break;

                case AccountTypeFlag.Deposit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            DepositAccount account = o.DepositAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.MoneyCount += MoneyAmount;
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            DepositAccount account = o.DepositAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount -= MoneyAmount;
                        }
                    }

                    break;

                case AccountTypeFlag.Credit:
                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == SendClient.PhoneNumber)
                        {
                            CreditAccount account = o.CreditAccounts.Find(m => m.AccountId == SendAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find send account");

                            account.MoneyCount += MoneyAmount;
                        }
                    }

                    foreach (BankClient o in CentralBank.AllClients)
                    {
                        if (o.PhoneNumber == RecieveClient.PhoneNumber)
                        {
                            CreditAccount account = o.CreditAccounts.Find(m => m.AccountId == RecieveAccountId);
                            if (account == null)
                                throw new BanksException("Can not to find receive account");

                            account.MoneyCount -= MoneyAmount;
                        }
                    }

                    break;
            }
        }
    }
}