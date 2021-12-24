using System;

namespace Banks
{
    public class PrintTool
    {
        public void PrintStartMenu()
        {
            Console.WriteLine("__________CENTRAL BANK MENU__________");
            Console.WriteLine("________WHAT DO YOU WANT TO DO?________");
            Console.WriteLine("1. Create bank"); // done
            Console.WriteLine("2. Create client"); // done
            Console.WriteLine("3. Open credit account for client"); // done
            Console.WriteLine("4. Open debit account for client"); // done
            Console.WriteLine("5. Open deposit account for client"); // done
            Console.WriteLine("6. Top up money"); // done
            Console.WriteLine("7. Withdraw money"); // done
            Console.WriteLine("8. Complete money transfer"); // done
            Console.WriteLine("9. Cancel money transfer"); // done
            Console.WriteLine("10. Skip one month for credit"); // done
            Console.WriteLine("11. Skip one month for debit"); // done
            Console.WriteLine("12. Skip one month for deposit"); // done
            Console.WriteLine("13. Get list of all banks"); // done
            Console.WriteLine("14. Get client information"); // done
            Console.WriteLine("15. Drink beer"); // processed
            Console.WriteLine("Exit. Shutdown"); // done
            Console.WriteLine("__________________________________");
            Console.WriteLine();
        }
    }
}