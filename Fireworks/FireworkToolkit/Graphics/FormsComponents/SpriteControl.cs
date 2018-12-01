using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireworkToolkit.SpriteGraphics;
using System.IO;

namespace FireworkToolkit.Graphics.FormsComponents
{
    public partial class SpriteControl : UserControl
    {
        #region Properties

        private Sprite sprite = new Sprite();
        public Sprite Value
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
                OnValueChanged();
            }
        }

        #endregion

        #region Events

        public delegate void ValueChangedEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Triggered when the sprite value changes
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;
        protected virtual void OnValueChanged()
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }

        #endregion

        public SpriteControl()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(!File.Exists(textBox1.Text))
            {
                textBox1.Text = Value.Name;
            }
            else
            {
                Value = new Sprite(textBox1.Text, Value.Zoom);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Value.Zoom = (double)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog wizard = new OpenFileDialog();
            wizard.Filter = "Bitmap | *.bmp";
            wizard.Title = "Select a sprite file to open";
            wizard.Multiselect = false;
            wizard.RestoreDirectory = true;
            wizard.ShowDialog();

            if (wizard.FileName != null)
                foreach (string s in wizard.FileNames)
                    if (s != "")
                    {
                        Sprite temp = new Sprite(s, Value.Zoom);
                        Value = temp;
                    }
        }
    }
}
