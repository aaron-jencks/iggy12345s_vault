using BudgettingTools.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgettingTools.Templates
{
    public partial class ExpenseWizard : Form
    {
        #region Properties

        /// <summary>
        /// Contains the Expense that this wizard was tasked to create.
        /// </summary>
        public AExpense Value { get; protected set; } = new AExpense();

        #endregion

        #region Events

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the value property changes.
        /// </summary>
        public event ValueChangedEventHandler ValueChangedEvent;
        protected void OnValueChangedEvent()
        {
            ValueChangedEvent?.Invoke(this, new EventArgs());
        }

        public delegate void AcceptedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the value property changes.
        /// </summary>
        public event AcceptedEventHandler AcceptedEvent;
        protected void OnAcceptedEvent()
        {
            AcceptedEvent?.Invoke(this, new EventArgs());
        }

        public delegate void NewAccountEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that a new account is created using the account linking wizard
        /// </summary>
        public event AcceptedEventHandler NewAccountEvent;
        protected void OnNewAccountEvent(IAccount acct)
        {
            AcceptedEvent?.Invoke(acct, new EventArgs());
        }

        #endregion

        #region Constructors

        public ExpenseWizard()
        {
            InitializeComponent();
        }

        public ExpenseWizard(ICollection<IAccount> globalAccountsList)
        {
            aExpenseForm1 = new FormsControls.AExpenseForm(globalAccountsList);
            aExpenseForm1.NewAccountEvent += AExpenseForm1_NewAccountEvent;
            InitializeComponent();
        }

        #endregion

        #region Methods

        private void AExpenseForm1_NewAccountEvent(object sender, EventArgs e)
        {
            OnNewAccountEvent((IAccount)sender);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            OnAcceptedEvent();
            Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void aExpenseForm1_ValueChangedEvent(object sender, EventArgs e)
        {
            OnValueChangedEvent();
        }

        #endregion
    }
}
