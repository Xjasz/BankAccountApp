using System;
using System.Collections.Generic;
using Xunit;

namespace App.UnitTest
{
    public class UnitTest
    {
        private readonly Bank _Bank;

        public UnitTest()
        {
            List<Account> list = new List<Account>{
                new Checking("Samantha"),
                new CorporateInvestment("Tyler", 100),
                new IndividualInvestment("Nick", 200)
            };
            _Bank = new Bank(list);
        }

        [Fact]
        public void CanDeposit()
        {
            Console.WriteLine("TEST ---- CanDeposit");

            int oldBalance1 = _Bank.GetAccountBalance("Samantha");
            int newBalance1 = _Bank.Deposit("Samantha", 100);
            Assert.NotEqual(oldBalance1, newBalance1);

            int oldBalance2 = _Bank.GetAccountBalance("Tyler");
            int newBalance2 = _Bank.Deposit("Tyler", 200);
            Assert.NotEqual(oldBalance2, newBalance2);

            int oldBalance3 = _Bank.GetAccountBalance("Nick");
            int newBalance3 = _Bank.Deposit("Nick", 300);
            Assert.NotEqual(oldBalance3, newBalance3);
        }

        [Fact]
        public void CanWithdraw()
        {
            Console.WriteLine("TEST ---- CanWithdraw");

            _Bank.Deposit("Samantha", 100);
            int result1 = _Bank.Withdraw("Samantha", 50);
            Assert.Equal(50, result1);

            _Bank.Deposit("Tyler", 100);
            int result2 = _Bank.Withdraw("Tyler", 75);
            Assert.Equal(75, result2);

            _Bank.Deposit("Nick", 200);
            int result3 = _Bank.Withdraw("Nick", 100);
            Assert.Equal(100, result3);
        }

        [Fact]
        public void CanTransfer()
        {
            Console.WriteLine("TEST ---- CanTransfer");

            _Bank.Deposit("Samantha", 100);
            int oldBalance1 = _Bank.GetAccountBalance("Samantha");
            int oldBalance2 = _Bank.GetAccountBalance("Tyler");

            bool result = _Bank.TransferFunds("Samantha", "Tyler", 25);
            int newBalance1 = _Bank.GetAccountBalance("Samantha");
            int newBalance2 = _Bank.GetAccountBalance("Tyler");

            Assert.True(result);
            Assert.NotEqual(oldBalance1, newBalance1);
            Assert.NotEqual(oldBalance2, newBalance2);
        }

        [Fact]
        public void CannotTransfer()
        {
            Console.WriteLine("TEST ---- CannotTransfer");

            int oldBalance1 = _Bank.GetAccountBalance("Tyler");
            int oldBalance2 = _Bank.GetAccountBalance("Nick");

            bool result = _Bank.TransferFunds("Tyler", "Nick", 777777);
            int newBalance1 = _Bank.GetAccountBalance("Tyler");
            int newBalance2 = _Bank.GetAccountBalance("Nick");

            Assert.False(result);
            Assert.Equal(oldBalance1, newBalance1);
            Assert.Equal(oldBalance2, newBalance2);
        }

        [Fact]
        public void HasIndividualWithdrawLimit()
        {
            Console.WriteLine("TEST ---- HasIndividualWithdrawLimit");

            _Bank.Deposit("Nick", 1111);
            int balanceAfter = _Bank.Withdraw("Nick", 1111);
            Assert.Equal(0, balanceAfter);
        }
    }
}
