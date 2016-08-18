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
        public List<Field> fieldList;
        private Size labelSize = new Size(LABEL_WIDTH, LABEL_HEIGHT);
        private Size textBoxSize = new Size(TEXTBOX_SIZE_WIDTH, TEXTBOX_SIZE_HEIGHT);
        private Size checkBoxSize = new Size(CHECKBOX_WIDTH, CHECKBOX_HEIGHT);
        private Size titleIndexSize = new Size(CHECKBOX_HEIGHT, CHECKBOX_HEIGHT);
        private TextBox tb;
        private CheckBox bodyCB;
        private CheckBox headerCB;
        private RadioButton victory;
        private RadioButton defeat;
        private RadioButton draw;
        private DateTimePicker dtp;
        private TextBox titleIndexTB;
        private Label l;
        private bool loading = false;
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
            selectFieldComboBox.Items.Clear();
            insertComboBox.Items.Clear();
            if (fieldList != null)
            {
                foreach (Field f in fieldList)
                {
                    selectFieldComboBox.Items.Add(f);
                    insertComboBox.Items.Add(f);
                }
            }
            if (mode == 1)
            {
                //adding
                selectFieldComboBox.Enabled = false;
                selectFieldLabel.Text = "Create field to add";
                //indexComboBox.Enabled = false;
                insertCheckBox.Enabled = true;
                insertComboBox.Enabled = false;
                updateButton.Text = "add";
            }
            else if (mode == 2)
            {
                //editing
                selectFieldLabel.Text = "Select field to edit";
                fieldTypeComboBox.Enabled = false;
                insertCheckBox.Enabled = false;
                insertComboBox.Enabled = false;
                updateButton.Text = "update";
            }
            else if (mode == 3)
            {
                //removing
                selectFieldLabel.Text = "Select field to remove";
                //indexComboBox.Enabled = false;
                updateButton.Enabled = false;
                insertCheckBox.Enabled = false;
                insertComboBox.Enabled = false;
                inBodyCheckBox.Enabled = false;
                inHeaderCheckBox.Enabled = false;
                fieldnameTextBox.Enabled = false;
                fieldTypeComboBox.Enabled = false;
                updateButton.Text = "remove";
                isDuplicateCheckBox.Enabled = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void inHeaderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            //verify field is selected for body and/or header
            if (!inHeaderCheckBox.Checked && !inBodyCheckBox.Checked)
            {
                MessageBox.Show("Field must be in eithor body or header");
                return;
            }
            //field name can't be blank
            if (fieldnameTextBox.Text.Equals("") && fieldnameTextBox.Enabled)
            {
                MessageBox.Show("Field name can't be blank yo");
                return;
            }
            //field can't be a duplicate
            int numHitz = 0;
            foreach (Field fi in fieldList)
            {
                if (fi.name.Equals(fieldnameTextBox.Text))
                {
                    numHitz++;
                    if (mode == 3)//if removal mode
                    {
                        //don't worry about it
                    }
                    else if (mode == 2)//if edit mode
                    {
                        if (numHitz == 2)
                        {

                        }
                    }
                    else if (mode == 1)
                    {
                        if (fi.type != f.type)
                        {

                        }
                        else
                        {
                            MessageBox.Show("duplicates names fields aren't allowed bruh");
                            return;
                        }
                    }
                    else { }
                }
            }
            if (insertCheckBox.Checked && insertComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("You need to select a place to move it to lol");
                return;
            }

            //check for duplicates all on botom
            if (mode == 1 || mode == 2)// if adding or editing
            {
                int numchanges = 0;
                List<Field> tempList = new List<Field>(fieldList);
                //apply temp changes based on update or add
                if (mode == 1)
                {
                    //adding
                    if (insertCheckBox.Checked)
                    {
                        //adding at position
                        tempList.Insert(insertComboBox.SelectedIndex, f);
                    }
                    else
                    {
                        //adding at bottom
                        tempList.Add(f);
                    }
                }
                else if (mode == 2)
                {
                    //editing
                    tempList[selectFieldComboBox.SelectedIndex] = f;
                }
                //now actually check for all duplicates on the bottom
                bool firstAssignment = true;
                bool lastChangedValue = false;
                foreach (Field fi in tempList)
                {
                    if (firstAssignment)
                    {
                        if (fi.duplicate)
                        {
                            MessageBox.Show("First field can't be a duplicate.");
                            return;
                        }
                        lastChangedValue = fi.duplicate;
                        firstAssignment = false;
                    }
                    else
                    {
                        if (fi.duplicate != lastChangedValue)
                        {
                            numchanges++;
                            lastChangedValue = fi.duplicate;
                        }
                    }
                }
                if (numchanges > 1)
                {
                    MessageBox.Show("All duplicate fields must be on the bottom");
                    return;
                }
            }
            //write logic for correct index number
            if (f.inTitle && f.titleIndex == 0)
            {
                MessageBox.Show("Header index can't be 0");
                return;
            }
            //write logic for not taken index number
            int numHeaderFields = 0;
            foreach (Field fi in fieldList)
            {
                if (fi.inTitle) numHeaderFields++;
                if (fi.titleIndex == f.titleIndex && f.inTitle)
                {
                    MessageBox.Show("That header index is already taken");
                    return;
                }
            }
            if (f.titleIndex > numHeaderFields+1)
            {
                MessageBox.Show("Header index can't be greator than the total number of fields with headers");
                return;
            }
            if (f.inTitle && f.duplicate)
            {
                MessageBox.Show("Duplicate item can't be a header item");
                return;
            }
            //apply changes based on remove, update, or add
            if (mode == 1)
            {
                //adding
                if (insertCheckBox.Checked)
                {
                    //adding at position
                    fieldList.Insert(insertComboBox.SelectedIndex, f);
                }
                else
                {
                    //adding at bottom
                    fieldList.Add(f);
                }
            }
            else if (mode == 2)
            {
                //editing
                fieldList[selectFieldComboBox.SelectedIndex] = f;
            }
            else if (mode == 3)
            {
                //removing
                fieldList.RemoveAt(selectFieldComboBox.SelectedIndex);
            }
            this.Close();
        }

        private void fieldTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateButton.Enabled = true;
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void updateUI()
        {
            if (fieldTypeComboBox.SelectedIndex == -1)
            {
                return;
            }
            panel1.Controls.Clear();

            //if adding a new field
            f = new Field(fieldnameTextBox.Text, fieldTypeComboBox.SelectedIndex + 1);
            f.inBody = inBodyCheckBox.Checked;
            f.inTitle = inHeaderCheckBox.Checked;
            f.titleIndex = 0;
            f.value = "";
            f.duplicate = isDuplicateCheckBox.Checked;
            if (f.inTitle)
            {
                headerPositionTextBox.Enabled = true;
                headerPositionTextBox.Visible = true;
                int test;
                if (int.TryParse(headerPositionTextBox.Text, out test)) f.titleIndex = int.Parse(headerPositionTextBox.Text);
            }
            else
            {
                headerPositionTextBox.Text = "";
                headerPositionTextBox.Enabled = false;
                headerPositionTextBox.Visible = false;
            }

            if (f.duplicate)
            {
                panel1.BackColor = SystemColors.ControlDark;
            }
            else
            {
                panel1.BackColor = SystemColors.Control;
            }
            setupTextBox();
            setupBodyCheckBox();
            setupHeaderCheckBox();
            if (f.inTitle) setupIndexBox();
            if (fieldTypeComboBox.SelectedIndex == 1)
            {
                //date
                setupTitleLabel("date");
                fieldnameTextBox.Text = "date";
                fieldnameTextBox.Enabled = false;
                setupDate();
            }
            else if (fieldTypeComboBox.SelectedIndex == 2)
            {
                //vicdefdraw
                setupTitleLabel("result");
                fieldnameTextBox.Text = "result";
                fieldnameTextBox.Enabled = false;
                setupRadioButtons();
            }
            else if (fieldTypeComboBox.SelectedIndex == 3)
            {
                //youtube
                fieldnameTextBox.Enabled = true;
                setupTitleLabel(f.name + " (youtube)");
            }
            else
            {
                //standard
                setupTitleLabel(f.name);
                fieldnameTextBox.Enabled = true;
            }
            panel1.Controls.Add(l);
            if (fieldTypeComboBox.SelectedIndex == 1)
            {
                //date
                panel1.Controls.Add(dtp);
            }
            else if (fieldTypeComboBox.SelectedIndex == 2)
            {
                //vicdefdraw
                panel1.Controls.Add(victory);
                panel1.Controls.Add(defeat);
                panel1.Controls.Add(draw);
            }
            else
            {
                //anything else
                panel1.Controls.Add(tb);
            }
            panel1.Controls.Add(bodyCB);
            panel1.Controls.Add(headerCB);
            panel1.Controls.Add(titleIndexTB);

        }

        private void setupTitleLabel(string name)
        {
            //setup the label in the panel
            l = new Label()
            {
                Text = name,
                Location = new Point(DELIMITER, DELIMITER),
                Size = labelSize
            };
        }


        private void setupTextBox()
        {
            tb = new TextBox()
            {
                Location = new Point(DELIMITER, TEXTBOX_LOCATION_Y),
                Size = textBoxSize,
            };
        }

        private void setupBodyCheckBox()
        {
            bodyCB = new CheckBox()
            {
                Enabled = false,
                Checked = f.inBody,
                Text = "in body",
                Location = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER),
                Size = checkBoxSize
            };
        }

        private void setupHeaderCheckBox()
        {
            headerCB = new CheckBox()
            {
                Enabled = false,
                Checked = f.inTitle,
                Text = "in header",
                Location = new Point(LABEL_WIDTH + DELIMITER, CHECKBOX_DELIMITER),
                Size = checkBoxSize
            };
        }

        private void setupRadioButtons()
        {
            victory = new RadioButton()
            {
                Text = "Win",
                Location = new Point(DELIMITER, TEXTBOX_LOCATION_Y)
            };
            defeat = new RadioButton()
            {
                Text = "Loss (loss)",
                Location = new Point(DELIMITER + victory.Size.Width + DELIMITER, TEXTBOX_LOCATION_Y)
            };
            draw = new RadioButton()
            {
                Text = "Loss (draw)",
                Location = new Point(DELIMITER + victory.Size.Width + DELIMITER + defeat.Size.Width + DELIMITER, TEXTBOX_LOCATION_Y)
            };
        }

        private void setupDate()
        {
            dtp = new DateTimePicker()
           {
               Format = DateTimePickerFormat.Custom,
               CustomFormat = "MM/dd/yy",
               ShowUpDown = true,
               Location = new Point(DELIMITER, TEXTBOX_LOCATION_Y),
               Size = labelSize
           };
        }

        private void setupIndexBox()
        {
            titleIndexTB = new TextBox()
            {
                Text = "" + f.titleIndex,
                Location = new Point(LABEL_WIDTH + DELIMITER + CHECKBOX_WIDTH + DELIMITER, CHECKBOX_DELIMITER),
                Size = titleIndexSize,
                ReadOnly = true
            };

        }

        private void fieldnameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void inBodyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void moveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (insertCheckBox.Checked) insertComboBox.Enabled = true;
            else insertComboBox.Enabled = false;
        }

        private int getIndex(Field f)
        {
            for (int i = 0; i < fieldList.Count; i++)
            {
                if (fieldList[i].name.Equals(f.name)) return i;
            }
            return -1;
        }

        private void selectFieldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loading = true;
            f = fieldList[selectFieldComboBox.SelectedIndex];
            inHeaderCheckBox.Checked = f.inTitle;
            inBodyCheckBox.Checked = f.inBody;
            fieldnameTextBox.Text = f.name;
            fieldTypeComboBox.SelectedIndex = f.type - 1;
            isDuplicateCheckBox.Checked = f.duplicate;
            loading = false;
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void isDuplicateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }

        private void headerPositionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (fieldTypeComboBox.SelectedIndex != -1 && !loading) updateUI();
        }
    }
}
