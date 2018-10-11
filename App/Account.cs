using System;

namespace App
{
    public enum AccountType
    {
        Checking,
        IndividualInvestment,
        CorporateInvestment
    }

    public class Account
    {
        public string Owner { get; private set; }
        public int Balance { get; private set; }

        public Account(string owner, int balance)
        {
            Owner = owner;
            Balance = balance;
        }

        public int Deposit(int amount)
        {
            Console.WriteLine("Performing Deposit");
            Balance = Balance + amount;
            return Balance;
        }

        public int Withdraw(int amount)
        {
            Console.WriteLine("Performing Withdraw");
            if (IsInvalidRequest(amount))
            {
                return 0;
            }
            Balance = Balance - amount;
            return amount;
        }

        public bool Transfer(int amount)
        {
            Console.WriteLine("Performing Transfer");
            if (IsInvalidRequest(amount))
            {
                return false;
            }
            Balance = Balance - amount;
            return true;
        }

        private bool IsInvalidRequest(int amount)
        {
            if (Balance < amount)
            {
                Console.WriteLine("Canceling to prevent overdraft.");
                return true;
            }
            else if (GetAccountType().Equals(AccountType.IndividualInvestment) && amount > 1000)
            {
                Console.WriteLine("IndividualInvestment accounts have a 1000 limit.");
                return true;
            }
            return false;
        }
        public virtual AccountType GetAccountType()
        {
            throw new NotImplementedException();
        }
    }

    public class Checking : Account
    {
        public Checking(string owner, int balance = 0) : base(owner, balance) { }

        public override AccountType GetAccountType()
        {
            return AccountType.Checking;
        }
    }
    public class IndividualInvestment : Account
    {
        public IndividualInvestment(string owner, int balance = 0) : base(owner, balance) { }

        public override AccountType GetAccountType()
        {
            return AccountType.IndividualInvestment;
        }
    }
    public class CorporateInvestment : Account
    {
        public CorporateInvestment(string owner, int balance = 0) : base(owner, balance) { }
        public override AccountType GetAccountType()
        {
            return AccountType.CorporateInvestment;
        }
    }
}
