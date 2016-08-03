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
    public partial class PleaseWait : Form
    {
        private bool showProgressBar = false;
        public PleaseWait(string message, bool showProgress = false)
        {
            InitializeComponent();
            loadingMessage.Text = message;
            showProgressBar = showProgress;
        }

        public void setProgress(int progress)
        {
            loadingProgressBar.Value = progress;
        }

        public void setMessage(string message)
        {
            loadingMessage.Text = message;
        }
    }
}
