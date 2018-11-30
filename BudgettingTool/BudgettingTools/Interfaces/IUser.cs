using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Interfaces
{
    /// <summary>
    /// A simple interface used to impelement a user, or a collection of accounts and expenses.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets all of the accounts in the user
        /// </summary>
        /// <returns>Returns all of the accounts belonging to this user</returns>
        IAccount[] GetAccounts();

        /// <summary>
        /// Adds an account to this user
        /// </summary>
        /// <param name="acct">The account to add</param>
        /// <returns>Returns a boolean indicator of success, returns true if the account didn't already exist, returns false otherwise</returns>
        bool AddAccount(IAccount acct);

        /// <summary>
        /// Creates a new account for this user
        /// </summary>
        /// <param name="title">Title of the account</param>
        /// <param name="description">[optional] Description of the account ("")</param>
        /// <param name="initialBalance">[optional] Initial Balance of the account (0)</param>
        /// <returns>Returns the integer id of the account that was created</returns>
        int CreateAccountInline(string title, string description = "", double initialBalance = 0);

        /// <summary>
        /// Launches the Account creating wizard to create a new account for this user
        /// </summary>
        void CreateAccount();

        /// <summary>
        /// Removes an account from this user
        /// </summary>
        /// <param name="title">String title of the account to remove</param>
        /// <returns>Returns true if the account was removed, returns false otherwise.</returns>
        bool RemoveAccount(string title);

        /// <summary>
        /// Removes an account from this user
        /// </summary>
        /// <param name="id">The integer id of the account to remove</param>
        /// <returns>Returns true if the account was removed, returns false otherwise.</returns>
        bool RemoveAccount(int id);

        /// <summary>
        /// Gets all of the expenses the belong to this user
        /// </summary>
        /// <returns>Returns an array of the expenses that belong to this user</returns>
        IExpense[] GetExpenses();

        /// <summary>
        /// Adds an expense to this user
        /// </summary>
        /// <param name="expense">The expense to add</param>
        /// <returns>Returns true if the expense didn't already exist, returns false otherwise.</returns>
        bool AddExpense(IExpense expense);

        /// <summary>
        /// Creates a new expense using the information given
        /// </summary>
        /// <param name="title">Title of the expense</param>
        /// <param name="description">Description of the expense</param>
        /// <param name="fee">The fee/rate of the expense</param>
        /// <param name="acct">The integer id of the account to link this expense to</param>
        /// <param name="repetition">[optional] The repetition type for this expense (None)</param>
        /// <param name="position">[optional] The position to repeat on for this expense (null)</param>
        /// <returns>Returns the integer id of the expense that was created</returns>
        int CreateExpenseInline(string title, string description, double fee, int acct,
            ExpenseRepetition repetition = ExpenseRepetition.None, RepetitionPosition position = null);

        /// <summary>
        /// Creates a new expense using the information given
        /// </summary>
        /// <param name="title">Title of the expense</param>
        /// <param name="description">Description of the expense</param>
        /// <param name="fee">The fee/rate of the expense</param>
        /// <param name="accts">The list of integer ids of the accounts to link this expense to</param>
        /// <param name="repetition">[optional] The repetition type for this expense (None)</param>
        /// <param name="position">[optional] The position to repeat on for this expense (null)</param>
        /// <returns>Returns the integer id of the expense that was created</returns>
        int CreateExpenseInline(string title, string description, double fee, IEnumerable<int> accts,
            ExpenseRepetition repetition = ExpenseRepetition.None, RepetitionPosition position = null);

        /// <summary>
        /// Launches the Expense wizard to create a new expense for this wizard.
        /// </summary>
        void CreateExpense();

        /// <summary>
        /// Removes an expense from the user
        /// </summary>
        /// <param name="title">The String title of the expense to remove</param>
        /// <returns>Returns true if the expense was removed, returns false otherwise</returns>
        bool RemoveExpense(string title);

        /// <summary>
        /// Removes an expense from the user
        /// </summary>
        /// <param name="id">The integer id used to identify the expense to remove</param>
        /// <returns>Returns true if the expense was removed, returns false otherwise.</returns>
        bool RemoveExpense(int id);

        /// <summary>
        /// Finds the specified account in the list of accounts that belong to the user
        /// Only works if the account specified extends the bank component class
        /// </summary>
        /// <param name="id">The integer id to search for</param>
        /// <returns>Returns the account if it finds one, or null otherwise</returns>
        IAccount findAccount(int id);

        /// <summary>
        /// Finds the specified expense in the list of expenses that belong to the user
        /// Only works if the expense specified extends the bank component class
        /// </summary>
        /// <param name="id">The integer id to search for</param>
        /// <returns>Returns the expense if it finds one, or null otherwise</returns>
        IExpense findExpense(int id);
    }
}
