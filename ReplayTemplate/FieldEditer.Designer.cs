namespace ReplayTemplate
{
    partial class FieldEditer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectFieldLabel = new System.Windows.Forms.Label();
            this.selectFieldComboBox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.fieldTypeLabel = new System.Windows.Forms.Label();
            this.fieldTypeComboBox = new System.Windows.Forms.ComboBox();
            this.fieldNameLabel = new System.Windows.Forms.Label();
            this.fieldnameTextBox = new System.Windows.Forms.TextBox();
            this.inHeaderCheckBox = new System.Windows.Forms.CheckBox();
            this.positionLabel = new System.Windows.Forms.Label();
            this.inBodyCheckBox = new System.Windows.Forms.CheckBox();
            this.previewLabel = new System.Windows.Forms.Label();
            this.insertCheckBox = new System.Windows.Forms.CheckBox();
            this.insertComboBox = new System.Windows.Forms.ComboBox();
            this.isDuplicateCheckBox = new System.Windows.Forms.CheckBox();
            this.headerPositionTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // selectFieldLabel
            // 
            this.selectFieldLabel.AutoSize = true;
            this.selectFieldLabel.Location = new System.Drawing.Point(20, 9);
            this.selectFieldLabel.Name = "selectFieldLabel";
            this.selectFieldLabel.Size = new System.Drawing.Size(69, 13);
            this.selectFieldLabel.TabIndex = 0;
            this.selectFieldLabel.Text = "select field to";
            // 
            // selectFieldComboBox
            // 
            this.selectFieldComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectFieldComboBox.FormattingEnabled = true;
            this.selectFieldComboBox.Location = new System.Drawing.Point(23, 25);
            this.selectFieldComboBox.Name = "selectFieldComboBox";
            this.selectFieldComboBox.Size = new System.Drawing.Size(121, 21);
            this.selectFieldComboBox.TabIndex = 1;
            this.selectFieldComboBox.SelectedIndexChanged += new System.EventHandler(this.selectFieldComboBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(23, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 45);
            this.panel1.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(23, 198);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(278, 198);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // fieldTypeLabel
            // 
            this.fieldTypeLabel.AutoSize = true;
            this.fieldTypeLabel.Location = new System.Drawing.Point(20, 49);
            this.fieldTypeLabel.Name = "fieldTypeLabel";
            this.fieldTypeLabel.Size = new System.Drawing.Size(49, 13);
            this.fieldTypeLabel.TabIndex = 8;
            this.fieldTypeLabel.Text = "field type";
            // 
            // fieldTypeComboBox
            // 
            this.fieldTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldTypeComboBox.FormattingEnabled = true;
            this.fieldTypeComboBox.Items.AddRange(new object[] {
            "1 - standard",
            "2 - date",
            "3 - victoryDefeatDraw",
            "4 - youtube"});
            this.fieldTypeComboBox.Location = new System.Drawing.Point(23, 65);
            this.fieldTypeComboBox.Name = "fieldTypeComboBox";
            this.fieldTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.fieldTypeComboBox.TabIndex = 9;
            this.fieldTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.fieldTypeComboBox_SelectedIndexChanged);
            // 
            // fieldNameLabel
            // 
            this.fieldNameLabel.AutoSize = true;
            this.fieldNameLabel.Location = new System.Drawing.Point(185, 68);
            this.fieldNameLabel.Name = "fieldNameLabel";
            this.fieldNameLabel.Size = new System.Drawing.Size(55, 13);
            this.fieldNameLabel.TabIndex = 10;
            this.fieldNameLabel.Text = "field name";
            // 
            // fieldnameTextBox
            // 
            this.fieldnameTextBox.Location = new System.Drawing.Point(246, 65);
            this.fieldnameTextBox.Name = "fieldnameTextBox";
            this.fieldnameTextBox.Size = new System.Drawing.Size(100, 20);
            this.fieldnameTextBox.TabIndex = 11;
            this.fieldnameTextBox.TextChanged += new System.EventHandler(this.fieldnameTextBox_TextChanged);
            // 
            // inHeaderCheckBox
            // 
            this.inHeaderCheckBox.AutoSize = true;
            this.inHeaderCheckBox.Location = new System.Drawing.Point(29, 92);
            this.inHeaderCheckBox.Name = "inHeaderCheckBox";
            this.inHeaderCheckBox.Size = new System.Drawing.Size(69, 17);
            this.inHeaderCheckBox.TabIndex = 12;
            this.inHeaderCheckBox.Text = "inHeader";
            this.inHeaderCheckBox.UseVisualStyleBackColor = true;
            this.inHeaderCheckBox.CheckedChanged += new System.EventHandler(this.inHeaderCheckBox_CheckedChanged);
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(20, 112);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(43, 13);
            this.positionLabel.TabIndex = 13;
            this.positionLabel.Text = "position";
            // 
            // inBodyCheckBox
            // 
            this.inBodyCheckBox.AutoSize = true;
            this.inBodyCheckBox.Location = new System.Drawing.Point(153, 92);
            this.inBodyCheckBox.Name = "inBodyCheckBox";
            this.inBodyCheckBox.Size = new System.Drawing.Size(58, 17);
            this.inBodyCheckBox.TabIndex = 14;
            this.inBodyCheckBox.Text = "inBody";
            this.inBodyCheckBox.UseVisualStyleBackColor = true;
            this.inBodyCheckBox.CheckedChanged += new System.EventHandler(this.inBodyCheckBox_CheckedChanged);
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
            this.previewLabel.Location = new System.Drawing.Point(20, 131);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Size = new System.Drawing.Size(47, 13);
            this.previewLabel.TabIndex = 16;
            this.previewLabel.Text = "preview:";
            // 
            // insertCheckBox
            // 
            this.insertCheckBox.AutoSize = true;
            this.insertCheckBox.Location = new System.Drawing.Point(158, 115);
            this.insertCheckBox.Name = "insertCheckBox";
            this.insertCheckBox.Size = new System.Drawing.Size(84, 17);
            this.insertCheckBox.TabIndex = 17;
            this.insertCheckBox.Text = "insert above";
            this.insertCheckBox.UseVisualStyleBackColor = true;
            this.insertCheckBox.CheckedChanged += new System.EventHandler(this.moveCheckBox_CheckedChanged);
            // 
            // insertComboBox
            // 
            this.insertComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.insertComboBox.FormattingEnabled = true;
            this.insertComboBox.Location = new System.Drawing.Point(246, 113);
            this.insertComboBox.Name = "insertComboBox";
            this.insertComboBox.Size = new System.Drawing.Size(121, 21);
            this.insertComboBox.TabIndex = 18;
            // 
            // isDuplicateCheckBox
            // 
            this.isDuplicateCheckBox.AutoSize = true;
            this.isDuplicateCheckBox.Location = new System.Drawing.Point(246, 92);
            this.isDuplicateCheckBox.Name = "isDuplicateCheckBox";
            this.isDuplicateCheckBox.Size = new System.Drawing.Size(78, 17);
            this.isDuplicateCheckBox.TabIndex = 19;
            this.isDuplicateCheckBox.Text = "isDuplicate";
            this.isDuplicateCheckBox.UseVisualStyleBackColor = true;
            this.isDuplicateCheckBox.CheckedChanged += new System.EventHandler(this.isDuplicateCheckBox_CheckedChanged);
            // 
            // headerPositionTextBox
            // 
            this.headerPositionTextBox.Enabled = false;
            this.headerPositionTextBox.Location = new System.Drawing.Point(69, 109);
            this.headerPositionTextBox.Name = "headerPositionTextBox";
            this.headerPositionTextBox.Size = new System.Drawing.Size(29, 20);
            this.headerPositionTextBox.TabIndex = 20;
            this.headerPositionTextBox.TextChanged += new System.EventHandler(this.headerPositionTextBox_TextChanged);
            // 
            // FieldEditer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 233);
            this.Controls.Add(this.headerPositionTextBox);
            this.Controls.Add(this.isDuplicateCheckBox);
            this.Controls.Add(this.insertComboBox);
            this.Controls.Add(this.insertCheckBox);
            this.Controls.Add(this.previewLabel);
            this.Controls.Add(this.inBodyCheckBox);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.inHeaderCheckBox);
            this.Controls.Add(this.fieldnameTextBox);
            this.Controls.Add(this.fieldNameLabel);
            this.Controls.Add(this.fieldTypeComboBox);
            this.Controls.Add(this.fieldTypeLabel);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectFieldComboBox);
            this.Controls.Add(this.selectFieldLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FieldEditer";
            this.Text = "FieldEditer";
            this.Load += new System.EventHandler(this.FieldEditer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectFieldLabel;
        private System.Windows.Forms.ComboBox selectFieldComboBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label fieldTypeLabel;
        private System.Windows.Forms.ComboBox fieldTypeComboBox;
        private System.Windows.Forms.Label fieldNameLabel;
        private System.Windows.Forms.TextBox fieldnameTextBox;
        private System.Windows.Forms.CheckBox inHeaderCheckBox;
        private System.Windows.Forms.Label positionLabel;
        private System.Windows.Forms.CheckBox inBodyCheckBox;
        private System.Windows.Forms.Label previewLabel;
        private System.Windows.Forms.CheckBox insertCheckBox;
        private System.Windows.Forms.ComboBox insertComboBox;
        private System.Windows.Forms.CheckBox isDuplicateCheckBox;
        private System.Windows.Forms.TextBox headerPositionTextBox;
    }
}