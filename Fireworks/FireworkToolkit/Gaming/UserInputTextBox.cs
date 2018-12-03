using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FireworkToolkit.Gaming
{
    public partial class UserInputTextBox : Form
    {
        #region Properties

        /// <summary>
        /// The text that was entered into this control
        /// </summary>
        public string Value { get; set; } = "";

        /// <summary>
        /// Determines if the user wants to allow a blank input
        /// </summary>
        protected bool AllowEmpty { get; private set; } = false;

        #endregion

        public UserInputTextBox()
        {
            InitializeComponent();
        }

        public UserInputTextBox(string caption, string title = "", bool allowEmpty = false)
        {
            InitializeComponent();

            Caption.Text = caption;
            Text = title;
            AllowEmpty = allowEmpty;
        }

        private void UserInputTextBox_Load(object sender, EventArgs e)
        {
            Caption.Left = (this.ClientSize.Width - Caption.Width) / 2;
            textBox1.Top = Caption.Height;
            button1.Top = Caption.Height + textBox1.Height;
            //ClientSize= new Size(ClientSize.Width, button1.Top + button1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "" || textBox1.Text == "Fill me in!") && !AllowEmpty)
                textBox1.Text = "Fill me in!";
            else
            {
                Value = textBox1.Text;
                Dispose();
            }
        }
    }
}
