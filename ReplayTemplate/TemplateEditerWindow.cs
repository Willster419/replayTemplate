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
        private Size labelSize = new Size(LABEL_WIDTH, LABEL_HEIGHT);
        private Size textBoxSize = new Size(TEXTBOX_SIZE_WIDTH, TEXTBOX_SIZE_HEIGHT);
        private Size checkBoxSize = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT);
        private Size titleIndexSize = new Size(CHECKBOX_HEIGHT, CHECKBOX_HEIGHT);
        List<Field> fieldList = new List<Field>();
        Template sampleTemplate;
        private FieldEditer editer;
        private RadioButton victory;
        private RadioButton defeat;
        private RadioButton draw;
        private DateTimePicker dtp;
        private int theTemplateType;
        private int numFields = 0;
        public TemplateEditerWindow()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(1);
            editer.fieldList = fieldList;
            editer.ShowDialog();
            this.reloadPanel();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(2);
            editer.fieldList = fieldList;
            editer.ShowDialog();
            this.reloadPanel();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            editer = new FieldEditer(3);
            editer.fieldList = fieldList;
            editer.ShowDialog();
            this.reloadPanel();
        }

        private void reloadPanel()
        {
            numFields = 0;
            panel2.Controls.Clear();
            foreach (Field f in fieldList)
            {
                if (f.type == 1)
                {
                    //standard
                    addStandard(f, f.name);
                }
                else if (f.type == 2)
                {
                    //date
                    addDate(f, f.name);
                }
                else if (f.type == 3)
                {
                    //victoryDefeat
                    addVictoryDefeat(f, f.name);
                }
                else
                {
                    //youtube
                    addYoutube(f, f.name);
                }
                numFields++;
            }
            numFieldsTextBox.Text = "" + numFields;
        }

        private void selectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasDuplicates = false;
            foreach (Field f in fieldList)
            {
                if (f.duplicate)
                {
                    hasDuplicates = true;
                }
            }
            //check for doubles. if doubles exist, tell user to remove doubles first
            theTemplateType = selectTypeComboBox.SelectedIndex;
            //if the user just selected single templates and has duplicate items
            if ((theTemplateType == 0) && hasDuplicates)
            {
                //can't be single with duplicate entries
                MessageBox.Show("There must be no duplicate fields present to be a single type template");
                selectTypeComboBox.SelectedIndex = -1;
            }
            if ((theTemplateType == 1 || theTemplateType == 2) && !hasDuplicates)
            {
                MessageBox.Show("There must be duplicate fields present to be a series or stronghood type template");
                selectTypeComboBox.SelectedIndex = -1;
            }
        }
        private void addStandard( Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            if (f.duplicate) p.BackColor = SystemColors.ControlDark;
            p.Width = PANEL_WIDTH;
            p.Height = PANEL_HEIGHT;
            Point panelLocation = new Point();
            panelLocation.X = DELIMITER;
            int totalOffset = DELIMITER * panel2.Controls.Count;
            int totalHeight = PANEL_HEIGHT * panel2.Controls.Count;
            panelLocation.Y = totalHeight + totalOffset;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = panelLocation;
            //setup the label in the panel
            Label l = new Label();
            l.Text = name;
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the text box in the panel
            TextBox tb = new TextBox();
            Point tbLocation = new Point();
            tbLocation.X = DELIMITER;
            tbLocation.Y = TEXTBOX_LOCATION_Y;
            tb.Location = tbLocation;
            tb.Size = textBoxSize;
            //setup the title check box
            CheckBox titleCB = new CheckBox();
            titleCB.Enabled = false;
            titleCB.Checked = f.inTitle;
            titleCB.Text = "in header";
            Point titleCBLocation = new Point(LABEL_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleCB.Location = titleCBLocation;
            titleCB.Size = checkBoxSize;
            //setup the body check box
            CheckBox bodyCB = new CheckBox();
            bodyCB.Enabled = false;
            bodyCB.Checked = f.inBody;
            bodyCB.Text = "in body";
            Point bodyCBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            bodyCB.Location = bodyCBLocation;
            bodyCB.Size = checkBoxSize;
            //setup the title index text box
            TextBox titleIndexTB = new TextBox();
            titleIndexTB.ReadOnly = true;
            titleIndexTB.Text = "" + f.titleIndex;
            if (f.titleIndex == 0) titleIndexTB.Visible = false;
            Point titleIndexTBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleIndexTB.Location = titleIndexTBLocation;
            titleIndexTB.Size = titleIndexSize;
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            p.Controls.Add(titleCB);
            p.Controls.Add(bodyCB);
            p.Controls.Add(titleIndexTB);
            panel2.Controls.Add(p);
        }

        private void addVictoryDefeat(Field f, string name)
        {
            victory = new RadioButton() { Text = "Win" };
            defeat = new RadioButton() { Text = "Loss (loss)" };
            draw = new RadioButton() { Text = "Loss (draw)" };
            //setup the panel
            Panel p = new Panel();
            if (f.duplicate) p.BackColor = SystemColors.ControlDark;
            p.Width = PANEL_WIDTH;
            p.Height = PANEL_HEIGHT;
            Point panelLocation = new Point();
            panelLocation.X = DELIMITER;
            int totalOffset = DELIMITER * panel2.Controls.Count;
            int totalHeight = PANEL_HEIGHT * panel2.Controls.Count;
            panelLocation.Y = totalHeight + totalOffset;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = panelLocation;
            //setup the label in the panel
            Label l = new Label();
            l.Text = name;
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the radioButtons in the panel
            Point victoryLocation = new Point();
            Point defeatLocation = new Point();
            Point drawLocation = new Point();
            victoryLocation.X = DELIMITER;
            victoryLocation.Y = TEXTBOX_LOCATION_Y;
            defeatLocation.X = DELIMITER + victory.Size.Width + DELIMITER;
            defeatLocation.Y = TEXTBOX_LOCATION_Y;
            drawLocation.X = DELIMITER + victory.Size.Width + DELIMITER + defeat.Size.Width + DELIMITER;
            drawLocation.Y = TEXTBOX_LOCATION_Y;
            victory.Location = victoryLocation;
            defeat.Location = defeatLocation;
            draw.Location = drawLocation;
            //setup the title check box
            CheckBox titleCB = new CheckBox();
            titleCB.Enabled = false;
            titleCB.Checked = f.inTitle;
            titleCB.Text = "in header";
            Point titleCBLocation = new Point(LABEL_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleCB.Location = titleCBLocation;
            titleCB.Size = checkBoxSize;
            //setup the body check box
            CheckBox bodyCB = new CheckBox();
            bodyCB.Enabled = false;
            bodyCB.Checked = f.inBody;
            bodyCB.Text = "in body";
            Point bodyCBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            bodyCB.Location = bodyCBLocation;
            bodyCB.Size = checkBoxSize;
            //setup the title index text box
            TextBox titleIndexTB = new TextBox();
            titleIndexTB.ReadOnly = true;
            titleIndexTB.Text = "" + f.titleIndex;
            if (f.titleIndex == 0) titleIndexTB.Visible = false;
            Point titleIndexTBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleIndexTB.Location = titleIndexTBLocation;
            titleIndexTB.Size = titleIndexSize;
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(victory);
            p.Controls.Add(defeat);
            p.Controls.Add(draw);
            p.Controls.Add(titleCB);
            p.Controls.Add(bodyCB);
            p.Controls.Add(titleIndexTB);
            panel2.Controls.Add(p);
        }

        private void addDate(Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            p.Width = PANEL_WIDTH;
            p.Height = PANEL_HEIGHT;
            Point panelLocation = new Point();
            panelLocation.X = DELIMITER;
            int totalOffset = DELIMITER * panel2.Controls.Count;
            int totalHeight = PANEL_HEIGHT * panel2.Controls.Count;
            panelLocation.Y = totalHeight + totalOffset;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = panelLocation;
            //setup the label in the panel
            Label l = new Label();
            l.Text = name;
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the dateTimePicker
            dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = "MM/dd/yy";
            dtp.ShowUpDown = true;
            Point dateLocation = new Point();
            dateLocation.X = DELIMITER;
            dateLocation.Y = TEXTBOX_LOCATION_Y;
            dtp.Location = dateLocation;
            dtp.Size = labelSize;
            //setup the title check box
            CheckBox titleCB = new CheckBox();
            titleCB.Enabled = false;
            titleCB.Checked = f.inTitle;
            titleCB.Text = "in header";
            Point titleCBLocation = new Point(LABEL_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleCB.Location = titleCBLocation;
            titleCB.Size = checkBoxSize;
            //setup the body check box
            CheckBox bodyCB = new CheckBox();
            bodyCB.Enabled = false;
            bodyCB.Checked = f.inBody;
            bodyCB.Text = "in body";
            Point bodyCBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            bodyCB.Location = bodyCBLocation;
            bodyCB.Size = checkBoxSize;
            //setup the title index text box
            TextBox titleIndexTB = new TextBox();
            titleIndexTB.ReadOnly = true;
            titleIndexTB.Text = "" + f.titleIndex;
            if (f.titleIndex == 0) titleIndexTB.Visible = false;
            Point titleIndexTBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleIndexTB.Location = titleIndexTBLocation;
            titleIndexTB.Size = titleIndexSize;
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(dtp);
            p.Controls.Add(titleCB);
            p.Controls.Add(bodyCB);
            p.Controls.Add(titleIndexTB);
            panel2.Controls.Add(p);
        }

        private void addYoutube(Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            if (f.duplicate) p.BackColor = SystemColors.ControlDark;
            p.Width = PANEL_WIDTH;
            p.Height = PANEL_HEIGHT;
            Point panelLocation = new Point();
            panelLocation.X = DELIMITER;
            int totalOffset = DELIMITER * panel2.Controls.Count;
            int totalHeight = PANEL_HEIGHT * panel2.Controls.Count;
            panelLocation.Y = totalHeight + totalOffset;
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Location = panelLocation;
            //setup the label in the panel
            Label l = new Label();
            l.Text = name + " (Youtube)";
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the text box in the panel
            TextBox tb = new TextBox();
            Point tbLocation = new Point();
            tbLocation.X = DELIMITER;
            tbLocation.Y = TEXTBOX_LOCATION_Y;
            tb.Location = tbLocation;
            tb.Size = textBoxSize;
            //setup the title check box
            CheckBox titleCB = new CheckBox();
            titleCB.Enabled = false;
            titleCB.Checked = f.inTitle;
            titleCB.Text = "in header";
            Point titleCBLocation = new Point(LABEL_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleCB.Location = titleCBLocation;
            titleCB.Size = checkBoxSize;
            //setup the body check box
            CheckBox bodyCB = new CheckBox();
            bodyCB.Enabled = false;
            bodyCB.Checked = f.inBody;
            bodyCB.Text = "in body";
            Point bodyCBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            bodyCB.Location = bodyCBLocation;
            bodyCB.Size = checkBoxSize;
            //setup the title index text box
            TextBox titleIndexTB = new TextBox();
            titleIndexTB.ReadOnly = true;
            titleIndexTB.Text = "" + f.titleIndex;
            if (f.titleIndex == 0) titleIndexTB.Visible = false;
            Point titleIndexTBLocation = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER);
            titleIndexTB.Location = titleIndexTBLocation;
            titleIndexTB.Size = titleIndexSize;
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            p.Controls.Add(titleCB);
            p.Controls.Add(bodyCB);
            p.Controls.Add(titleIndexTB);
            panel2.Controls.Add(p);
        }

        private void resetUIButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Completely reset this template design?", "are you sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                panel2.Controls.Clear();

            }
            else
            {

            }
        }
    }
}
