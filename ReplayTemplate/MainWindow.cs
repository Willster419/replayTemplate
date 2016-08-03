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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DEBUG ONLY
            //this.addStandard(new Field("test"));
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
            dateLabel.Text = String.Format("{0:MM/dd/yy}",date);
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

        private void displayTemplate(int index)
        {
            Template t = templateList[index];
            //infrom which is selected for verification
            selectionLabel.Text = t.clanName + " selected";
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

        private void createSampleREL2Fields()
        {
            rel2.fieldList.Add(new Field("Date",2));
            rel2.fieldList.Add(new Field("Opponent"));
            rel2.fieldList.Add(new Field("Province"));
            rel2.fieldList.Add(new Field("Map"));
            rel2.fieldList.Add(new Field("FC"));
            rel2.fieldList.Add(new Field("Strat"));
            rel2.fieldList.Add(new Field("Strat",4));
            rel2.fieldList.Add(new Field("Result",3));
            rel2.fieldList.Add(new Field("Replay File"));
            rel2.fieldList.Add(new Field("Battle",4));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            rel2.clanName = "REL2";
            rel2.threadURL = "http://relicgaming.com/index.php?action=post;board=24.0";
            this.createSampleREL2Fields();
            templateList.Add(rel2);
            this.updateTemplateComboBox();
        }

        private void templateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (templateComboBox.Text.Equals("create custom..."))
            {
                //launch editor

            }
            else
            {
                //load template
                this.displayTemplate(templateComboBox.SelectedIndex);
            }
        }
        //updates template combo box with list of templates
        private void updateTemplateComboBox()
        {
            for (int i = 0; i < templateList.Count; i++)
            {
                templateComboBox.Items.Add(templateList[i]);
            }
            templateComboBox.Items.Add("create custom...");
        }

        private void resetThreadButton_Click(object sender, EventArgs e)
        {
            //infrom which is selected for verification
            selectionLabel.Text = "Nothing selected";
            //clear the current panel
            while (panel2.Controls.Count != 0)
            {
                panel2.Controls.RemoveAt(0);
            }
        }

        private void editFieldButton_Click(object sender, EventArgs e)
        {
            edit.ShowDialog();
        }

        private void createThreadButton_Click(object sender, EventArgs e)
        {
            bodySB = new StringBuilder();
            textOut.body.Text = "";
            if (templateComboBox.Text.Equals("create custom..."))
            {

            }
            else
            {
                //create new UNLINKED LIST
                this.createNewUnlinkedList();
                Template t = tempTemplateList[templateComboBox.SelectedIndex];
                for (int i = 0; i < t.fieldList.Count; i++)
                {
                    Field f = t.fieldList[i];
                    if (f.type == 3)
                    {
                        //special case victory/defeat
                        Panel temp = (Panel)panel2.Controls[i];
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
                        t.fieldList[i].name = t.fieldList[i].name + ": ";
                    }
                    else if (f.type == 2)
                    {
                        //special case date
                        Panel temp = (Panel)panel2.Controls[i];
                        Label l = (Label)temp.Controls[1];
                        string s = l.Text;
                        t.fieldList[i].name = t.fieldList[i].name + ": ";
                        t.fieldList[i].value = s;
                    }
                    else if (f.type == 4)
                    {
                        //special case youtube
                        Panel temp = (Panel)panel2.Controls[i];
                        TextBox tb = (TextBox)temp.Controls[1];
                        string s = tb.Text;
                        t.fieldList[i].name = t.fieldList[i].name + ": \n";
                        t.fieldList[i].value = "[youtube]" + s + "[/youtube]";
                    }
                    else
                    {
                        //normal cases
                        Panel temp = (Panel)panel2.Controls[i];
                        TextBox tb = (TextBox)temp.Controls[1];
                        string s = tb.Text;
                        t.fieldList[i].name = t.fieldList[i].name + ": ";
                        t.fieldList[i].value = s;
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
        private void createNewUnlinkedList()
        {
            tempTemplateList = new List<Template>();
            foreach (Template temp in templateList)
            {
                Template newTemp = new Template();
                newTemp.clanName = temp.clanName;
                for (int i = 0; i < temp.fieldList.Count; i++)
                {
                    Field f = new Field(temp.fieldList[i].name, temp.fieldList[i].type);
                    f.value = temp.fieldList[i].value;
                    newTemp.fieldList.Add(f);
                }
                newTemp.fieldList = temp.fieldList;
                newTemp.threadURL = temp.threadURL;
                tempTemplateList.Add(newTemp);
            }
        }
    }
}
