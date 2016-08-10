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
    public partial class TemplateEditerWindow : Form
    {
        private static int DELIMITER = 3;
        private static int CHECKBOX_DELIMITER = 1;
        private static int PANEL_WIDTH = 330;
        private static int PANEL_HEIGHT = 45;
        private static int TEXTBOX_LOCATION_Y = 20;
        private static int LABEL_WIDTH = 85;
        private static int LABEL_HEIGHT = 13;
        private static int TEXTBOX_SIZE_WIDTH = 300;
        private static int TEXTBOX_SIZE_HEIGHT = 20;
        private static int CHECKBOX_WIDTH = 75;
        private static int CHECKBOX_HEIGHT = 20;
        private static int TAB_START = 17;
        Field[] fieldList;
        Template sampleTemplate;
        private FieldEditer editer;
        public TemplateEditerWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(1);
            editer.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(2);
            editer.ShowDialog();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(3);
            editer.ShowDialog();
        }
    }
}
