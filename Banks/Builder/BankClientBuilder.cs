using System;
using System.IO;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Builder
{
    public class BankClientBuilder : IBuilder
    {
        private const int PhoneNumberLength = 11;
        private const int PassportDataMinLength = 10;
        private const int PassportDataMaxLength = 11;

        private BankClient _client = new BankClient();

        public BankClientBuilder()
        {
            Reset();
        }

        public void RegisterPhoneNumber()
        {
            Console.WriteLine("Enter your phone number (11 symbols) without '+' :");
            string str = Console.ReadLine();
            if (str == null || str.Length != PhoneNumberLength)
                throw new BanksException("Phone number must contain 11 symbols!");
            _client.PhoneNumber = str;
        }

        public void RegisterClientName()
        {
            Console.WriteLine("Enter you first name: ");
            string name = Console.ReadLine();
            if (name == null)
                throw new BanksException("You must enter your name! ");
            _client.Name = name;

            Console.WriteLine("Enter you second name: ");
            string surname = Console.ReadLine();
            if (surname == null)
                throw new BanksException("You must enter your surname! ");
            _client.Surname = surname;
        }

        public void RegisterClientPassportData()
        {
            Console.WriteLine("Enter you passport data: ");
            Console.WriteLine("You can skip this step, but your account will be questionable");
            Console.WriteLine("It means that it will be impossible to withdraw money and you will have a limit to money transfer ");
            Console.WriteLine("Skip? (y/n)");

            char cnt = Console.ReadKey(true).KeyChar;

            if (cnt == 'y')
            {
                Console.WriteLine("Your account is questionable!");
                _client.PassportNumber = null;
                _client.AccountStatus = false;
            }
            else if (cnt == 'n')
            {
                string passportData = Console.ReadLine();
                if (passportData == null)
                    throw new BanksException("You must enter correct passport data! ");
                if (passportData.Length != PassportDataMinLength || passportData.Length != PassportDataMaxLength)
                    throw new BanksException("Passport number must contain 10 symbols");

                _client.PassportNumber = passportData;
                _client.AccountStatus = true;
            }
            else
            {
                Console.WriteLine("Please, start again");
            }
        }

        public BankClient GetClient()
        {
            BankClient result = _client;
            this.Reset();

            Console.WriteLine();
            Console.WriteLine("You have successfully registered in bank service!");
            Console.WriteLine("Your name: " + result.Name + ' ' + result.Surname);
            Console.WriteLine("Your phone number: " + result.PhoneNumber);
            if (!result.AccountStatus)
            {
                Console.WriteLine("No data about passport");
                Console.WriteLine("Your account is questionable. Opportunities on your account are limited");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Your passport number: " + result.PassportNumber);
                Console.WriteLine();
            }

            return result;
        }

        private void Reset()
        {
            _client = new BankClient();
        }
    }
}