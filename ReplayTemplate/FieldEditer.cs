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
    public partial class FieldEditer : Form
    {
        private static int DELIMITER = 3;
        private static int CHECKBOX_DELIMITER = 1;
        private static int TEXTBOX_LOCATION_Y = 20;
        private static int LABEL_WIDTH = 85;
        private static int LABEL_HEIGHT = 13;
        private static int TEXTBOX_SIZE_WIDTH = 300;
        private static int TEXTBOX_SIZE_HEIGHT = 20;
        private static int CHECKBOX_WIDTH = 75;
        private static int CHECKBOX_HEIGHT = 20;
        public int mode { get; set; }
        public Field f { get; set; }
        public Field[] fieldList;
        public FieldEditer()
        {
            InitializeComponent();
            mode = 0;
        }
        public FieldEditer(int theMode)
        {
            InitializeComponent();
            mode = theMode;
        }

        private void FieldEditer_Load(object sender, EventArgs e)
        {
            if (mode == 1)
            {
                //adding
                selectFieldLabel.Visible = false;
                indexComboBox.Enabled = false;
                removeButton.Enabled = false;
            }
            else if (mode == 2)
            {
                //editing
                selectFieldLabel.Text = "Select field to edit";
                removeButton.Enabled = false;
            }
            else if (mode == 3)
            {
                //removing
                selectFieldLabel.Text = "Select field to remove";
                indexComboBox.Enabled = false;
                updateButton.Enabled = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
