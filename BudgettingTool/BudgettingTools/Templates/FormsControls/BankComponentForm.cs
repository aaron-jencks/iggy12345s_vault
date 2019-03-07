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
    public partial class BankComponentForm : UserControl
    {
        #region Properties

        public BankComponent Value { get; set; } = new BankComponent();

        #endregion

        #region Events

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered every time that the value property changes
        /// </summary>
        public event ValueChangedEventHandler ValueChangedEvent;
        protected void OnValueChangedEvent()
        {
            ValueChangedEvent?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Constructors

        public BankComponentForm()
        {
            InitializeComponent();
        }

        #endregion

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            Value.Title = textBoxTitle.Text;
            OnValueChangedEvent();
        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {
            Value.Description = textBoxDescription.Text;
            OnValueChangedEvent();
        }
    }
}
