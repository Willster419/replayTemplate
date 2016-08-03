using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace ReplayTemplate
{
    public partial class MainWindow : Form
    {
        private string version = "Alpha 1";
        private static int DELIMITER = 3;
        private static int PANEL_WIDTH = 330;
        private static int PANEL_HEIGHT = 45;
        private static int TEXTBOX_LOCATION_Y = 20;
        private static int LABEL_WIDTH = 100;
        private static int LABEL_HEIGHT = 13;
        private static int TEXTBOX_SIZE_WIDTH = 300;
        private static int TEXTBOX_SIZE_HEIGHT = 20;
        private StringBuilder titleSB = new StringBuilder();
        private StringBuilder bodySB = new StringBuilder();
        private Template rel2 = new Template();
        private List<Template> templateList = new List<Template>();
        private List<Template> tempTemplateList = new List<Template>();
        private DateTime date = new DateTime();
        //private Point p;
        private Size labelSize = new Size(LABEL_WIDTH, LABEL_HEIGHT);
        private Size textBoxSize = new Size(TEXTBOX_SIZE_WIDTH, TEXTBOX_SIZE_HEIGHT);
        private RadioButton victory = new RadioButton() { Text = "Win" };
        private RadioButton defeat = new RadioButton() { Text = "Loss" };
        //private Panel FieldPanel = new Panel() { Width = PANEL_WIDTH, Height = PANEL_HEIGHT, BorderStyle = BorderStyle.FixedSingle };
        private EditFieldWindow edit = new EditFieldWindow();
        private TextOutputWindow textOut = new TextOutputWindow();
        private XmlTextWriter templateWriter;
        private XmlTextReader templateReader;
        string saveFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\templateLists.xml";
        string savePath = Path.GetDirectoryName(Application.ExecutablePath);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addStandard(Field f)
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
            l.Text = f.name;
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
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            panel2.Controls.Add(p);
        }

        private void addVictoryDefeat()
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
            l.Text = "Result";
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the radioButtons in the panel
            Point victoryLocation = new Point();
            Point defeatLocation = new Point();
            victoryLocation.X = DELIMITER;
            victoryLocation.Y = TEXTBOX_LOCATION_Y;
            defeatLocation.X = DELIMITER + victory.Size.Width + DELIMITER;
            defeatLocation.Y = TEXTBOX_LOCATION_Y;
            victory.Location = victoryLocation;
            defeat.Location = defeatLocation;
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(victory);
            p.Controls.Add(defeat);
            panel2.Controls.Add(p);
        }

        private void addDate()
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
            l.Text = "Date";
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the text box in the panel
            date = DateTime.Now;
            Label dateLabel = new Label();
            Point dateLocation = new Point();
            dateLocation.X = DELIMITER;
            dateLocation.Y = TEXTBOX_LOCATION_Y;
            dateLabel.Location = dateLocation;
            dateLabel.Size = textBoxSize;
            dateLabel.Text = String.Format("{0:MM/dd/yy}", date);
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(dateLabel);
            panel2.Controls.Add(p);
        }

        private void addYoutube(Field f)
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
            l.Text = f.name + " (Youtube)";
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
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            panel2.Controls.Add(p);
        }

        private void displaySelectedTemplate(int index)
        {
            Template t = templateList[index];
            //infrom which is selected for verification
            selectionLabel.Text = t.clanName + " selected";
            //show youtube embed syntax
            youtubeEmbedStartTextBox.Text = t.youtubeEmbedStartURL;
            youtubeEmbedEndTextBox.Text = t.youtubeEmbedEndURL;
            numFieldsTextBox.Text = "" + t.numFields;
            templateTypeTextBox.Text = t.templateType;
            //clear the current panel
            while (panel2.Controls.Count != 0)
            {
                panel2.Controls.RemoveAt(0);
            }
            //fill panel
            for (int i = 0; i < t.fieldList.Count; i++)
            {
                Field f = t.fieldList[i];
                int selection = f.type;
                if (selection == 1)
                {
                    //standard
                    this.addStandard(f);
                }
                else if (selection == 2)
                {
                    //date
                    this.addDate();
                }
                else if (selection == 3)
                {
                    //victoryDefeat
                    this.addVictoryDefeat();
                }
                else if (selection == 4)
                {
                    //youtube
                    this.addYoutube(f);
                }
                else
                {
                    MessageBox.Show("Database error. The program can continue not and must exit.");
                    //this.Dispose();
                    this.Close();
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.loadTemplates();
        }

        private void templateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateComboBox.SelectedIndex == -1)
            {
                //do nothing
            }
            else if (templateComboBox.Text.Equals("create/edit custom template..."))
            {
                //launch editor

            }
            else
            {
                //load template
                this.displaySelectedTemplate(templateComboBox.SelectedIndex);
            }
        }

        private void resetUIButton_Click(object sender, EventArgs e)
        {
            this.resetUI();
        }

        private void editFieldButton_Click(object sender, EventArgs e)
        {
            edit.ShowDialog();
        }

        private void createThreadButton_Click(object sender, EventArgs e)
        {
            if (templateComboBox.SelectedIndex == -1)
            {
                //MessageBox.Show("Cannot show black template");
            }
            else
            {
                bodySB = new StringBuilder();
                textOut.body.Text = "";
                if (templateComboBox.Text.Equals("create custom..."))
                {

                }
                else
                {
                    //create new UNLINKED template
                    Template t = this.createUnlinkedTemplate(templateList[templateComboBox.SelectedIndex]);
                    for (int i = 0; i < t.fieldList.Count; i++)
                    {
                        Field f = t.fieldList[i];
                        if (f.type == 3)
                        {
                            //special case victory/defeat
                            Panel temp = (Panel)panel2.Controls[i];
                            Label lName = (Label)temp.Controls[0];
                            string name = lName.Text;
                            RadioButton vic = (RadioButton)temp.Controls[1];
                            bool b = vic.Checked;
                            if (b)
                            {
                                //was victory
                                t.fieldList[i].value = "Win";
                            }
                            else
                            {
                                //was defeat
                                t.fieldList[i].value = "Loss";
                            }
                            t.fieldList[i].name = name + ": ";
                        }
                        else if (f.type == 2)
                        {
                            //special case date
                            Panel temp = (Panel)panel2.Controls[i];
                            Label lName = (Label)temp.Controls[0];
                            string name = lName.Text;
                            Label lValue = (Label)temp.Controls[1];
                            string value = lValue.Text;
                            t.fieldList[i].name = name + ": ";
                            t.fieldList[i].value = value;
                        }
                        else if (f.type == 4)
                        {
                            //special case youtube
                            Panel temp = (Panel)panel2.Controls[i];
                            Label lName = (Label)temp.Controls[0];
                            string name = lName.Text;
                            TextBox tb = (TextBox)temp.Controls[1];
                            string value = tb.Text;
                            t.fieldList[i].name = name + ": \n";
                            t.fieldList[i].value = "[youtube]" + value + "[/youtube]";
                        }
                        else
                        {
                            //normal cases
                            Panel temp = (Panel)panel2.Controls[i];
                            Label lName = (Label)temp.Controls[0];
                            string name = lName.Text;
                            TextBox tb = (TextBox)temp.Controls[1];
                            string value = tb.Text;
                            t.fieldList[i].name = name + ": ";
                            t.fieldList[i].value = value;
                        }
                    }
                    //build the string
                    //first thread title

                    //then thread body
                    for (int i = 0; i < t.fieldList.Count; i++)
                    {
                        bodySB.Append(t.fieldList[i].name + t.fieldList[i].value + "\n");
                    }
                    //output to window
                    textOut.body.Text = bodySB.ToString();
                    textOut.ShowDialog();
                }
            }
        }
        private Template createUnlinkedTemplate(Template temp)
        {
            Template t = new Template();
            t.clanName = temp.clanName;
            t.fieldList = new List<Field>(temp.fieldList);
            t.threadURL = temp.threadURL;
            t.youtubeEmbedEndURL = temp.youtubeEmbedEndURL;
            t.youtubeEmbedStartURL = temp.youtubeEmbedStartURL;
            return t;
        }

        private void saveTemplateButton_Click(object sender, EventArgs e)
        {
            this.saveTemplates();
        }

        private void loadTemplatesButton_Click(object sender, EventArgs e)
        {
            this.loadTemplates();
        }

        private void resetUI()
        {
            //clear display panel
            while (panel2.Controls.Count != 0)
            {
                panel2.Controls.RemoveAt(0);
            }
            //reload comboBox items
            while (templateComboBox.Items.Count != 0)
            {
                templateComboBox.Items.RemoveAt(0);
            }
            for (int i = 0; i < templateList.Count; i++)
            {
                templateComboBox.Items.Add(templateList[i]);
            }
            templateComboBox.Items.Add("create/edit custom template...");
            //reset selection
            templateComboBox.SelectedIndex = -1;
            selectionLabel.Text = "Nothing selected";
            youtubeEmbedStartTextBox.Text = "null";
            youtubeEmbedEndTextBox.Text = "null";
            numFieldsTextBox.Text = "null";
            templateTypeTextBox.Text = "null";
        }

        private void saveTemplates()
        {
            if (templateComboBox.SelectedIndex == -1)
            {

            }
            else
            {
                if (File.Exists(saveFile)) File.Delete(saveFile);
                templateWriter = new XmlTextWriter(saveFile, Encoding.UTF8);
                templateWriter.Formatting = Formatting.Indented;
                templateWriter.WriteStartDocument();
                templateWriter.WriteStartElement(Path.GetFileName(saveFile));
                templateWriter.WriteStartElement("templates");
                for (int i = 0; i < templateList.Count; i++)
                {
                    templateWriter.WriteStartElement("template");
                    templateWriter.WriteElementString("clanName", templateList[i].clanName);
                    templateWriter.WriteElementString("threadURL", templateList[i].threadURL);
                    templateWriter.WriteElementString("youtubeEmbedStartURL", templateList[i].youtubeEmbedStartURL);
                    templateWriter.WriteElementString("youtubeEmbedEndURL", templateList[i].youtubeEmbedEndURL);
                    templateWriter.WriteElementString("numFields", "" + templateList[i].numFields);
                    templateWriter.WriteElementString("templateType", templateList[i].templateType);
                    templateWriter.WriteStartElement("fields");
                    for (int j = 0; j < templateList[i].fieldList.Count; j++)
                    {
                        Field f = templateList[i].fieldList[j];
                        templateWriter.WriteStartElement("field");
                        templateWriter.WriteElementString("name", f.name);
                        templateWriter.WriteElementString("type", "" + f.type);
                        templateWriter.WriteEndElement();
                    }
                    templateWriter.WriteEndElement();
                }
                templateWriter.WriteEndElement();
                templateWriter.WriteEndElement();
                templateWriter.Close();
            }
        }

        private void loadTemplates()
        {
            templateComboBox.SelectedIndex = -1;
            templateList = new List<Template>();
            templateReader = new XmlTextReader(saveFile);
            Template t = new Template();
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
                                    t.clanName = templateReader.ReadString();
                                    break;
                                case "threadURL":
                                    t.threadURL = templateReader.ReadString();
                                    break;
                                case "youtubeEmbedStartURL":
                                    t.youtubeEmbedStartURL = templateReader.ReadString();
                                    break;
                                case "youtubeEmbedEndURL":
                                    t.youtubeEmbedEndURL = templateReader.ReadString();
                                    break;
                                case "numFields":
                                    t.numFields = int.Parse(templateReader.ReadString());
                                    break;
                                case "templateType":
                                    t.templateType = templateReader.ReadString();
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
                                                        //type HAS to be last thing read from each field node for this to work
                                                        case "type":
                                                            f.type = int.Parse(templateReader.ReadString());
                                                            break;
                                                    }
                                                    //}
                                                    if (templateReader.Name.Equals("type"))
                                                    {
                                                        //add field to list and reset temp field
                                                        if (f.name != "null") t.fieldList.Add(f);
                                                        numFieldsAdded++;
                                                        if (numFieldsAdded == t.numFields)
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
                            //add the template to list and reset the temp template
                            templateList.Add(t);
                            t = new Template();
                            break;
                        }
                    }
                }
            }
            templateReader.Close();
            this.resetUI();
        }
    }
}
