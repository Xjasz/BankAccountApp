using System;
using System.Collections.Generic;

namespace App
{
    public class Bank
    {
        private readonly List<Account> _accounts;

        public Bank(List<Account> accounts)
        {
            _accounts = accounts;
        }

        public bool TransferFunds(string from, string to, int amount)
        {
            bool transferSuccess = false;
            var toAccount = GetAccount(to);
            var fromAccount = GetAccount(from);
            if (toAccount == null)
            {
                Console.WriteLine("Transfer account doesn't exist.");
            }
            else if (fromAccount.Transfer(amount))
            {
                toAccount.Deposit(amount);
                transferSuccess = true;
            }
            return transferSuccess;
        }

        public int GetAccountBalance(string name)
        {
            foreach (var item in _accounts)
            {
                if (item.Owner.Equals(name))
                {
                    return item.Balance;
                }
            }
            return 0;
        }

        public int Deposit(string name, int amount)
        {
            foreach (var item in _accounts)
            {
                if (item.Owner.Equals(name))
                {
                    return item.Deposit(amount);
                }
            }
            return 0;
        }

        public int Withdraw(string name, int amount)
        {
            foreach (var item in _accounts)
            {
                if (item.Owner.Equals(name))
                {
                    return item.Withdraw(amount);
                }
            }
            return 0;
        }

        private Account GetAccount(string name)
        {
            foreach (var item in _accounts)
            {
                if (item.Owner.Equals(name))
                {
                    return item;
                }
            }
            return null;
        }
    }

}