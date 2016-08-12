using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

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
        private FieldEditer editer;
        private RadioButton victory;
        private RadioButton defeat;
        private RadioButton draw;
        private DateTimePicker dtp;
        private int theTemplateType;
        private int numFields = 0;
        private TextOutputWindow textOut = new TextOutputWindow();
        StringBuilder bodySB;
        StringBuilder titleSB;
        int battleCount = 1;
        private List<int> origionalLengths = new List<int>();
        private bool firstTimeLandingStronghold = true;
        int lastSelected = 0;
        private XmlTextWriter templateWriter;
        private XmlTextReader templateReader;
        bool editMode = true;
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
            if (editMode) selectTypeComboBox_SelectedIndexChanged(null, null);
        }

        private void selectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool hasDuplicates = false;
            foreach (Field fi in fieldList)
            {
                if (fi.duplicate)
                {
                    hasDuplicates = true;
                }
            }
            //check for doubles. if doubles exist, tell user to remove doubles first
            theTemplateType = selectTypeComboBox.SelectedIndex;
            //if the user just selected single templates and has duplicate items
            if ((theTemplateType == 0) && hasDuplicates && editMode)
            {
                //can't be single with duplicate entries
                MessageBox.Show("There must be no duplicate fields present to be a single type template");
                selectTypeComboBox.SelectedIndex = -1;
                return;
            }
            if ((theTemplateType == 1 || theTemplateType == 2) && !hasDuplicates && editMode)
            {
                MessageBox.Show("There must be duplicate fields present to be a series or stronghood type template");
                selectTypeComboBox.SelectedIndex = -1;
                return;
            }
        }
        private void addStandard(Field f, string name)
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

        private void createThreadButton_Click(object sender, EventArgs e)
        {
            if (!everythingIsFilledOut())
            {
                MessageBox.Show("not everything is filled out");
                return;
            }
            textOut = new TextOutputWindow();
            textOut.label1.Text = "this is what your header will look like";
            textOut.label2.Text = "this is what your body will look like, without duplicate formatting";
            {
                bodySB = new StringBuilder();
                titleSB = new StringBuilder();
                textOut.body.Text = "";
                textOut.title.Text = "";
                Field[] headerList = new Field[99];
                for (int i = 0; i < fieldList.Count; i++)
                {
                    Field f = fieldList[i];
                    if (f.type == 3)
                    {
                        //special case victory/defeat
                        Panel temp = (Panel)panel2.Controls[i];
                        Label lName = (Label)temp.Controls[0];
                        string name = lName.Text;
                        string value;
                        RadioButton vic = (RadioButton)temp.Controls[1];
                        RadioButton def = (RadioButton)temp.Controls[2];
                        RadioButton draw = (RadioButton)temp.Controls[3];
                        if (vic.Checked)
                        {
                            //was victory
                            value = "Win";
                        }
                        else if (def.Checked)
                        {
                            //was defeat
                            value = "Loss (loss)";
                        }
                        else if (draw.Checked)
                        {
                            //was draw
                            value = "Loss (draw)";
                        }
                        else
                        {
                            //value not specified
                            value = "null";
                        }
                        if (f.duplicate) name = name + " (duplicate) ";
                        name = name + ": ";
                        if (f.inBody) bodySB.Append(name + value + "\n");
                        Field tempf = new Field(f.name, f.type);
                        tempf.value = value;
                        if (f.inTitle) headerList[f.titleIndex - 1] = tempf;
                    }
                    else if (f.type == 2)
                    {
                        //special case date
                        Panel temp = (Panel)panel2.Controls[i];
                        Label lName = (Label)temp.Controls[0];
                        string name = lName.Text;
                        DateTimePicker lValue = (DateTimePicker)temp.Controls[1];
                        string value = lValue.Text;
                        if (f.duplicate) name = name + "(duplicate) ";
                        name = name + ": ";
                        if (f.inBody) bodySB.Append(name + value + "\n");
                        Field tempf = new Field(f.name, f.type);
                        tempf.value = value;
                        if (f.inTitle) headerList[f.titleIndex - 1] = tempf;
                    }
                    else if (f.type == 4)
                    {
                        //special case youtube
                        Panel temp = (Panel)panel2.Controls[i];
                        Label lName = (Label)temp.Controls[0];
                        string name = lName.Text;
                        TextBox tb = (TextBox)temp.Controls[1];
                        string value = tb.Text;
                        if (f.duplicate) name = name + " (duplicate) ";
                        name = name + ":";
                        name = name + "\n";
                        value = youtubeEmbedStartTextBox.Text + value + youtubeEmbedEndTextBox.Text;
                        if (f.inBody) bodySB.Append(name + value + "\n");
                        Field tempf = new Field(f.name, f.type);
                        tempf.value = value;
                        if (f.inTitle) headerList[f.titleIndex - 1] = tempf;
                    }
                    else
                    {
                        //normal cases
                        Panel temp = (Panel)panel2.Controls[i];
                        Label lName = (Label)temp.Controls[0];
                        string name = lName.Text;
                        TextBox tb = (TextBox)temp.Controls[1];
                        string value = tb.Text;
                        if (f.duplicate) name = name + " (duplicate) ";
                        name = name + ": ";
                        if (f.inBody) bodySB.Append(name + value + "\n");
                        Field tempf = new Field(f.name, f.type);
                        tempf.value = value;
                        if (f.inTitle) headerList[f.titleIndex - 1] = tempf;
                    }
                }


                //build the string
                //first thread title
                int k = 0;
                while (headerList[k] != null)
                {
                    if (headerList[k + 1] == null)
                    {
                        if (selectTypeComboBox.SelectedIndex == 0)
                        {
                            //last single header event, don't add space
                            titleSB.Append(headerList[k].value);
                        }
                        else
                        {
                            //last sh or series header event, add type
                            titleSB.Append(headerList[k].value + delimiterTextBox.Text + (selectTypeComboBox.SelectedIndex + 1));
                        }

                    }
                    else
                    {
                        titleSB.Append(headerList[k].value + delimiterTextBox.Text);
                    }
                    k++;
                }
                //then thread body
                if (selectTypeComboBox.SelectedIndex == 0)
                {
                    //single
                    for (int i = 0; i < fieldList.Count; i++)
                    {
                        //bodySB.Append(fieldList[i].name + fieldList[i].value + "\n");
                    }
                }
                //output to window
                textOut.title.Text = titleSB.ToString();
                textOut.body.Text = bodySB.ToString();
                textOut.button2.Enabled = false;
                textOut.ShowDialog();

            }
        }

        private string parseName(string name, int intendedLength)
        {
            string temp = name.Substring(0, intendedLength);
            return temp;
        }

        private bool everythingIsFilledOut()
        {
            if (numFields == 0) return false;
            if (delimiterTextBox.Text.Equals("")) return false;
            if (youtubeEmbedEndTextBox.Text.Equals("")) return false;
            if (youtubeEmbedStartTextBox.Text.Equals("")) return false;
            if (selectTypeComboBox.SelectedIndex == -1) return false;
            return true;
        }

        private bool everythingIsFilledOut2()
        {
            if (numFields == 0) return false;
            if (delimiterTextBox.Text.Equals("")) return false;
            if (youtubeEmbedEndTextBox.Text.Equals("")) return false;
            if (youtubeEmbedStartTextBox.Text.Equals("")) return false;
            if (selectTypeComboBox.SelectedIndex == -1) return false;
            if (textBox1.Text.Equals("")) return false;
            if (textBox2.Text.Equals("")) return false;
            return true;
        }

        private void saveTemplateButton_Click(object sender, EventArgs e)
        {
            if (!everythingIsFilledOut2())
            {
                MessageBox.Show("noeverything is filled out");
                return;
            }
            saveTemplateDialog.InitialDirectory = Application.StartupPath;
            saveTemplateDialog.ShowDialog();
        }

        private void loadTemplateButton_Click(object sender, EventArgs e)
        {
            loadTemplateDialog.InitialDirectory = Application.StartupPath;
            loadTemplateDialog.ShowDialog();
        }

        private void loadTemplateDialog_FileOk(object sender, CancelEventArgs e)
        {
            editMode = false;
            fieldList = new List<Field>();
            string templateFile = loadTemplateDialog.FileName;
            templateReader = new XmlTextReader(templateFile);
            templateReader.Read();
            templateReader.Read();
            templateReader.Read();
            templateReader.Read();
            templateReader.Read();
            templateReader.Read();
            while (templateReader.Read())
            {
                if (templateReader.Name.Equals("template"))
                {
                    while (templateReader.Read())
                    {
                        if (templateReader.IsStartElement())
                        {
                            switch (templateReader.Name)
                            {
                                //parse everything into temp template
                                case "clanName":
                                    textBox1.Text = templateReader.ReadString();
                                    break;
                                case "threadURL":
                                    textBox2.Text = templateReader.ReadString();
                                    break;
                                case "youtubeEmbedStartURL":
                                    youtubeEmbedStartTextBox.Text = templateReader.ReadString();
                                    break;
                                case "youtubeEmbedEndURL":
                                    youtubeEmbedEndTextBox.Text = templateReader.ReadString();
                                    break;
                                case "numFields":
                                    numFields = int.Parse(templateReader.ReadString());
                                    break;
                                case "templateType":
                                    int tmep = int.Parse(templateReader.ReadString());
                                    tmep--;
                                    selectTypeComboBox.SelectedIndex = tmep; 
                                    break;
                                case "delimiter":
                                    delimiterTextBox.Text = templateReader.ReadString();
                                    break;
                                //fields HAS to be the last thing read from each template node for this to work
                                case "fields":
                                    {
                                        int numFieldsAdded = 0;
                                        Field f = new Field("null");
                                        templateReader.Read();
                                        while (templateReader.Read())
                                        {
                                            bool needToBreak = false;
                                            if (templateReader.Name.Equals("field"))
                                            {
                                                while (templateReader.Read())
                                                {
                                                    //if (templateReader.IsStartElement())
                                                    //{
                                                    switch (templateReader.Name)
                                                    {
                                                        case "name":
                                                            f.name = templateReader.ReadString();
                                                            break;
                                                        case "duplicate":
                                                            f.duplicate = bool.Parse(templateReader.ReadString());
                                                            break;
                                                        case "inTitle":
                                                            f.inTitle = bool.Parse(templateReader.ReadString());
                                                            break;
                                                        case "inBody":
                                                            f.inBody = bool.Parse(templateReader.ReadString());
                                                            break;
                                                        case "titleIndex":
                                                            f.titleIndex = int.Parse(templateReader.ReadString());
                                                            break;
                                                        //type HAS to be last thing read from each field node for this to work
                                                        case "type":
                                                            f.type = int.Parse(templateReader.ReadString());
                                                            break;
                                                    }
                                                    //}
                                                    if (templateReader.Name.Equals("type"))
                                                    {
                                                        //add field to list and reset temp field
                                                        if (f.name != "null") fieldList.Add(f);
                                                        numFieldsAdded++;
                                                        if (numFieldsAdded == numFields)
                                                        {
                                                            //all fields added
                                                            needToBreak = true;
                                                        }
                                                        f = new Field("null");
                                                        break;
                                                    }
                                                }
                                            }
                                            if (needToBreak) break;
                                        }
                                        break;
                                    }
                            }
                        }
                        if (templateReader.Name.Equals("fields"))
                        {
                            break;
                        }
                    }
                }
            }
            templateReader.Close();
            this.reloadPanel();
            editMode = true;
        }

        private void saveTemplateDialog_FileOk(object sender, CancelEventArgs e)
        {
            string templateFile = saveTemplateDialog.FileName;
            if (File.Exists(templateFile)) File.Delete(templateFile);
            templateWriter = new XmlTextWriter(templateFile, Encoding.UTF8);
            templateWriter.Formatting = Formatting.Indented;
            templateWriter.WriteStartDocument();
            templateWriter.WriteStartElement(Path.GetFileName(templateFile));
            templateWriter.WriteStartElement("templates");
            templateWriter.WriteStartElement("template");
            templateWriter.WriteElementString("clanName", textBox1.Text);
            templateWriter.WriteElementString("threadURL", textBox2.Text);
            templateWriter.WriteElementString("youtubeEmbedStartURL", youtubeEmbedStartTextBox.Text);
            templateWriter.WriteElementString("youtubeEmbedEndURL", youtubeEmbedEndTextBox.Text);
            templateWriter.WriteElementString("numFields", "" + numFields);
            templateWriter.WriteElementString("templateType", "" + (selectTypeComboBox.SelectedIndex + 1));
            templateWriter.WriteElementString("delimiter", delimiterTextBox.Text);
            templateWriter.WriteStartElement("fields");
            for (int j = 0; j < fieldList.Count; j++)
            {
                Field field = fieldList[j];
                templateWriter.WriteStartElement("field");
                templateWriter.WriteElementString("name", field.name);
                templateWriter.WriteElementString("duplicate", "" + field.duplicate);
                templateWriter.WriteElementString("inTitle", "" + field.inTitle);
                templateWriter.WriteElementString("inBody", "" + field.inBody);
                if(field.inTitle) templateWriter.WriteElementString("titleIndex", "" + field.titleIndex);
                templateWriter.WriteElementString("type", "" + field.type);
                templateWriter.WriteEndElement();
            }
            templateWriter.WriteEndElement();
            templateWriter.WriteEndElement();
            templateWriter.WriteEndElement();
            templateWriter.WriteEndElement();
            templateWriter.Close();
        }
    }
}
