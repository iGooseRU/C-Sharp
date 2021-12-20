using System;
using System.Collections.Generic;
using Banks.Account;
using Banks.Entities;
using Banks.Tools;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            // ДАВАЙТЕ НЕ БУДЕМ НИЧЕГО ТРОГАТЬ В КОДЕ ПОЖАЛУЙСТА
            // ПОКА ОНО РАБОТАЕТ С ГОРЕМ ПОПОЛАМ
            var centralBank = new CentralBank();

            Console.ForegroundColor = ConsoleColor.Red;
            string ch = "y";
            while (ch == "y" || ch == "Y")
            {
                var printTool = new PrintTool();
                printTool.PrintStartMenu();
                Console.Write("Enter your answer:");

                string ans = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("__________________________________");
                Console.WriteLine();
                switch (ans)
                {
                    default:
                        throw new BanksException("Unknown answer. Try again.");
                    case "Exit":
                        return;
                    case "exit":
                        return;

                    case "1":
                        Console.Write("Enter bank's name:");
                        var cnt = centralBank.CreateBank(Console.ReadLine());

                        if (centralBank.Banks.Find(m => m.BankName == cnt.BankName) == null)
                        {
                            throw new BanksException("Can not to create bank. try later");
                        }
                        else
                        {
                            Console.WriteLine(cnt.BankName + "has been created successfully!");
                        }

                        break;

                    case "2":
                        Console.WriteLine("Enter information about client:");
                        Console.Write("Phone number:");
                        string phoneNum = Console.ReadLine();

                        Console.Write("Name:");
                        string name = Console.ReadLine();

                        Console.Write("Surname:");
                        string surname = Console.ReadLine();

                        Console.Write("Passport data not required. Enter data or enter 'skip': ");
                        string passData = Console.ReadLine();

                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankName = Console.ReadLine();

                        var client = centralBank.CreateClient(phoneNum, name, surname, passData, bankName);

                        if (centralBank.AllClients.Find(m => m.PhoneNumber == phoneNum) == null)
                            throw new BanksException("Can not to create cliet. Try later.");
                        else
                            Console.WriteLine("Client has been created successfully!");
                        break;

                    case "3":
                        Console.WriteLine("Creating credit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph = Console.ReadLine();
                        Console.Write("Enter credit limit:");
                        int lim = Convert.ToInt32(Console.ReadLine());

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph == o.PhoneNumber)
                            {
                                o.CreateCreditAccount(new CreditAccount(o), lim);
                            }
                        }

                        break;

                    case "4":
                        Console.WriteLine("Creating debit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns1 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns1 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph1 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph1 == o.PhoneNumber)
                            {
                                o.CreateDebitAccount(new DebitAccount(o));
                            }
                        }

                        break;

                    case "5":
                        Console.WriteLine("Creating deposit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns2 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns2 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph2 = Console.ReadLine();
                        Console.Write("Enter deposit term: (DD/MM/YYYY FORMAT)");
                        DateTime term = DateTime.Parse(Console.ReadLine());

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph2 == o.PhoneNumber)
                            {
                                o.CreateDepositAccount(new DepositAccount(o), term);
                            }
                        }

                        break;

                    case "6":
                        Console.WriteLine("__________TOP UP MONEY MENU__________");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAnsa = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAnsa == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string phnum = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (phnum == o.PhoneNumber)
                            {
                                Console.WriteLine("All available accounts:");
                                o.GetListOfAccounts(o);
                                Console.Write("Enter account:");
                                string accTopUp = Console.ReadLine();
                                var accType = o.AccountTypeHandler(accTopUp);
                                Console.Write("Enter money amount: ");
                                double moneyAmount = Convert.ToDouble(Console.ReadLine());

                                switch (accType)
                                {
                                    default:
                                        throw new BanksException("Can not to top up money. Try later.");
                                    case AccountTypeFlag.Unknown:
                                        throw new BanksException("Can not to top up money. Try later.");

                                    case AccountTypeFlag.Credit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accTopUp == m.GetAccountId())
                                            {
                                                m.TopUpMoney(moneyAmount);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;

                                    case AccountTypeFlag.Debit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accTopUp == m.GetAccountId())
                                            {
                                                m.TopUpMoney(moneyAmount);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;

                                    case AccountTypeFlag.Deposit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accTopUp == m.GetAccountId())
                                            {
                                                m.TopUpMoney(moneyAmount);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;
                                }
                            }
                        }

                        break;

                    case "7":
                        Console.WriteLine("__________WITHDRAW MONEY MENU__________");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAnsp = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAnsp == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string phnum1 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (phnum1 == o.PhoneNumber)
                            {
                                Console.WriteLine("All available accounts:");
                                o.GetListOfAccounts(o);
                                Console.Write("Enter account:");
                                string accWithdraw = Console.ReadLine();
                                var accType = o.AccountTypeHandler(accWithdraw);
                                Console.Write("Enter money amount: ");
                                int moneyAmount = Convert.ToInt32(Console.ReadLine());

                                switch (accType)
                                {
                                    default:
                                        throw new BanksException("Can not to withdraw money. Try later.");
                                    case AccountTypeFlag.Unknown:
                                        throw new BanksException("Can not to withdraw money. Try later.");

                                    case AccountTypeFlag.Credit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accWithdraw == m.GetAccountId())
                                            {
                                                o.WithdrawMoney(moneyAmount, accWithdraw);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;

                                    case AccountTypeFlag.Debit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accWithdraw == m.GetAccountId())
                                            {
                                                m.MoneyWithdraw(moneyAmount);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;

                                    case AccountTypeFlag.Deposit:
                                        foreach (var m in o.Accounts)
                                        {
                                            if (accWithdraw == m.GetAccountId())
                                            {
                                                m.MoneyWithdraw(moneyAmount);
                                            }
                                            else
                                            {
                                                throw new BanksException("Can not to find account. Try again.");
                                            }
                                        }

                                        break;
                                }
                            }
                        }

                        break;

                    case "8":
                        Console.WriteLine("__________MONEY TRANSFER__________");
                        int moneyAmountTransfer = 0;
                        string bankAnsTransfer = string.Empty;
                        string bankAnsTransfer1 = string.Empty;
                        BankClient sendClient = null;
                        BankClient receiveClient = null;
                        string sendId = string.Empty;
                        string receiveId = string.Empty;

                        Console.Write("Enter money amount: ");
                        moneyAmountTransfer = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter send client: ");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        bankAnsTransfer = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAnsTransfer == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string phNumTransfer = Console.ReadLine();
                        foreach (var o in centralBank.AllClients)
                        {
                            if (phNumTransfer == o.PhoneNumber)
                            {
                                sendClient = o;
                                Console.WriteLine("Choose account type:");
                                Console.WriteLine("Credit");
                                Console.WriteLine("Debit");
                                Console.WriteLine("Deposit");
                                Console.Write("Your answer:");
                                string choose = Console.ReadLine();
                                AccountTypeFlag type = o.AccountTypeHandler(choose);

                                switch (type)
                                {
                                    default:
                                        throw new BanksException("Can not to get accounts. Try later.");
                                    case AccountTypeFlag.Unknown:
                                        throw new BanksException("Can not to get accounts. Try later.");
                                    case AccountTypeFlag.Credit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListCreditAccounts();
                                        Console.Write("Your answer: ");
                                        sendId = Console.ReadLine();
                                        break;

                                    case AccountTypeFlag.Debit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListDebitAccounts();
                                        Console.Write("Your answer: ");
                                        sendId = Console.ReadLine();
                                        break;

                                    case AccountTypeFlag.Deposit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListDepositAccounts();
                                        Console.Write("Your answer: ");
                                        sendId = Console.ReadLine();
                                        break;
                                }
                            }
                        }

                        Console.WriteLine("Enter receive client: ");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        bankAnsTransfer1 = Console.ReadLine();

                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAnsTransfer1 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string phNumTransfer1 = Console.ReadLine();
                        foreach (var o in centralBank.AllClients)
                        {
                            if (phNumTransfer1 == o.PhoneNumber)
                            {
                                receiveClient = o;
                                Console.WriteLine("Choose account type:");
                                Console.WriteLine("Credit");
                                Console.WriteLine("Debit");
                                Console.WriteLine("Deposit");
                                Console.Write("Your answer:");
                                string choose = Console.ReadLine();
                                var type = o.AccountTypeHandler(choose);

                                switch (type)
                                {
                                    default:
                                        throw new BanksException("Can not to get accounts. Try later.");
                                    case AccountTypeFlag.Unknown:
                                        throw new BanksException("Can not to get accounts. Try later.");
                                    case AccountTypeFlag.Credit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListCreditAccounts();
                                        Console.Write("Your answer: ");
                                        receiveId = Console.ReadLine();
                                        break;

                                    case AccountTypeFlag.Debit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListDebitAccounts();
                                        Console.Write("Your answer: ");
                                        receiveId = Console.ReadLine();
                                        break;

                                    case AccountTypeFlag.Deposit:
                                        Console.WriteLine("Choose account:");
                                        o.GetListDepositAccounts();
                                        Console.Write("Your answer: ");
                                        receiveId = Console.ReadLine();
                                        break;
                                }
                            }
                        }

                        centralBank.MakeMoneyTransfer(moneyAmountTransfer, sendClient, receiveClient, sendId, receiveId);

                        break;

                    case "9":
                        Console.WriteLine("__________CANCEL LAST OPERATION MENU__________");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAnsCancelOperation = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAnsCancelOperation == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string phNumCancelOperation = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (phNumCancelOperation == o.PhoneNumber)
                            {
                                centralBank.CancelLastOperation(o);
                                Console.WriteLine("You have successfully canceled last operation!");
                                Console.WriteLine(o.LastOperation.MoneyAmount + "returned to " + o.LastOperation.SendAccountId);
                            }
                        }

                        break;

                    case "10":
                        Console.WriteLine("Skipping one month for credit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns3 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns3 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph3 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph3 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your credit accounts: ");
                                o.GetListCreditAccounts();
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.Accounts)
                                {
                                    if (a == cr.GetAccountId())
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case "11":
                        Console.WriteLine("Skipping one month for debit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns4 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns4 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph4 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph4 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your debit accounts: ");
                                o.GetListDebitAccounts();
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.Accounts)
                                {
                                    if (a == cr.GetAccountId())
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case "12":
                        Console.WriteLine("Skipping one month for deposit account...");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns5 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns5 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph5 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph5 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your deposit accounts: ");
                                o.GetListDepositAccounts();
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.Accounts)
                                {
                                    if (a == cr.GetAccountId())
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case "13":
                        centralBank.GetListOfAllBanks();
                        break;

                    case "14":
                        Console.WriteLine("CLIENT'S INFORMATION:");
                        Console.WriteLine("Choose bank:");
                        Console.WriteLine("All available banks:");
                        centralBank.GetListOfAllBanks();
                        Console.Write("Enter bank:");
                        string bankAns6 = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Choose client:");
                        foreach (var o in centralBank.Banks)
                        {
                            if (bankAns6 == o.BankName)
                            {
                                List<BankClient> clients = o.GetClientsList();
                                foreach (var j in clients)
                                {
                                    Console.WriteLine("Surname: " + j.SecondName + ' ' + "Phone number: " + j.PhoneNumber);
                                }
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph6 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph6 == o.PhoneNumber)
                            {
                                o.GetClientsInfo(o);
                            }
                        }

                        break;

                    case "15":
                        Console.WriteLine("You have successfully drank a beer.");
                        Console.WriteLine("You are awesome!");
                        break;
                }
            }
        }
    }
}
