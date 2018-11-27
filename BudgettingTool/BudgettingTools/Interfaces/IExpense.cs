using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Interfaces
{
    /// <summary>
    /// An interface to simulate an expenditure
    /// </summary>
    public interface IExpense
    {
        /// <summary>
        /// Assigns an account to be charged to when the expenditure is calculated
        /// </summary>
        /// <param name="acct">The account to assign to</param>
        void AssignAccount(IAccount acct);

        /// <summary>
        /// Calculates the expenditure, removes it from the assigned account and returns true if there isn't a balance left over, otherwise returns false
        /// </summary>
        /// <returns>Returns true if the user does not owe money after removing the expenditure from the bank account assigned to it, returns false otherwise.</returns>
        List<bool> Calculate();

        /// <summary>
        /// Performs the same thing as Calculate(), except it returns true if and only if ALL accounts returned true
        /// </summary>
        /// <returns>Returns true if ALL accounts return true after withdrawing this balance from them, false otherwise</returns>
        bool CalculateGeneral();

        /// <summary>
        /// Transfers extra money that wasn't used to another bank account.
        /// </summary>
        /// <param name="acct">Account to transfer to</param>
        void TransferExcess(IAccount acct);

        /// <summary>
        /// Gets the next date that this expenditure should be calculated on.
        /// </summary>
        /// <returns>Returns a DateTime object specifying the next date that this expenditure is due</returns>
        DateTime GetNextDueDate();

        /// <summary>
        /// Determines if the expense is due for calculation
        /// </summary>
        /// <returns>Returns true if the expense needs to be calculated, false otherwise</returns>
        bool CalculationDue();

        /// <summary>
        /// Assumes a calculation was performed and updates the due date to the next iteration.
        /// </summary>
        void UpdateDueDate();
    }
}
