namespace ReplayTemplate
{
    partial class TemplateEditerWindow
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
            this.clearHistoryButton = new System.Windows.Forms.Button();
            this.delimiterTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numBattlesComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.templateTypeTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numFieldsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveTemplateButton = new System.Windows.Forms.Button();
            this.youtubeEmbedEndTextBox = new System.Windows.Forms.TextBox();
            this.youtubeEmbedStartTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectionLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.resetUIButton = new System.Windows.Forms.Button();
            this.createThreadButton = new System.Windows.Forms.Button();
            this.selectTypeComboBox = new System.Windows.Forms.ComboBox();
            this.editFieldButton = new System.Windows.Forms.Button();
            this.addFieldButton = new System.Windows.Forms.Button();
            this.removeFieldButton = new System.Windows.Forms.Button();
            this.loadTemplateButton = new System.Windows.Forms.Button();
            this.loadTemplateDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTemplateDialog = new System.Windows.Forms.SaveFileDialog();
            this.clanNamelabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.headerLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // clearHistoryButton
            // 
            this.clearHistoryButton.Enabled = false;
            this.clearHistoryButton.Location = new System.Drawing.Point(146, 505);
            this.clearHistoryButton.Name = "clearHistoryButton";
            this.clearHistoryButton.Size = new System.Drawing.Size(74, 23);
            this.clearHistoryButton.TabIndex = 38;
            this.clearHistoryButton.TabStop = false;
            this.clearHistoryButton.Text = "clear history";
            this.clearHistoryButton.UseVisualStyleBackColor = true;
            // 
            // delimiterTextBox
            // 
            this.delimiterTextBox.Location = new System.Drawing.Point(334, 65);
            this.delimiterTextBox.Name = "delimiterTextBox";
            this.delimiterTextBox.Size = new System.Drawing.Size(32, 20);
            this.delimiterTextBox.TabIndex = 37;
            this.delimiterTextBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(331, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "delimiter";
            // 
            // numBattlesComboBox
            // 
            this.numBattlesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numBattlesComboBox.Enabled = false;
            this.numBattlesComboBox.FormattingEnabled = true;
            this.numBattlesComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.numBattlesComboBox.Location = new System.Drawing.Point(284, 64);
            this.numBattlesComboBox.Name = "numBattlesComboBox";
            this.numBattlesComboBox.Size = new System.Drawing.Size(34, 21);
            this.numBattlesComboBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(281, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "# battles";
            // 
            // templateTypeTextBox
            // 
            this.templateTypeTextBox.Location = new System.Drawing.Point(205, 26);
            this.templateTypeTextBox.Name = "templateTypeTextBox";
            this.templateTypeTextBox.ReadOnly = true;
            this.templateTypeTextBox.Size = new System.Drawing.Size(73, 20);
            this.templateTypeTextBox.TabIndex = 29;
            this.templateTypeTextBox.TabStop = false;
            this.templateTypeTextBox.Text = "null";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "type";
            // 
            // numFieldsTextBox
            // 
            this.numFieldsTextBox.Location = new System.Drawing.Point(12, 65);
            this.numFieldsTextBox.Name = "numFieldsTextBox";
            this.numFieldsTextBox.ReadOnly = true;
            this.numFieldsTextBox.Size = new System.Drawing.Size(34, 20);
            this.numFieldsTextBox.TabIndex = 26;
            this.numFieldsTextBox.TabStop = false;
            this.numFieldsTextBox.Text = "null";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "# fields";
            // 
            // saveTemplateButton
            // 
            this.saveTemplateButton.Location = new System.Drawing.Point(57, 505);
            this.saveTemplateButton.Name = "saveTemplateButton";
            this.saveTemplateButton.Size = new System.Drawing.Size(83, 23);
            this.saveTemplateButton.TabIndex = 24;
            this.saveTemplateButton.TabStop = false;
            this.saveTemplateButton.Text = "save template";
            this.saveTemplateButton.UseVisualStyleBackColor = true;
            // 
            // youtubeEmbedEndTextBox
            // 
            this.youtubeEmbedEndTextBox.Location = new System.Drawing.Point(134, 64);
            this.youtubeEmbedEndTextBox.Name = "youtubeEmbedEndTextBox";
            this.youtubeEmbedEndTextBox.Size = new System.Drawing.Size(59, 20);
            this.youtubeEmbedEndTextBox.TabIndex = 28;
            this.youtubeEmbedEndTextBox.TabStop = false;
            this.youtubeEmbedEndTextBox.Text = "end";
            // 
            // youtubeEmbedStartTextBox
            // 
            this.youtubeEmbedStartTextBox.Location = new System.Drawing.Point(60, 65);
            this.youtubeEmbedStartTextBox.Name = "youtubeEmbedStartTextBox";
            this.youtubeEmbedStartTextBox.Size = new System.Drawing.Size(68, 20);
            this.youtubeEmbedStartTextBox.TabIndex = 27;
            this.youtubeEmbedStartTextBox.TabStop = false;
            this.youtubeEmbedStartTextBox.Text = "start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Youtube Embed style";
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Location = new System.Drawing.Point(69, 9);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(87, 13);
            this.selectionLabel.TabIndex = 35;
            this.selectionLabel.Text = "Nothing selected";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(12, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 358);
            this.panel2.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Template";
            // 
            // templateComboBox
            // 
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.Enabled = false;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(12, 25);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(144, 21);
            this.templateComboBox.TabIndex = 20;
            // 
            // resetUIButton
            // 
            this.resetUIButton.Location = new System.Drawing.Point(12, 505);
            this.resetUIButton.Name = "resetUIButton";
            this.resetUIButton.Size = new System.Drawing.Size(39, 23);
            this.resetUIButton.TabIndex = 23;
            this.resetUIButton.TabStop = false;
            this.resetUIButton.Text = "reset";
            this.resetUIButton.UseVisualStyleBackColor = true;
            // 
            // createThreadButton
            // 
            this.createThreadButton.Location = new System.Drawing.Point(226, 505);
            this.createThreadButton.Name = "createThreadButton";
            this.createThreadButton.Size = new System.Drawing.Size(140, 23);
            this.createThreadButton.TabIndex = 25;
            this.createThreadButton.TabStop = false;
            this.createThreadButton.Text = "create sample thread";
            this.createThreadButton.UseVisualStyleBackColor = true;
            // 
            // selectTypeComboBox
            // 
            this.selectTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectTypeComboBox.FormattingEnabled = true;
            this.selectTypeComboBox.Items.AddRange(new object[] {
            "single",
            "series",
            "stronghold"});
            this.selectTypeComboBox.Location = new System.Drawing.Point(205, 63);
            this.selectTypeComboBox.Name = "selectTypeComboBox";
            this.selectTypeComboBox.Size = new System.Drawing.Size(73, 21);
            this.selectTypeComboBox.TabIndex = 39;
            // 
            // editFieldButton
            // 
            this.editFieldButton.Location = new System.Drawing.Point(93, 480);
            this.editFieldButton.Name = "editFieldButton";
            this.editFieldButton.Size = new System.Drawing.Size(75, 23);
            this.editFieldButton.TabIndex = 40;
            this.editFieldButton.Text = "edit field...";
            this.editFieldButton.UseVisualStyleBackColor = true;
            this.editFieldButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addFieldButton
            // 
            this.addFieldButton.Location = new System.Drawing.Point(12, 480);
            this.addFieldButton.Name = "addFieldButton";
            this.addFieldButton.Size = new System.Drawing.Size(75, 23);
            this.addFieldButton.TabIndex = 41;
            this.addFieldButton.Text = "Add Field...";
            this.addFieldButton.UseVisualStyleBackColor = true;
            this.addFieldButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeFieldButton
            // 
            this.removeFieldButton.Location = new System.Drawing.Point(174, 480);
            this.removeFieldButton.Name = "removeFieldButton";
            this.removeFieldButton.Size = new System.Drawing.Size(84, 23);
            this.removeFieldButton.TabIndex = 42;
            this.removeFieldButton.Text = "remove field...";
            this.removeFieldButton.UseVisualStyleBackColor = true;
            this.removeFieldButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // loadTemplateButton
            // 
            this.loadTemplateButton.Location = new System.Drawing.Point(264, 480);
            this.loadTemplateButton.Name = "loadTemplateButton";
            this.loadTemplateButton.Size = new System.Drawing.Size(102, 23);
            this.loadTemplateButton.TabIndex = 43;
            this.loadTemplateButton.Text = "load template";
            this.loadTemplateButton.UseVisualStyleBackColor = true;
            // 
            // loadTemplateDialog
            // 
            this.loadTemplateDialog.FileName = "openFileDialog1";
            // 
            // clanNamelabel
            // 
            this.clanNamelabel.AutoSize = true;
            this.clanNamelabel.Location = new System.Drawing.Point(284, 8);
            this.clanNamelabel.Name = "clanNamelabel";
            this.clanNamelabel.Size = new System.Drawing.Size(56, 13);
            this.clanNamelabel.TabIndex = 44;
            this.clanNamelabel.Text = "clan name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(284, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 45;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Location = new System.Drawing.Point(10, 93);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(43, 13);
            this.headerLabel.TabIndex = 46;
            this.headerLabel.Text = "header:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(56, 90);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(310, 20);
            this.textBox2.TabIndex = 47;
            // 
            // TemplateEditerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 536);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.clanNamelabel);
            this.Controls.Add(this.loadTemplateButton);
            this.Controls.Add(this.removeFieldButton);
            this.Controls.Add(this.addFieldButton);
            this.Controls.Add(this.editFieldButton);
            this.Controls.Add(this.selectTypeComboBox);
            this.Controls.Add(this.clearHistoryButton);
            this.Controls.Add(this.delimiterTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numBattlesComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.templateTypeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numFieldsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveTemplateButton);
            this.Controls.Add(this.youtubeEmbedEndTextBox);
            this.Controls.Add(this.youtubeEmbedStartTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.selectionLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.resetUIButton);
            this.Controls.Add(this.createThreadButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TemplateEditerWindow";
            this.Text = "TemplateEditerWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clearHistoryButton;
        private System.Windows.Forms.TextBox delimiterTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox numBattlesComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox templateTypeTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox numFieldsTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveTemplateButton;
        private System.Windows.Forms.TextBox youtubeEmbedEndTextBox;
        private System.Windows.Forms.TextBox youtubeEmbedStartTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label selectionLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.Button resetUIButton;
        private System.Windows.Forms.Button createThreadButton;
        private System.Windows.Forms.ComboBox selectTypeComboBox;
        private System.Windows.Forms.Button editFieldButton;
        private System.Windows.Forms.Button addFieldButton;
        private System.Windows.Forms.Button removeFieldButton;
        private System.Windows.Forms.Button loadTemplateButton;
        private System.Windows.Forms.OpenFileDialog loadTemplateDialog;
        private System.Windows.Forms.SaveFileDialog saveTemplateDialog;
        private System.Windows.Forms.Label clanNamelabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.TextBox textBox2;
    }
}