using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BudgettingTools.Interfaces;

namespace BudgettingTools.Templates.FormsControls
{
    public partial class AExpenseForm : UserControl
    {
        #region Properties

        public AExpense Value { get; set; } = new AExpense();

        private ICollection<IAccount> globalAccountList { get; set; } = new List<IAccount>();

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

        public AExpenseForm()
        {
            InitializeComponent();
        }

        public AExpenseForm(ICollection<IAccount> globalAccountList)
        {
            InitializeComponent();
            this.globalAccountList = globalAccountList;
        }

        private void numericUpDownRate_ValueChanged(object sender, EventArgs e)
        {
            Value.Rate = (double)numericUpDownRate.Value;
            OnValueChangedEvent();
        }

        private void aAccountForm1_Load(object sender, EventArgs e)
        {
            AAccount temp = aAccountForm1.Value;
            Value.Withdraw(Value.CheckBalance());
            Value.Deposit(temp.CheckBalance());
            Value.Title = temp.Title;
            Value.Description = temp.Description;
            OnValueChangedEvent();
        }

        private void buttonAccountAdd_Click(object sender, EventArgs e)
        {
            AccountLinkingWizard temp = new AccountLinkingWizard();
            temp.AcceptedEvent += Temp_AcceptedEvent;
            temp.NewAccountEvent += Temp_NewAccountEvent;
            temp.ShowDialog();
        }

        private void Temp_NewAccountEvent(object sender, EventArgs e)
        {
            globalAccountList.Add((IAccount)sender);
            OnNewAccountEvent((IAccount)sender);
        }

        private void Temp_AcceptedEvent(object sender, EventArgs e)
        {
            foreach (IAccount acct in ((AccountLinkingWizard)sender).Value)
            {
                listBoxAccounts.Items.Add(acct);
                Value.AssignAccount(acct);
            }
            OnValueChangedEvent();
        }

        private void buttonAccountRemove_Click(object sender, EventArgs e)
        {
            listBoxAccounts.Items.RemoveAt(listBoxAccounts.SelectedIndex);
            Value.AssignedAccounts.Remove((IAccount)listBoxAccounts.SelectedItem);
        }

        private void comboBoxExpenseRepeitition_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBoxExpenseRepeitition.SelectedText)
            {
                case "Minutely":
                    Value.RepetitionFrequency = ExpenseRepetition.Minutely;
                    numericUpDownPosition.Enabled = true;
                    labelPosition.Text = "Second:";
                    numericUpDownPosition.Maximum = 59;
                    numericUpDownPosition.Minimum = 0;
                    dateTimePicker1.Enabled = false;
                    break;
                case "Hourly":
                    Value.RepetitionFrequency = ExpenseRepetition.Hourly;
                    numericUpDownPosition.Enabled = true;
                    labelPosition.Text = "Minute:";
                    numericUpDownPosition.Maximum = 59;
                    numericUpDownPosition.Minimum = 0;
                    dateTimePicker1.Enabled = false;
                    break;
                case "Daily":
                    Value.RepetitionFrequency = ExpenseRepetition.Daily;
                    numericUpDownPosition.Enabled = false;
                    labelPosition.Text = "Time:";
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.CustomFormat = "h:mm tt";
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    break;
                case "Weekly":
                    Value.RepetitionFrequency = ExpenseRepetition.Weekly;
                    numericUpDownPosition.Enabled = false;
                    labelPosition.Text = "Day/Time:";
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.CustomFormat = "dddd h:mm tt";
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    break;
                case "Monthly":
                    Value.RepetitionFrequency = ExpenseRepetition.Monthly;
                    numericUpDownPosition.Enabled = true;
                    labelPosition.Text = "Day/Time:";
                    numericUpDownPosition.Maximum = 30;
                    numericUpDownPosition.Minimum = 1;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.CustomFormat = "dddd h:mm tt";
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    break;
                case "Yearly":
                    Value.RepetitionFrequency = ExpenseRepetition.Yearly;
                    numericUpDownPosition.Enabled = true;
                    labelPosition.Text = "Day:";
                    numericUpDownPosition.Maximum = 365;
                    numericUpDownPosition.Minimum = 1;
                    dateTimePicker1.Enabled = true;
                    dateTimePicker1.CustomFormat = "MMMM d h:mm tt";
                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                    break;
                case "None":
                    Value.RepetitionFrequency = ExpenseRepetition.None;
                    numericUpDownPosition.Enabled = false;
                    dateTimePicker1.Enabled = false;
                    break;
            }
        }

        private void numericUpDownPosition_ValueChanged(object sender, EventArgs e)
        {
            switch(Value.RepetitionFrequency)
            {
                case ExpenseRepetition.Hourly:
                    Value.repetitionPosition.MinuteNum = (int)numericUpDownPosition.Value;
                    break;
                case ExpenseRepetition.Minutely:
                    Value.repetitionPosition.SecondNum = (int)numericUpDownPosition.Value;
                    break;
                case ExpenseRepetition.Monthly:
                    Value.repetitionPosition.DayNum = (int)numericUpDownPosition.Value;
                    break;
                case ExpenseRepetition.Yearly:
                    Value.repetitionPosition.DayNum = (int)numericUpDownPosition.Value;
                    break;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            switch(Value.RepetitionFrequency)
            {
                case ExpenseRepetition.Weekly:
                    Value.repetitionPosition.Weekday = (Weekdays)dateTimePicker1.Value.DayOfWeek;
                    Value.repetitionPosition.HourNum = dateTimePicker1.Value.Hour;
                    Value.repetitionPosition.MinuteNum = dateTimePicker1.Value.Minute;
                    Value.repetitionPosition.SecondNum = dateTimePicker1.Value.Second;
                    break;
                case ExpenseRepetition.Daily:
                case ExpenseRepetition.Monthly:
                    Value.repetitionPosition.HourNum = dateTimePicker1.Value.Hour;
                    Value.repetitionPosition.MinuteNum = dateTimePicker1.Value.Minute;
                    Value.repetitionPosition.SecondNum = dateTimePicker1.Value.Second;
                    break;
                case ExpenseRepetition.Yearly:
                    Value.repetitionPosition.DayNum = dateTimePicker1.Value.DayOfYear;
                    Value.repetitionPosition.HourNum = dateTimePicker1.Value.Hour;
                    Value.repetitionPosition.MinuteNum = dateTimePicker1.Value.Minute;
                    Value.repetitionPosition.SecondNum = dateTimePicker1.Value.Second;
                    break;
            }
        }
    }
}
