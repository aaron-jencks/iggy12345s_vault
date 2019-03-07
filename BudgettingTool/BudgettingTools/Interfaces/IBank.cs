using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Interfaces
{
    /// <summary>
    /// An interface for a bank of users and their respective accounts
    /// </summary>
    public interface IBank
    {
        /// <summary>
        /// Gets all of the users in the bank
        /// </summary>
        /// <returns>Returns the list of all users that are stored in the bank</returns>
        IUser[] GetAllUsers();

        /// <summary>
        /// Gets all of the accounts of all of the users in the bank
        /// </summary>
        /// <returns>Returns all of the accounts of all of the users concatenated in the bank</returns>
        IAccount[] GetAllAccounts();

        /// <summary>
        /// Gets all of the expenses of all of the users in the bank
        /// </summary>
        /// <returns>Returns all of the expenses of all of the users concatenated in the bank</returns>
        IExpense[] GetAllExpenses();

        /// <summary>
        /// Adds a new user to the bank
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>Returns true if the user was added, returns false if the user already exists</returns>
        bool AddUser(IUser user);

        /// <summary>
        /// Removes a user from the bank
        /// </summary>
        /// <param name="id">The integer id of the user to remove</param>
        /// <returns>Returns true if the user was removed, returns false if the user never existed</returns>
        bool RemoveUser(int id);

        /// <summary>
        /// Removes a user from the bank
        /// </summary>
        /// <param name="title">The string title of the user to remove</param>
        /// <returns>Returns true if the user was removed, returns false if the user never existed</returns>
        bool RemoveUser(string title);

        /// <summary>
        /// Finds the user with the corresponding integer id
        /// </summary>
        /// <param name="id">The integer id of the user to find</param>
        /// <returns>Returns the user that was found, returns null if it wasn't found</returns>
        IUser findUser(int id);

        /// <summary>
        /// Launches a wizard to create a new user
        /// </summary>
        void CreateUser();

        /// <summary>
        /// Creates a new user and returns its id
        /// </summary>
        /// <param name="name">string title to give to the user</param>
        /// <param name="description">[optional] string description to give the user ("")</param>
        /// <param name="accts">[optional] List of accounts to initialize the user with (null)</param>
        /// <param name="expenses">[optional] List of expenses to initialize the user with (null)</param>
        /// <returns>Returns the integer id that the new user was assigned</returns>
        int CreateUserInline(string name, string description = "", IEnumerable<IAccount> accts = null, IEnumerable<IExpense> expenses = null);

        /// <summary>
        /// Creates a new user and returns its id
        /// </summary>
        /// <param name="name">string title to give to the user</param>
        /// <param name="description">[optional] string description to give the user ("")</param>
        /// <param name="acct">[optional] Account to initialize the user with (null)</param>
        /// <param name="expenses">[optional] List of expenses to initialize the user with (null)</param>
        /// <returns>Returns the integer id that the new user was assigned</returns>
        int CreateUserInline(string name, string description = "", IAccount acct = null, IEnumerable<IExpense> expenses = null);

        /// <summary>
        /// Creates a new user and returns its id
        /// </summary>
        /// <param name="name">string title to give to the user</param>
        /// <param name="description">[optional] string description to give the user ("")</param>
        /// <param name="accts">[optional] List of accounts to initialize the user with (null)</param>
        /// <param name="expense">[optional] Expense to initialize the user with (null)</param>
        /// <returns>Returns the integer id that the new user was assigned</returns>
        int CreateUserInline(string name, string description = "", IEnumerable<IAccount> accts = null, IExpense expense = null);

        /// <summary>
        /// Creates a new user and returns its id
        /// </summary>
        /// <param name="name">string title to give to the user</param>
        /// <param name="description">[optional] string description to give the user ("")</param>
        /// <param name="acct">[optional] Account to initialize the user with (null)</param>
        /// <param name="expense">[optional] Expense to initialize the user with (null)</param>
        /// <returns>Returns the integer id that the new user was assigned</returns>
        int CreateUserInline(string name, string description = "", IAccount acct = null, IExpense expense = null);
    }
}
