using Banks.Entities;

namespace Banks.Account
{
    public interface IAccount
    {
        void TopUpMoney(double moneyAmount);
        void MoneyWithdraw(int moneyAmount);
        void MakeOperation(int moneyAmount, BankClient recieveClient, string sendAccountId);
        void CancelOperation();
        string GetAccountId();
        double GetMoneyAmount();
        bool GetAccountStatus();
        void PrintAccountInfo();
        void SkipMonth();
    }
}