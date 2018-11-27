using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgettingTools.Interfaces;

namespace BudgettingTools.Templates
{
    /// <summary>
    /// Represents a template for a basic bank account with deposit, withdrawal, and balance checking functions
    /// </summary>
    public class AAccount : BankComponent, IAccount
    {
        #region Properties

        /// <summary>
        /// Contains the current balance of the account
        /// </summary>
        protected double Balance { get; set; }

        #endregion

        #region Events

        public delegate void BalanceChangeEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the balance of this account changes.
        /// </summary>
        public event BalanceChangeEventHandler BalanceChangeEvent;
        protected void OnBalanceChangeEvent()
        {
            BalanceChangeEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Account using the given balance or zero
        /// </summary>
        /// <param name="initBal">[Optional] Initial balance to use (0)</param>
        public AAccount(double initBal = 0)
        {
            Balance = initBal;
            OnBalanceChangeEvent();
        }

        #endregion

        #region Methods

        public double CheckBalance()
        {
            return Balance;
        }

        public void Deposit(double amt)
        {
            if (amt < 0)
                throw new InvalidOperationException("Cannot deposit a negative value");

            Balance += amt;
            OnBalanceChangeEvent();
        }

        public bool Withdraw(double amt)
        {
            if (amt < 0)
                throw new InvalidOperationException("Cannot withdraw a negative value");

            if (Balance - amt < 0)
            {
                OnBalanceChangeEvent();
                return false;
            }
            Balance -= amt;
            OnBalanceChangeEvent();
            return true;
        }

        #endregion
    }
}
