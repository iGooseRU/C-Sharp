using System;
using Banks.Account;
using Banks.Builder;
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

            string ch = "y";
            while (ch == "y" || ch == "Y")
            {
                var printTool = new PrintTool();
                printTool.PrintStartMenu();
                Console.Write("Enter your answer:");

                int ans = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("__________________________________");
                Console.WriteLine();
                switch (ans)
                {
                    default:
                        throw new BanksException("Unknown answer. Try again.");
                    case 15:
                        return;

                    case 1:
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

                    case 2:
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

                    case 3:
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
                                o.GetClientsList();
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

                    case 4:
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
                                o.GetClientsList();
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

                    case 5:
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
                                o.GetClientsList();
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

                    case 6:
                        // top up money
                        break;

                    case 7:
                        // withdraw
                        break;

                    case 8:
                        // money transfer
                        break;

                    case 9:
                        // cancel last money transfer
                        break;

                    case 10:
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
                                o.GetClientsList();
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph3 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph3 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your credit accounts: ");
                                o.GetListCreditAccounts(o);
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.CreditAccounts)
                                {
                                    if (a == cr.AccountId)
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case 11:
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
                                o.GetClientsList();
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph4 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph4 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your debit accounts: ");
                                o.GetListDebitAccounts(o);
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.DebitAccounts)
                                {
                                    if (a == cr.AccountId)
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case 12:
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
                                o.GetClientsList();
                            }
                        }

                        Console.Write("Enter client's phone number:");
                        string ph5 = Console.ReadLine();

                        foreach (var o in centralBank.AllClients)
                        {
                            if (ph5 == o.PhoneNumber)
                            {
                                Console.WriteLine("Your deposit accounts: ");
                                o.GetListDepositAccounts(o);
                                Console.Write("Enter account id: ");
                                string a = Console.ReadLine();

                                foreach (var cr in o.DepositAccounts)
                                {
                                    if (a == cr.AccountId)
                                    {
                                        cr.SkipMonth();
                                        Console.WriteLine("You have successfully skipped one month");
                                    }
                                }
                            }
                        }

                        break;

                    case 13:
                        centralBank.GetListOfAllBanks();
                        break;

                    case 14:
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
                                o.GetClientsList();
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
                }
            }
        }
    }
}
