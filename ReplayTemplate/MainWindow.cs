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
         * fix bugs as they are found
         * optimize code (maybe lol)
         */
        private string version = "Beta 5";
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
        private int tabInc = 0;
        private StringBuilder titleSB = new StringBuilder();
        private StringBuilder bodySB = new StringBuilder();
        private List<Template> templateList = new List<Template>();
        private List<Template> tempTemplateList = new List<Template>();
        private DateTime date = new DateTime();
        //private Point p;
        private Size labelSize = new Size(LABEL_WIDTH, LABEL_HEIGHT);
        private Size textBoxSize = new Size(TEXTBOX_SIZE_WIDTH, TEXTBOX_SIZE_HEIGHT);
        private Size checkBoxSize = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT);
        private Size titleIndexSize = new Size(CHECKBOX_HEIGHT, CHECKBOX_HEIGHT);
        private RadioButton victory = new RadioButton() { Text = "Win" };
        private RadioButton defeat = new RadioButton() { Text = "Loss" };
        private RadioButton draw = new RadioButton();
        //private Panel FieldPanel = new Panel() { Width = PANEL_WIDTH, Height = PANEL_HEIGHT, BorderStyle = BorderStyle.FixedSingle };
        private TemplateEditerWindow edit = new TemplateEditerWindow();
        private TextOutputWindow textOut = new TextOutputWindow();
        private XmlTextWriter templateWriter;
        private XmlTextReader templateReader;
        string tempPath;
        string templateFile;
        string tempPath2;
        string cachePath;
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
        private DateTimePicker dtp;
        private string templateCachePath;
        //Comparer<Template> defaultCompate = Comparer<Template>.Default;
        private bool pause = true;
        private List<bool> toggleVisableList = new List<bool>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void parsePaths()
        {
            tempPath2 = Application.StartupPath;
            tempPath = Path.GetTempPath() + "\\ReplayTemplate";
            //uncomment the below string for debug mode
            debug = false;
            if (debug) tempPath = tempPath2;
            templateFile = tempPath + "\\templateLists.xml";
            createThreadButton.LostFocus += new EventHandler(createThreadButton_Unfocused);
            cachePath = tempPath + "\\cache";

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
                try
                {
                    client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/version.txt", tempPath + "\\version.txt");
                }
                catch (WebException)
                {
                    MessageBox.Show("Error downloading, eithor you are offline or the update server timmed out");
                    this.Close();
                }
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
                    uw.updateNotesRTB.Text = File.ReadAllText(tempPath + "\\updateNotes.txt");
                    uw.updateAvailableLabel.Text = "An update is available: " + newVersion;
                    uw.ShowDialog();
                    if (uw.update)
                    {
                        //download new version
                        string temp = Path.GetFullPath(Application.StartupPath);
                        client.DownloadFile("https://dl.dropboxusercontent.com/u/44191620/ReplayTemplate/ReplayTemplate.exe", temp + "\\replayTemplate V_" + newVersion + ".exe");
                        //open new one
                        try
                        {
                            System.Diagnostics.Process.Start(temp + "\\replayTemplate V_" + newVersion + ".exe");
                        }
                        catch (Win32Exception)
                        {
                            MessageBox.Show("Could not start the new version, but it is located in\n" + temp);
                        }
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

        private void addStandard(Template t, Field f, string name)
        {
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
            p.TabIndex = TAB_START + tabInc++;
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
            l.TabIndex = TAB_START + tabInc++;
            Point labelLocation = new Point();
            labelLocation.X = DELIMITER;
            labelLocation.Y = DELIMITER;
            l.Location = labelLocation;
            l.Size = labelSize;
            //setup the text box in the panel
            TextBox tb = new TextBox();
            tb.GotFocus += new EventHandler(panel2_Focused);
            tb.TabIndex = TAB_START + tabInc++; ;
            Point tbLocation = new Point();
            tbLocation.X = DELIMITER;
            tbLocation.Y = TEXTBOX_LOCATION_Y;
            tb.Location = tbLocation;
            tb.Size = textBoxSize;
            tb.AutoCompleteMode = AutoCompleteMode.Suggest;
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
            string[] entries = this.loadHistory(t.ToString(), f.name);
            if (entries == null)
            {

            }
            else
            {
                tb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                foreach (string s in entries) tb.AutoCompleteCustomSource.Add(s);
            }
            //add custom sources here
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
            titleIndexTB.TabStop = false;
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
            victory = new RadioButton() { Text = "Win", TabStop = true, TabIndex = TAB_START + tabInc++ };
            defeat = new RadioButton() { Text = "Loss (loss)", TabStop = true, TabIndex = TAB_START + tabInc++ };
            draw = new RadioButton() { Text = "Loss (draw)", TabStop = true, TabIndex = TAB_START + tabInc++ };
            victory.GotFocus += new EventHandler(Victory_Focused);
            victory.LostFocus += new EventHandler(Victory_Unfocused);
            defeat.GotFocus += new EventHandler(Defeat_Focused);
            defeat.LostFocus += new EventHandler(Defeat_Unfocused);
            //setup the panel
            Panel p = new Panel();
            p.TabStop = true;
            p.TabIndex = TAB_START + tabInc++;
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
            l.TabIndex = TAB_START + tabInc++;
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
            titleIndexTB.TabStop = false;
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
            p.TabStop = true;
            p.TabIndex = TAB_START + tabInc++;
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
            l.TabIndex = TAB_START + tabInc++;
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
            date = DateTime.Now;
            dtp.TabStop = true;
            dtp.TabIndex = TAB_START + tabInc++;
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
            titleIndexTB.TabStop = false;
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
            p.TabStop = true;
            p.TabIndex = TAB_START + tabInc++;
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
            titleIndexTB.TabStop = false;
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

        private void displaySelectedTemplate(int index)
        {
            tabInc = 0;
            firstTimeLandingStronghold = true;
            displayTemplate = templateList[index];
            //infrom which is selected for verification
            selectionLabel.Text = "[" + displayTemplate.clanName + "] selected";
            //show youtube embed syntax
            youtubeEmbedStartTextBox.Text = displayTemplate.youtubeEmbedStartURL;
            youtubeEmbedEndTextBox.Text = displayTemplate.youtubeEmbedEndURL;
            numFieldsTextBox.Text = "" + displayTemplate.numFields;
            templateTypeTextBox.Text = "" + displayTemplate.templateType;
            delimiterTextBox.Text = displayTemplate.delimiter;
            //clear the current panel
            panel2.Controls.Clear();
            //fill panel
            for (int i = 0; i < displayTemplate.fieldList.Count; i++)
            {
                Field f = displayTemplate.fieldList[i];
                int selection = f.type;
                //trim the field names just to be safe
                f.name = f.name.Substring(0, f.nameLength);
                if (selection == 1)
                {
                    //standard
                    this.addStandard(displayTemplate, f, f.name);
                }
                else if (selection == 2)
                {
                    //date
                    this.addDate(f, f.name);
                }
                else if (selection == 3)
                {
                    //victoryDefeat
                    this.addVictoryDefeat(f, f.name);
                }
                else if (selection == 4)
                {
                    //youtube
                    this.addYoutube(f, f.name);
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
                templateTypeTextBox.Text = "series";
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
            //create the history for the template if it does not already exist.
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
            this.Text = "ReplayTemplate version " + version;
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
                edit = new TemplateEditerWindow();
                edit.ShowDialog();
                this.resetUI();
            }
            else
            {
                //load template
                this.setupCache(templateList[templateComboBox.SelectedIndex]);
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
                titleSB = new StringBuilder();
                textOut.body.Text = "";
                Field[] headerList = new Field[99];
                if (templateComboBox.Text.Equals("create custom..."))
                {
                    //do nothing
                }
                else
                {
                    //create new UNLINKED template
                    Template t = this.createUnlinkedTemplate(templateList[templateComboBox.SelectedIndex]);
                    //create list of single fields
                    List<Field> singleFields = new List<Field>();
                    //create list of double fields
                    List<Field> doubleFields = new List<Field>();
                    if (t.templateType == 1)
                    {
                        //single type template
                        for (int i = 0; i < t.fieldList.Count; i++)
                        {
                            Field f = t.fieldList[i];
                            if (f.type == 3)
                            {
                                //special case victory/defeat
                                Panel temp = (Panel)panel2.Controls[i];
                                RadioButton vic = (RadioButton)temp.Controls[1];
                                RadioButton def = (RadioButton)temp.Controls[2];
                                if (vic.Checked)
                                {
                                    //was victory
                                    f.value = "Win";
                                }
                                else if (def.Checked)
                                {
                                    //was defeat
                                    f.value = "Loss (loss)";
                                }
                                else
                                {
                                    //was draw
                                    f.value = "Loss (draw)";
                                }
                                //trim the string to make sure
                                //f.name = f.name + ": ";
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else if (f.type == 2)
                            {
                                //special case date
                                Panel temp = (Panel)panel2.Controls[i];
                                DateTimePicker lValue = (DateTimePicker)temp.Controls[1];
                                f.value = lValue.Text;
                                //trim the string to make sure
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else if (f.type == 4)
                            {
                                //special case youtube
                                Panel temp = (Panel)panel2.Controls[i];
                                TextBox tb = (TextBox)temp.Controls[1];
                                f.value = t.youtubeEmbedStartURL + tb.Text + t.youtubeEmbedEndURL;
                                //trim the string to make sure
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + " (Youtube)" + ": \n" + f.value + "\n\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else
                            {
                                //normal cases
                                Panel temp = (Panel)panel2.Controls[i];
                                Label lName = (Label)temp.Controls[0];
                                TextBox tb = (TextBox)temp.Controls[1];
                                f.name = lName.Text;
                                f.value = tb.Text;
                                //trim the string to make sure
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                this.appendEntry(t.ToString(), f.name + ": ", f.value);
                            }
                        }
                    }
                    else
                    {
                        //series or stronghold type template
                        //compile list of each
                        foreach (Field f in t.fieldList)
                        {
                            if (!f.duplicate)
                            {
                                //is single field
                                singleFields.Add(f);
                            }
                            else
                            {
                                //is double field
                                doubleFields.Add(f);
                            }
                        }
                        //run through list of single fields
                        //single type template
                        for (int i = 0; i < singleFields.Count; i++)
                        {
                            Field f = singleFields[i];
                            if (f.type == 3)
                            {
                                //special case victory/defeat
                                Panel temp = (Panel)panel2.Controls[i];
                                Label lName = (Label)temp.Controls[0];
                                RadioButton vic = (RadioButton)temp.Controls[1];
                                RadioButton def = (RadioButton)temp.Controls[2];
                                if (vic.Checked)
                                {
                                    //was victory
                                    f.value = "Win";
                                }
                                else if (def.Checked)
                                {
                                    //was defeat
                                    f.value = "Loss (loss)";
                                }
                                else
                                {
                                    //was draw
                                    f.value = "Loss (draw)";
                                }
                                //trim the field just to be safe
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else if (f.type == 2)
                            {
                                //special case date
                                Panel temp = (Panel)panel2.Controls[i];
                                Label lName = (Label)temp.Controls[0];
                                DateTimePicker lValue = (DateTimePicker)temp.Controls[1];
                                f.name = lName.Text ;
                                f.value = lValue.Text;
                                //trim the field just to be safe
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else if (f.type == 4)
                            {
                                //special case youtube
                                Panel temp = (Panel)panel2.Controls[i];
                                Label lName = (Label)temp.Controls[0];
                                TextBox tb = (TextBox)temp.Controls[1];
                                f.name = lName.Text ;
                                f.value = t.youtubeEmbedStartURL + tb.Text + t.youtubeEmbedEndURL;
                                //trim the field just to be safe
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + " (Youtube)" + ": \n" + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                            }
                            else
                            {
                                //normal cases
                                Panel temp = (Panel)panel2.Controls[i];
                                Label lName = (Label)temp.Controls[0];
                                TextBox tb = (TextBox)temp.Controls[1];
                                f.name = lName.Text ;
                                f.value = tb.Text;
                                //trim the field just to be safe
                                f.name = f.name.Substring(0, f.nameLength);
                                if (f.inBody) bodySB.Append(f.name + ": " + f.value + "\n");
                                if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                this.appendEntry(t.ToString(), f.name + ": ", f.value);
                            }
                        }
                        //run through list of double fields
                        //double type template
                        int anotherTemp = 0;
                        for (int j = 1; j < battleCount + 1; j++)
                        {
                            bodySB.Append("\n\n");
                            anotherTemp = singleFields.Count;
                            for (int i = singleFields.Count + ((j - 1) * doubleFields.Count); i < doubleFields.Count + singleFields.Count + ((j - 1) * doubleFields.Count); i++)
                            {
                                int fieldIndex = anotherTemp - singleFields.Count;
                                Field f = doubleFields[fieldIndex];
                                if (f.type == 3)
                                {
                                    //special case victory/defeat
                                    Panel temp = (Panel)panel2.Controls[i];
                                    Label lName = (Label)temp.Controls[0];
                                    f.name = this.parseName(f.name, f.nameLength);
                                    RadioButton vic = (RadioButton)temp.Controls[1];
                                    RadioButton def = (RadioButton)temp.Controls[2];
                                    if (vic.Checked)
                                    {
                                        //was victory
                                        f.value = "Win";
                                    }
                                    else if (def.Checked)
                                    {
                                        //was defeat
                                        f.value = "Loss (loss)";
                                    }
                                    else
                                    {
                                        //was draw
                                        f.value = "Loss (draw)";
                                    }
                                    if (f.inBody) bodySB.Append(f.name + " " + j + ": " + t.fieldList[anotherTemp].value + "\n");
                                    if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                }
                                else if (f.type == 2)
                                {
                                    //special case date
                                    Panel temp = (Panel)panel2.Controls[i];
                                    Label lName = (Label)temp.Controls[0];
                                    f.name = this.parseName(f.name, f.nameLength);
                                    DateTimePicker lValue = (DateTimePicker)temp.Controls[1];
                                    f.value = lValue.Text;
                                    if (f.inBody) bodySB.Append(f.name + " " + j + ": " + t.fieldList[anotherTemp].value + "\n");
                                    if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                }
                                else if (f.type == 4)
                                {
                                    //special case youtube
                                    Panel temp = (Panel)panel2.Controls[i];
                                    Label lName = (Label)temp.Controls[0];
                                    f.name = this.parseName(f.name, f.nameLength);
                                    TextBox tb = (TextBox)temp.Controls[1];
                                    f.value = t.youtubeEmbedStartURL + tb.Text + t.youtubeEmbedEndURL;
                                    if (f.inBody) bodySB.Append(f.name + " " + j + ": \n" + t.fieldList[anotherTemp].value + "\n");
                                    if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                }
                                else
                                {
                                    //normal cases
                                    Panel temp = (Panel)panel2.Controls[i];
                                    Label lName = (Label)temp.Controls[0];
                                    f.name = this.parseName(f.name, f.nameLength);
                                    TextBox tb = (TextBox)temp.Controls[1];
                                    f.value = tb.Text;
                                    if (f.inBody) bodySB.Append(f.name + " " + j + ": " + t.fieldList[anotherTemp].value + "\n");
                                    if (f.inTitle) headerList[f.titleIndex - 1] = f;
                                    this.appendEntry(t.ToString(), f.name, f.value);
                                }
                                anotherTemp++;
                            }
                        }
                    }
                    //build the string
                    //first thread title
                    int k = 0;
                    while (headerList[k] != null)
                    {
                        if (headerList[k + 1] == null)
                        {
                            if (t.templateType == 1)
                            {
                                //last single header event, don't add space
                                titleSB.Append(headerList[k].value);
                            }
                            else if (t.templateType == 2)
                            {
                                //last series header event, add type
                                titleSB.Append(headerList[k].value + t.delimiter + "series");
                            }
                            else
                            {
                                //last sh header event, add type
                                titleSB.Append(headerList[k].value + t.delimiter + "stronghold");
                            }
                            
                        }
                        else
                        {
                            titleSB.Append(headerList[k].value + t.delimiter);
                        }
                        k++;
                    }
                    //then thread body
                    if (t.templateType == 1)
                    {
                        //single
                        for (int i = 0; i < t.fieldList.Count; i++)
                        {
                            //bodySB.Append(t.fieldList[i].name + t.fieldList[i].value + "\n");
                        }
                    }
                    else
                    {
                        //not single
                        for (int i = 0; i < singleFields.Count; i++)
                        {
                            //
                        }
                        for (int j = 1; j < battleCount + 1; j++)
                        {
                            for (int i = singleFields.Count; i < doubleFields.Count + singleFields.Count; i++)
                            {
                                //
                            }
                        }
                    }
                    //add version information
                    bodySB.Append("\n\nCreated using ReplayTemplate V_" + version);
                    //output to window
                    textOut.title.Text = titleSB.ToString();
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
            t.delimiter = temp.delimiter;
            t.numFields = temp.numFields;
            t.templateType = temp.templateType;
            return t;
        }

        private void saveTemplateButton_Click(object sender, EventArgs e)
        {
            this.saveTemplates();
        }

        private void resetUI()
        {
            tabInc = 0;
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
            delimiterTextBox.Text = "null";
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
                //this is old and needs to be updated
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
                    templateWriter.WriteElementString("delimiter", templateList[i].delimiter);
                    templateWriter.WriteStartElement("fields");
                    for (int j = 0; j < templateList[i].fieldList.Count; j++)
                    {
                        Field f = templateList[i].fieldList[j];
                        templateWriter.WriteStartElement("field");
                        templateWriter.WriteElementString("name", f.name);
                        templateWriter.WriteElementString("duplicate", "" + f.duplicate);
                        templateWriter.WriteElementString("type", "" + f.type);
                        templateWriter.WriteElementString("inTitle", "" + f.inTitle);
                        templateWriter.WriteElementString("inBody", "" + f.inBody);
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
                                case "delimiter":
                                    t.delimiter = templateReader.ReadString();
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
                                                        case "nameLength":
                                                            f.nameLength = int.Parse(templateReader.ReadString());
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
            this.sortTemplates();
            this.resetUI();
        }

        private void sortTemplates()
        {
            templateList.Sort(Template.CompareTemplates);
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
            }
                //for each battle, for each item in the array, add it to the list
                for (int i = currentBattle; i < battleCount + 1; i++)
                {
                    for (int j = 0; j < tempList.Count; j++)
                    {
                        int selection = tempList[j].type;
                        if (selection == 1)
                        {
                            //standard
                            tempList[j].name = this.parseName(tempList[j].name, tempList[j].nameLength);
                            this.addStandard(displayTemplate, tempList[j], tempList[j].name + " " + i);
                        }
                        else if (selection == 2)
                        {
                            //date
                            tempList[j].name = this.parseName(tempList[j].name, tempList[j].nameLength);
                            this.addDate(tempList[j], tempList[j].name + " " + i);
                        }
                        else if (selection == 3)
                        {
                            //victoryDefeat
                            tempList[j].name = this.parseName(tempList[j].name, tempList[j].nameLength);
                            this.addVictoryDefeat(tempList[j], tempList[j].name + " " + i);
                        }
                        else if (selection == 4)
                        {
                            //youtube
                            tempList[j].name = this.parseName(tempList[j].name, tempList[j].nameLength);
                            this.addYoutube(tempList[j], tempList[j].name + " " + i);
                        }
                        else
                        {
                            MessageBox.Show("Database error. The program must exit. Tell Willster419 he screwed up. A lot.");
                            this.Close();
                        }
                    }
                }
            
            lastSelected = int.Parse(numBattlesComboBox.Text);
            lastTemplateComboBoxSelectedIndex = templateComboBox.SelectedIndex;
        }

        private string parseName(string name, int intendedLength)
        {
            string temp = name.Substring(0, intendedLength);
            return temp;
        }

        private void Victory_Focused(Object sender, System.EventArgs e)
        {
            victory.TabStop = false;
            defeat.TabStop = true;
            draw.TabStop = true;
        }

        private void Victory_Unfocused(Object sender, System.EventArgs e)
        {
            //victory.TabStop = true;
            //defeat.TabStop = false;
            //draw.TabStop = false;
        }

        private void Defeat_Focused(Object sender, System.EventArgs e)
        {
            victory.TabStop = false;
            defeat.TabStop = false;
            draw.TabStop = true;
        }

        private void Defeat_Unfocused(Object sender, System.EventArgs e)
        {
            victory.TabStop = true;
            defeat.TabStop = false;
            draw.TabStop = false;
        }

        private void panel2_Focused(Object sender, System.EventArgs e)
        {
            resetUIButton.TabStop = true;
            saveTemplateButton.TabStop = true;
            createThreadButton.TabStop = true;
            clearHistoryButton.TabStop = true;
        }

        private void createThreadButton_Unfocused(Object sender, System.EventArgs e)
        {
            resetUIButton.TabStop = false;
            saveTemplateButton.TabStop = false;
            createThreadButton.TabStop = false;
            clearHistoryButton.TabStop = false;
        }

        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            //create messagebox for are you sure
            DialogResult result = MessageBox.Show("Are you sure you want to delete the cache of all clan data?", "Are you sure", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (Directory.Exists(cachePath)) Directory.Delete(cachePath, true);
            }
        }

        private void setupCache(Template t)
        {
            //check for main folder
            templateCachePath = cachePath + "\\" + t.ToString();
            if (!Directory.Exists(templateCachePath)) Directory.CreateDirectory(templateCachePath);
            //check for subfolders
            foreach (Field f in t.fieldList)
            {
                if (f.type == 1)
                {
                    //is standard type, make history for it
                    string[] realName = f.name.Split(':');
                    if (realName.Length > 2)
                    {
                        //user used ':' in entry, abort saving
                        return;
                    }
                    string fieldCachePath = templateCachePath + "\\" + realName[0];
                    if (!Directory.Exists(fieldCachePath)) Directory.CreateDirectory(fieldCachePath);
                    string historyPath = fieldCachePath + "\\history.txt";
                    if (!File.Exists(historyPath)) File.WriteAllText(historyPath, "");
                }
            }
        }

        private void appendEntry(string clanName, string fieldName, string value)
        {
            string[] data3 = fieldName.Split(':');
            if (data3.Length > 2)
            {
                //user used ':' in entry, abort saving
                return;
            }
            string filename = cachePath + "\\" + clanName + "\\" + data3[0] + "\\history.txt";
            string data = File.ReadAllText(filename, Encoding.UTF8);
            string[] data2 = data.Split(':');
            foreach (string s in data2)
            {
                //duplicate value
                if (s.Equals(value)) return;
            }
            data = data + ":" + value;
            File.Delete(filename);
            File.WriteAllText(filename, data, Encoding.UTF8);
        }

        private string[] loadHistory(string clanName, string fieldName)
        {
            string[] data2 = fieldName.Split(':');
            if (data2.Length > 2)
            {
                //user used ':' in entry, abort saving
                return null;
            }
            string filename = cachePath + "\\" + clanName + "\\" + data2[0] + "\\history.txt";
            string data = File.ReadAllText(filename, Encoding.UTF8);
            return data.Split(':');
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (pause)
            {
                //toggle the form disabled
                pauseLabel.Visible = true;
                //get the current enabled status of specific form contorls saved and write them all to false
                int temp = this.Controls.Count;
                foreach (Control c in this.Controls)
                {
                    bool controlVisableTemp = c.Enabled;
                    if (c.Name.Equals("pauseButton"))
                    {
                        controlVisableTemp = true;
                    }
                    toggleVisableList.Add(controlVisableTemp);
                    if (!c.Name.Equals("pauseButton")) c.Enabled = false;
                }
                pauseButton.Text = "resume";
                pause = false;
            }
            else
            {
                //toggle the form enabled
                pauseLabel.Visible = false;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    this.Controls[i].Enabled = toggleVisableList[i];
                }
                toggleVisableList = new List<bool>();
                pauseButton.Text = "pause";
                pause = true;
            }
        }
    }
}
