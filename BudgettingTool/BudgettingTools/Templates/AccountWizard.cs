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
    public partial class AccountWizard : Form
    {
        #region Properties

        /// <summary>
        /// Value of the current AAccount that is being modified by the wizard
        /// </summary>
        public AAccount Value { get; set; } = new AAccount();

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

        #endregion

        public AccountWizard()
        {
            InitializeComponent();
        }

        private void aAccountForm1_ValueChangedEvent(object sender, EventArgs e)
        {
            Value = aAccountForm1.Value;
            OnValueChangedEvent();
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
    }
}
