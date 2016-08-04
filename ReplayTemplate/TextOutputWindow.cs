using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReplayTemplate
{
    public partial class TextOutputWindow : Form
    {
        private string clanURL;
        public TextOutputWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(clanURL);
        }

        public void passClanUrl(string URL)
        {
            clanURL = URL;
        }
    }
}
