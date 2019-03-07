using BudgettingTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingTools.Templates
{
    public class AUser : BankComponent, IUser
    {
        #region Properties

        /// <summary>
        /// The list of accounts that belong to this user
        /// </summary>
        protected ICollection<IAccount> Accounts { get; private set; } = new List<IAccount>();

        /// <summary>
        /// The list of expenses that belong to this user
        /// </summary>
        protected ICollection<IExpense> Expenses { get; private set; } = new List<IExpense>();

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new user using the given data
        /// </summary>
        /// <param name="Name">String name for the account</param>
        /// <param name="accounts">A list of accounts to give to the user</param>
        /// <param name="expenses">A list of expenses to give to the user</param>
        public AUser(string Name = "", ICollection<IAccount> accounts = null, ICollection<IExpense> expenses = null)
        {
            Title = Name;

            Accounts = accounts ?? new List<IAccount>();

            Expenses = expenses ?? new List<IExpense>();
        }

        #endregion

        #region Methods

        #region Accounts

        public bool AddAccount(IAccount acct)
        {
            if (Accounts.Contains(acct))
                return false;
            else
                Accounts.Add(acct);
            return true;
        }

        public bool AddExpense(IExpense expense)
        {
            if (Expenses.Contains(expense))
                return false;
            else
                Expenses.Add(expense);
            return true;
        }

        #region Account Wizard

        public void CreateAccount()
        {
            AccountWizard wizard = new AccountWizard();
            wizard.AcceptedEvent += AcountWizard_AcceptedEvent;
            wizard.ShowDialog();
        }

        private void AcountWizard_AcceptedEvent(object sender, EventArgs e)
        {
            AddAccount(((AccountWizard)sender).Value);
        }

        #endregion

        public int CreateAccountInline(string title, string description = "", double initialBalance = 0)
        {
            AAccount acct = new AAccount(initialBalance)
            {
                Title = title,
                Description = description
            };
            AddAccount(acct);
            return acct.ID;
        }

        public IAccount findAccount(int id)
        {
            AAccount comp = new AAccount();
            comp.AssignID(id);
            if (Accounts.Contains(comp))
            {
                int index = Accounts.ToList().IndexOf(comp);
                return Accounts.ElementAt(index);
            }
            else
                return null;
        }

        #endregion

        #region Expenses

        #region Expense Wizard

        public void CreateExpense()
        {
            ExpenseWizard wizard = new ExpenseWizard(Accounts);
            wizard.AcceptedEvent += ExpenseWizard_AcceptedEvent;
            wizard.NewAccountEvent += ExpenseWizard_NewAccountEvent;
            wizard.ShowDialog();
        }

        private void ExpenseWizard_NewAccountEvent(object sender, EventArgs e)
        {
            AddAccount((IAccount)sender);
        }

        private void ExpenseWizard_AcceptedEvent(object sender, EventArgs e)
        {
            AddExpense(((ExpenseWizard)sender).Value);
        }

        #endregion

        public int CreateExpenseInline(string title, string description, double fee, int acct, ExpenseRepetition repetition = ExpenseRepetition.None, RepetitionPosition position = null)
        {
            if(findAccount(acct) == null)
            {
                AAccount account = new AAccount();
                account.AssignID(acct);
                if (!AddAccount(account))
                    throw new InvalidOperationException("Account didn't exist and creation of new account failed!");
            }

            AExpense exp = new AExpense(fee, findAccount(acct), repetition, position);
            AddExpense(exp);
            return exp.ID;
        }

        public int CreateExpenseInline(string title, string description, double fee, IEnumerable<int> accts, ExpenseRepetition repetition = ExpenseRepetition.None, RepetitionPosition position = null)
        {
            List<IAccount> accounts = new List<IAccount>(accts.Count());

            foreach (int acct in accts)
            {
                if (findAccount(acct) == null)
                {
                    AAccount account = new AAccount();
                    account.AssignID(acct);
                    if (!AddAccount(account))
                        throw new InvalidOperationException("Account didn't exist and creation of new account failed!");
                }

                accounts.Add(findAccount(acct));
            }

            AExpense exp = new AExpense(fee, accounts, repetition, position);
            AddExpense(exp);
            return exp.ID;
        }

        public IExpense findExpense(int id)
        {
            AExpense comp = new AExpense();
            comp.AssignID(id);
            if (Expenses.Contains(comp))
            {
                int index = Expenses.ToList().IndexOf(comp);
                return Expenses.ElementAt(index);
            }
            else
                return null;
        }

        #endregion

        public IAccount[] GetAccounts()
        {
            return Accounts.ToArray();
        }

        public IExpense[] GetExpenses()
        {
            return Expenses.ToArray();
        }

        public bool RemoveAccount(string title)
        {
            IAccount account = null;

            foreach(IAccount acct in Accounts)
                if(acct.ToString().Contains(title))
                {
                    account = acct;
                    break;
                }

            if (account == null)
                return false;

            Accounts.Remove(account);
            return true;
        }

        public bool RemoveAccount(int id)
        {
            if (findAccount(id) == null)
                return false;
            else
            {
                Accounts.Remove(findAccount(id));
                return true;
            }
        }

        public bool RemoveExpense(string title)
        {
            IExpense account = null;

            foreach (IExpense acct in Expenses)
                if (acct.ToString().Contains(title))
                {
                    account = acct;
                    break;
                }

            if (account == null)
                return false;

            Expenses.Remove(account);
            return true;
        }

        public bool RemoveExpense(int id)
        {
            if (findExpense(id) == null)
                return false;
            else
            {
                Expenses.Remove(findExpense(id));
                return true;
            }
        }

        #endregion
    }
}
