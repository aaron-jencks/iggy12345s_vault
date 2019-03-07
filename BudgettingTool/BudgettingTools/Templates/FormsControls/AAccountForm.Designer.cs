namespace BudgettingTools.Templates.FormsControls
{
    partial class AAccountForm
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
            BudgettingTools.Templates.BankComponent bankComponent1 = new BudgettingTools.Templates.BankComponent();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownInitialBalance = new System.Windows.Forms.NumericUpDown();
            this.bankComponentForm1 = new BudgettingTools.Templates.FormsControls.BankComponentForm();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting Balance:";
            // 
            // numericUpDownInitialBalance
            // 
            this.numericUpDownInitialBalance.DecimalPlaces = 2;
            this.numericUpDownInitialBalance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownInitialBalance.Location = new System.Drawing.Point(106, 124);
            this.numericUpDownInitialBalance.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownInitialBalance.Name = "numericUpDownInitialBalance";
            this.numericUpDownInitialBalance.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownInitialBalance.TabIndex = 1;
            this.numericUpDownInitialBalance.ThousandsSeparator = true;
            this.numericUpDownInitialBalance.ValueChanged += new System.EventHandler(this.numericUpDownInitialBalance_ValueChanged);
            // 
            // bankComponentForm1
            // 
            this.bankComponentForm1.Location = new System.Drawing.Point(3, 3);
            this.bankComponentForm1.Name = "bankComponentForm1";
            this.bankComponentForm1.Size = new System.Drawing.Size(266, 115);
            this.bankComponentForm1.TabIndex = 2;
            bankComponent1.Description = "";
            bankComponent1.Title = "";
            this.bankComponentForm1.Value = bankComponent1;
            this.bankComponentForm1.ValueChangedEvent += new BudgettingTools.Templates.FormsControls.BankComponentForm.ValueChangedEventHandler(this.bankComponentForm1_ValueChanged);
            // 
            // AAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bankComponentForm1);
            this.Controls.Add(this.numericUpDownInitialBalance);
            this.Controls.Add(this.label1);
            this.Name = "AAccountForm";
            this.Size = new System.Drawing.Size(283, 166);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInitialBalance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private BankComponentForm bankComponentForm1;
        public System.Windows.Forms.NumericUpDown numericUpDownInitialBalance;
    }
}
