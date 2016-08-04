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
using System.Net;

namespace ReplayTemplate
{
    public partial class MainWindow : Form
    {
        /*
         * TODO:
         * add sorting to template list (later)
         * create editor (later)
         * start caching the history of previous entries (soon)
         * put titles in as well
         * order tabs
         * fix problem with outputDisplay
         * ?
         * */
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
        string tempPath;
        string templateFile;
        string tempPath2;
        private TemplateEditorWindow TEW = new TemplateEditorWindow();
        private WebClient client = new WebClient();
        private UpdateWindow uw = new UpdateWindow();
        private PleaseWait pw = new PleaseWait("please wait");
        bool debug = false;
        int battleCount = 1;
        private Template displayTemplate;
        //our first bool hack
        private bool firstTimeLandingStronghold = true;
        int lastSelected = 0;
        int lastTemplateComboBoxSelectedIndex = -1;
        private List<int> origionalLengths = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void parsePaths()
        {
            tempPath2 = Application.StartupPath;
            tempPath = Path.GetTempPath() + "\\ReplayTemplate";
            //uncomment the below string for debug mode
            debug = true;
            if (debug) tempPath = tempPath2;
            templateFile = tempPath + "\\templateLists.xml";
        }

        private void checkForUpdates()
        {
            if (debug)
            {

            }
            else
            {
                if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
                //download version.txt
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/version.txt", tempPath + "\\version.txt");
                string newVersion = File.ReadAllText(tempPath + "\\version.txt");
                if (newVersion.Equals(version))
                {
                    //up to date
                }
                else
                {
                    //download updateNotes.txt
                    client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/updateNotes.txt", tempPath + "\\updateNotes.txt");
                    //prompt user
                    uw.updateNotesRTB.Text = "Version_" + newVersion + ":\n" + File.ReadAllText(tempPath + "\\updateNotes.txt");
                    uw.updateAvailableLabel.Text = "An update is available: " + newVersion;
                    uw.ShowDialog();
                    if (uw.update)
                    {
                        //download new version
                        client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/ReplayTemplate.exe", tempPath + "\\replayTemplate V_" + newVersion + ".exe");
                        //open new one
                        System.Diagnostics.Process.Start(tempPath + "\\replayTemplate V_" + newVersion + ".exe");
                        //close this one
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void checkForFiles()
        {
            if (debug)
            {

            }
            else
            {
                //delete templates xml if it exists
                if (File.Exists(tempPath + "\\templateLists.xml")) File.Delete(tempPath + "\\templateLists.xml");
                //download latest
                client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/templateLists.xml", tempPath + "\\templateLists.xml");
            }
        }

        private void addStandard(Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
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
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            panel2.Controls.Add(p);
        }

        private void addVictoryDefeat(Field f, string name)
        {
            victory = new RadioButton() { Text = "Win", TabStop = true, TabIndex = 2 };
            defeat = new RadioButton() { Text = "Loss", TabStop = true, TabIndex = 1 };
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
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

        private void addDate(string name)
        {
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
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

        private void addYoutube(Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
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
            //add the componets
            p.Controls.Add(l);
            p.Controls.Add(tb);
            panel2.Controls.Add(p);
        }

        private void displaySelectedTemplate(int index)
        {
            firstTimeLandingStronghold = true;
            displayTemplate = templateList[index];
            //infrom which is selected for verification
            selectionLabel.Text = "[" + displayTemplate.clanName + "] selected";
            //show youtube embed syntax
            youtubeEmbedStartTextBox.Text = displayTemplate.youtubeEmbedStartURL;
            youtubeEmbedEndTextBox.Text = displayTemplate.youtubeEmbedEndURL;
            numFieldsTextBox.Text = "" + displayTemplate.numFields;
            templateTypeTextBox.Text = "" + displayTemplate.templateType;
            //clear the current panel
            while (panel2.Controls.Count != 0)
            {
                panel2.Controls.RemoveAt(0);
            }
            //fill panel
            for (int i = 0; i < displayTemplate.fieldList.Count; i++)
            {
                Field f = displayTemplate.fieldList[i];
                int selection = f.type;
                if (selection == 1)
                {
                    //standard
                    this.addStandard(f, f.name);
                }
                else if (selection == 2)
                {
                    //date
                    this.addDate(f.name);
                }
                else if (selection == 3)
                {
                    //victoryDefeat
                    this.addVictoryDefeat(f,f.name);
                }
                else if (selection == 4)
                {
                    //youtube
                    this.addYoutube(f,f.name);
                }
                else
                {
                    MessageBox.Show("Database error. The program can continue not and must exit.");
                    //this.Dispose();
                    this.Close();
                }
            }
            //empty battle combo box selection
            while (numBattlesComboBox.Items.Count != 0)
            {
                numBattlesComboBox.Items.RemoveAt(0);
            }
            //set match battle type textBox and re-fill battle number combo box
            //1=single, 2=landing, 3=StrongHold
            if (displayTemplate.templateType == 1)
            {
                templateTypeTextBox.Text = "single";
                numBattlesComboBox.Items.Add("1");
                numBattlesComboBox.SelectedIndex = 0;
                numBattlesComboBox.Enabled = false;
                battleCount = 1;
            }
            if (displayTemplate.templateType == 2)
            {
                templateTypeTextBox.Text = "landing";
                numBattlesComboBox.Items.Add("2");
                numBattlesComboBox.Items.Add("3");
                numBattlesComboBox.Items.Add("4");
                numBattlesComboBox.Items.Add("5");
                numBattlesComboBox.SelectedIndex = 0;
                numBattlesComboBox.Enabled = true;
            }
            if (displayTemplate.templateType == 3)
            {
                templateTypeTextBox.Text = "stronghold";
                numBattlesComboBox.Items.Add("3");
                numBattlesComboBox.Items.Add("4");
                numBattlesComboBox.Items.Add("5");
                numBattlesComboBox.SelectedIndex = 0;
                numBattlesComboBox.Enabled = true;
            }

            //match number battles
            //use above to determine allowed range of battles
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            pw.setMessage("parsing paths");
            pw.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            this.parsePaths();
            pw.setMessage("Checking for updates...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            this.checkForUpdates();
            pw.setMessage("Checking for files...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            this.checkForFiles();
            pw.setMessage("Loading templates...");
            Application.DoEvents();
            System.Threading.Thread.Sleep(100);
            this.loadTemplates();
            pw.Close();
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
                TEW.ShowDialog();
                this.resetUI();
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
            if (templateComboBox.SelectedIndex == -1 || numBattlesComboBox.SelectedIndex == -1)
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
                    //add version information
                    bodySB.Append("Created using ReplayTemplate V_" + version);
                    //output to window
                    textOut.body.Text = bodySB.ToString();
                    textOut.passClanUrl(t.threadURL);
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
            //empty battle combo box selection
            while (numBattlesComboBox.Items.Count != 0)
            {
                numBattlesComboBox.Items.RemoveAt(0);
            }
            numBattlesComboBox.Enabled = false;

        }

        private void saveTemplates()
        {
            if (templateComboBox.SelectedIndex == -1)
            {

            }
            else
            {
                if (File.Exists(templateFile)) File.Delete(templateFile);
                templateWriter = new XmlTextWriter(templateFile, Encoding.UTF8);
                templateWriter.Formatting = Formatting.Indented;
                templateWriter.WriteStartDocument();
                templateWriter.WriteStartElement(Path.GetFileName(templateFile));
                templateWriter.WriteStartElement("templates");
                for (int i = 0; i < templateList.Count; i++)
                {
                    templateWriter.WriteStartElement("template");
                    templateWriter.WriteElementString("clanName", templateList[i].clanName);
                    templateWriter.WriteElementString("threadURL", templateList[i].threadURL);
                    templateWriter.WriteElementString("youtubeEmbedStartURL", templateList[i].youtubeEmbedStartURL);
                    templateWriter.WriteElementString("youtubeEmbedEndURL", templateList[i].youtubeEmbedEndURL);
                    templateWriter.WriteElementString("numFields", "" + templateList[i].numFields);
                    templateWriter.WriteElementString("templateType", "" + templateList[i].templateType);
                    templateWriter.WriteStartElement("fields");
                    for (int j = 0; j < templateList[i].fieldList.Count; j++)
                    {
                        Field f = templateList[i].fieldList[j];
                        templateWriter.WriteStartElement("field");
                        templateWriter.WriteElementString("name", f.name);
                        templateWriter.WriteElementString("duplicate", "" + f.duplicate);
                        templateWriter.WriteElementString("type", "" + f.type);
                        templateWriter.WriteEndElement();
                    }
                    templateWriter.WriteEndElement();
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
            templateReader = new XmlTextReader(templateFile);
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
                                    t.templateType = int.Parse(templateReader.ReadString());
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

        private void numBattlesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currentBattle = 1;
            
            List<Field> tempList = new List<Field>();
            //copy all duplicate fields to second list
            for (int i = 0; i < displayTemplate.fieldList.Count; i++)
            {
                if (displayTemplate.fieldList[i].duplicate)
                {
                    //add to second list
                    tempList.Add(displayTemplate.fieldList[i]);
                }
            }
            //remove all duplicate fields from gui
            if (firstTimeLandingStronghold)
            {
                for (int i = 0; i < displayTemplate.fieldList.Count; i++)
                {
                    if (displayTemplate.fieldList[i].duplicate)
                    {
                        //remove from gui
                        panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                    }
                }
                firstTimeLandingStronghold = false;
            }
            else
            {
                int limit = 0;
                for (int i = 0; i < displayTemplate.fieldList.Count; i++)
                {
                    if (displayTemplate.fieldList[i].duplicate)
                    {
                        limit++;
                    }
                }
                limit = limit * lastSelected;
                for (int i = 0; i < limit; i++)
                {
                        //remove from gui
                        panel2.Controls.RemoveAt(panel2.Controls.Count - 1);
                }
            }
            
            //determine if landing or stronghold
            if (templateTypeTextBox.Text.Equals("stronghold"))
            {
                //process stronghold battles
                battleCount = numBattlesComboBox.SelectedIndex + 3;
            }
            else
            {
                //process series/landing battles
                battleCount = numBattlesComboBox.SelectedIndex + 2;
                //get the origional length of each item in the temp list. don't run it if the index didn't change
                int currentTemplateComboBoxSelectedIndex = templateComboBox.SelectedIndex;
                if (currentTemplateComboBoxSelectedIndex == lastTemplateComboBoxSelectedIndex)
                {
                    //don't run it
                }
                else
                {
                    //run it
                    origionalLengths = new List<int>();
                    foreach (Field f in tempList)
                    {
                        origionalLengths.Add(f.name.Length);
                    }
                }
                //for each battle, for each item in the array, add it to the list
                for (int i = currentBattle; i < battleCount+1; i++)
                {
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        int selection = tempList[j].type;
                        if (selection == 1)
                        {
                            //standard
                            this.addStandard(tempList[j], tempList[j].name + " " + i);
                        }
                        else if (selection == 2)
                        {
                            //date
                            this.addDate(tempList[j].name + " " + i);
                        }
                        else if (selection == 3)
                        {
                            //victoryDefeat
                            this.addVictoryDefeat(tempList[j], tempList[j].name + " " + i);
                        }
                        else if (selection == 4)
                        {
                            //youtube
                            this.addYoutube(tempList[j], tempList[j].name + " " + i);
                        }
                        else
                        {
                            MessageBox.Show("Database error. The program can continue not and must exit.");
                            //this.Dispose();
                            this.Close();
                        }
                    }
                }
            }
            lastSelected = int.Parse(numBattlesComboBox.Text);
            lastTemplateComboBoxSelectedIndex = templateComboBox.SelectedIndex;
        }
    }
}
