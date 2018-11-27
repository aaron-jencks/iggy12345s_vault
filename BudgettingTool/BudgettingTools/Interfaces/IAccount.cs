using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Interfaces
{
    /// <summary>
    /// An interface used to simulate a bank account
    /// </summary>
    public interface IAccount
    {
        /// <summary>
        /// Deposits amt into the account
        /// </summary>
        /// <param name="amt">Amount to deposit</param>
        void Deposit(double amt);

        /// <summary>
        /// Withdraws an amount from the account
        /// </summary>
        /// <param name="amt">Amount to withdraw from the account</param>
        /// <returns>Returns true if the withdrawal was successfull, and false otherwise</returns>
        bool Withdraw(double amt);

        /// <summary>
        /// Gets the current account balance
        /// </summary>
        /// <returns>Returns the current account balance</returns>
        double CheckBalance();
    }
}
