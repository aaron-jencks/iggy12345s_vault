using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BudgettingTools.Templates.FormsControls
{
    public partial class AAccountForm : UserControl
    {
        #region Properties

        /// <summary>
        /// Current value of the Account template
        /// </summary>
        public AAccount Value { get; set; } = new AAccount();

        #endregion

        #region Events

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the value properties change.
        /// </summary>
        public event ValueChangedEventHandler ValueChangedEvent;
        protected void OnValueChangedEvent()
        {
            ValueChangedEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        public AAccountForm()
        {
            InitializeComponent();
        }

        private void numericUpDownInitialBalance_ValueChanged(object sender, EventArgs e)
        {
            Value = new AAccount((double)numericUpDownInitialBalance.Value);
            OnValueChangedEvent();
        }

        private void bankComponentForm1_ValueChanged(object sender, EventArgs e)
        {
            Value.Title = bankComponentForm1.Value.Title;
            Value.Description = bankComponentForm1.Value.Description;
            OnValueChangedEvent();
        }
    }
}
