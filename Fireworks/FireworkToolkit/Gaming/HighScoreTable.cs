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
    public partial class HighScoreTable : Form
    { 
        public HighScoreTable()
        {
            InitializeComponent();
        }

        public HighScoreTable(ICollection<HighScore> scores)
        {
            InitializeComponent();
            List<HighScore> temp = scores.ToList();
            temp.Sort();
            temp.Reverse();
            foreach (HighScore s in temp)
                listBox1.Items.Add(s);
            
        }

        private void HighScoreTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }
    }
}
