using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgettingTools.Interfaces;

namespace BudgettingTools.Templates
{
    /// <summary>
    /// Represents a template for an expenditure
    /// </summary>
    public class AExpense : AAccount, IExpense
    {
        #region Properties

        /// <summary>
        /// The list of assigned account that this expense withdraws from upon calculation
        /// </summary>
        public ICollection<IAccount> AssignedAccounts { get; private set; } = new List<IAccount>();

        /// <summary>
        /// Amount to remove from each of the assigned accounts upon calculation
        /// </summary>
        public double Rate { get; set; } = 0;

        /// <summary>
        /// The frequency of how often this expense repeats defaults to None
        /// </summary>
        public ExpenseRepetition RepetitionFrequency { get; set; } = ExpenseRepetition.None;

        /// <summary>
        /// Summarizes the repetition preferences of the this expense
        /// </summary>
        public RepetitionPosition repetitionPosition { get; set; } = new RepetitionPosition();

        /// <summary>
        /// The next calculation day
        /// </summary>
        protected DateTime NextCalculation { get; set; } = new DateTime();

        /// <summary>
        /// Previous date that this expense was calculated on
        /// </summary>
        protected DateTime prevCalculation { get; set; } = new DateTime();

        #endregion

        #region Events

        public delegate void OnCalculateEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered whenever this expense is calculated.
        /// </summary>
        public event OnCalculateEventHandler OnCalculateEvent;
        protected void OnOnCalculateEvent()
        {
            OnCalculateEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new expense object with the given rate, account and frequency
        /// </summary>
        /// <param name="rate">Rate to withdraw from the specified account</param>
        /// <param name="acct">Account to withdraw the rate from</param>
        /// <param name="frequency">How often to withdraw the rate from the account</param>
        /// <param name="repPosition">What day, minute, hour, second, weekday, etc... to repeat this rate on</param>
        public AExpense(double rate = 0, IAccount acct = null, ExpenseRepetition frequency = ExpenseRepetition.None, RepetitionPosition repPosition = null) : base()
        {
            Rate = rate;
            AssignAccount(acct ?? new AAccount());
            if (frequency != ExpenseRepetition.None)
            {
                RepetitionFrequency = frequency;
                repetitionPosition = repPosition ?? new RepetitionPosition();
            }
        }

        /// <summary>
        /// Creates a new expense object with the given rate, accounts and frequency
        /// </summary>
        /// <param name="rate">Rate to withdraw from the specified account</param>
        /// <param name="accts">Accounts to withdraw the rate from</param>
        /// <param name="frequency">How often to withdraw the rate from the account</param>
        /// <param name="repPosition">What day, minute, hour, second, weekday, etc... to repeat this rate on</param>
        public AExpense(double rate, ICollection<IAccount> accts, 
            ExpenseRepetition frequency = ExpenseRepetition.None, RepetitionPosition repPosition = null) : base()
        {
            Rate = rate;
            foreach (IAccount acct in accts)
                AssignAccount(acct);
            if(frequency != ExpenseRepetition.None)
            {
                RepetitionFrequency = frequency;
                repetitionPosition = repPosition ?? new RepetitionPosition();
            }
        }

        #endregion

        #region Methods

        public void AssignAccount(IAccount acct)
        {
            if (!AssignedAccounts.Contains(acct))
                AssignedAccounts.Add(acct);
        }

        public List<bool> Calculate()
        {
            List<bool> temp = new List<bool>();
            foreach (IAccount acct in AssignedAccounts)
            {
                if (acct.Withdraw(Rate))
                {
                    temp.Add(true);
                    Deposit(Rate);
                }
                else
                    temp.Add(false);
            }
            OnOnCalculateEvent();
            return temp;
        }

        public bool CalculateGeneral()
        {
            foreach (IAccount acct in AssignedAccounts)
                if (!acct.Withdraw(Balance))
                {
                    OnOnCalculateEvent();
                    return false;
                }
                else
                    Deposit(Rate);
            OnOnCalculateEvent();
            return true;
        }

        public DateTime GetNextDueDate()
        {
            return NextCalculation;
        }

        public void TransferExcess(IAccount acct)
        {
            if(CheckBalance() > AssignedAccounts.Count * Rate)
            {
                double diff = Balance - (AssignedAccounts.Count * Rate);
                Withdraw(diff);
                acct.Deposit(diff);
            }
        }

        public bool CalculationDue()
        {
            return DateTime.Now.CompareTo(NextCalculation) > 0;
        }

        public void UpdateDueDate()
        {
            switch(RepetitionFrequency)
            {
                case ExpenseRepetition.Daily:
                    NextCalculation.AddDays(1);
                    break;
                case ExpenseRepetition.Hourly:
                    NextCalculation.AddHours(1);
                    break;
                case ExpenseRepetition.Minutely:
                    NextCalculation.AddMinutes(1);
                    break;
                case ExpenseRepetition.Monthly:
                    NextCalculation.AddMonths(1);
                    break;
                case ExpenseRepetition.Weekly:
                    NextCalculation.AddDays(7);
                    break;
                case ExpenseRepetition.Yearly:
                    NextCalculation.AddYears(1);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
