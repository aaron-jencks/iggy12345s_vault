using BudgettingTools.Interfaces;
using BudgettingTools.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgettingTools
{
    public partial class AccountLinkingWizard : Form
    {
        #region Properties

        public List<IAccount> Value { get; set; } = new List<IAccount>();

        #endregion

        #region Events

        public delegate void AcceptedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the accept button is pressed.
        /// </summary>
        public event AcceptedEventHandler AcceptedEvent;
        protected void OnAcceptedEvent()
        {
            AcceptedEvent?.Invoke(this, new EventArgs());
        }

        public delegate void NewAccountEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the accept button is pressed.
        /// </summary>
        public event NewAccountEventHandler NewAccountEvent;
        protected void OnNewAccountEvent(IAccount acct)
        {
            NewAccountEvent?.Invoke(acct, new EventArgs());
        }

        #endregion

        public AccountLinkingWizard()
        {
            InitializeComponent();
        }

        public AccountLinkingWizard(ICollection<IAccount> globalAccounts)
        {
            InitializeComponent();
            foreach (IAccount acct in globalAccounts)
                listBoxAccounts.Items.Add(acct);
        }

        private void buttonNewAccount_Click(object sender, EventArgs e)
        {
            AccountWizard temp = new AccountWizard();
            temp.AcceptedEvent += Temp_AcceptedEvent;
            temp.ShowDialog();
        }

        private void Temp_AcceptedEvent(object sender, EventArgs e)
        {
            listBoxAccounts.Items.Add(((AccountWizard)sender).Value);
            OnNewAccountEvent((IAccount)(listBoxAccounts.Items[listBoxAccounts.Items.Count - 1]));
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            foreach (object o in listBoxAccounts.SelectedItems)
                Value.Add((IAccount)o);
            OnAcceptedEvent();
            Dispose();
        }
    }
}
