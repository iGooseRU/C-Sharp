using Banks.Entities;

namespace Banks.Account
{
    public interface IAccount
    {
        void TopUpMoney(double moneyAmount);
        void MoneyWithdraw(int moneyAmount);
        string GetAccountId();
        double GetMoneyAmount();
        bool GetAccountStatus();
        void GetAccountInfo();
        void SkipMonth();
    }
}