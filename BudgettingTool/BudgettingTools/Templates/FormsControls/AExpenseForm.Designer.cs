namespace BudgettingTools.Templates.FormsControls
{
    partial class AExpenseForm
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.aAccountForm1 = new BudgettingTools.Templates.FormsControls.AAccountForm();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxAccounts = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAccountAdd = new System.Windows.Forms.Button();
            this.buttonAccountRemove = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownRate = new System.Windows.Forms.NumericUpDown();
            this.comboBoxExpenseRepeitition = new System.Windows.Forms.ComboBox();
            this.numericUpDownPosition = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // aAccountForm1
            // 
            this.aAccountForm1.Location = new System.Drawing.Point(246, 20);
            this.aAccountForm1.Name = "aAccountForm1";
            this.aAccountForm1.Size = new System.Drawing.Size(283, 165);
            this.aAccountForm1.TabIndex = 0;
            this.aAccountForm1.ValueChangedEvent += new BudgettingTools.Templates.FormsControls.AAccountForm.ValueChangedEventHandler(this.aAccountForm1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Account Data";
            // 
            // listBoxAccounts
            // 
            this.listBoxAccounts.FormattingEnabled = true;
            this.listBoxAccounts.Location = new System.Drawing.Point(246, 204);
            this.listBoxAccounts.Name = "listBoxAccounts";
            this.listBoxAccounts.Size = new System.Drawing.Size(198, 212);
            this.listBoxAccounts.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Assigned Accounts:";
            // 
            // buttonAccountAdd
            // 
            this.buttonAccountAdd.Location = new System.Drawing.Point(246, 422);
            this.buttonAccountAdd.Name = "buttonAccountAdd";
            this.buttonAccountAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAccountAdd.TabIndex = 4;
            this.buttonAccountAdd.Text = "Add";
            this.buttonAccountAdd.UseVisualStyleBackColor = true;
            this.buttonAccountAdd.Click += new System.EventHandler(this.buttonAccountAdd_Click);
            // 
            // buttonAccountRemove
            // 
            this.buttonAccountRemove.Location = new System.Drawing.Point(327, 422);
            this.buttonAccountRemove.Name = "buttonAccountRemove";
            this.buttonAccountRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonAccountRemove.TabIndex = 5;
            this.buttonAccountRemove.Text = "Remove";
            this.buttonAccountRemove.UseVisualStyleBackColor = true;
            this.buttonAccountRemove.Click += new System.EventHandler(this.buttonAccountRemove_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Expense Amount:";
            // 
            // numericUpDownRate
            // 
            this.numericUpDownRate.DecimalPlaces = 2;
            this.numericUpDownRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownRate.Location = new System.Drawing.Point(174, 30);
            this.numericUpDownRate.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownRate.Name = "numericUpDownRate";
            this.numericUpDownRate.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownRate.TabIndex = 7;
            this.numericUpDownRate.ThousandsSeparator = true;
            this.numericUpDownRate.ValueChanged += new System.EventHandler(this.numericUpDownRate_ValueChanged);
            // 
            // comboBoxExpenseRepeitition
            // 
            this.comboBoxExpenseRepeitition.FormattingEnabled = true;
            this.comboBoxExpenseRepeitition.Items.AddRange(new object[] {
            "Minutely",
            "Hourly",
            "Daily",
            "Weekly",
            "Monthly",
            "Yearly",
            "None"});
            this.comboBoxExpenseRepeitition.Location = new System.Drawing.Point(120, 56);
            this.comboBoxExpenseRepeitition.Name = "comboBoxExpenseRepeitition";
            this.comboBoxExpenseRepeitition.Size = new System.Drawing.Size(121, 21);
            this.comboBoxExpenseRepeitition.TabIndex = 8;
            this.comboBoxExpenseRepeitition.SelectedIndexChanged += new System.EventHandler(this.comboBoxExpenseRepeitition_SelectedIndexChanged);
            // 
            // numericUpDownPosition
            // 
            this.numericUpDownPosition.Location = new System.Drawing.Point(190, 83);
            this.numericUpDownPosition.Name = "numericUpDownPosition";
            this.numericUpDownPosition.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownPosition.TabIndex = 9;
            this.numericUpDownPosition.ValueChanged += new System.EventHandler(this.numericUpDownPosition_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Repetition Frequency:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(85, 85);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(29, 13);
            this.labelPosition.TabIndex = 11;
            this.labelPosition.Text = "Day:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Expense Data";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(41, 109);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // AExpenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownPosition);
            this.Controls.Add(this.comboBoxExpenseRepeitition);
            this.Controls.Add(this.numericUpDownRate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonAccountRemove);
            this.Controls.Add(this.buttonAccountAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBoxAccounts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aAccountForm1);
            this.Name = "AExpenseForm";
            this.Size = new System.Drawing.Size(515, 451);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AAccountForm aAccountForm1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxAccounts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAccountAdd;
        private System.Windows.Forms.Button buttonAccountRemove;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownRate;
        private System.Windows.Forms.ComboBox comboBoxExpenseRepeitition;
        private System.Windows.Forms.NumericUpDown numericUpDownPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
