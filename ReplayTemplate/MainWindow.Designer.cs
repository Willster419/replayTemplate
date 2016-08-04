namespace ReplayTemplate
{
    partial class MainWindow
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
            this.createThreadButton = new System.Windows.Forms.Button();
            this.resetUIButton = new System.Windows.Forms.Button();
            this.uploadDownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.selectionLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.youtubeEmbedStartTextBox = new System.Windows.Forms.TextBox();
            this.youtubeEmbedEndTextBox = new System.Windows.Forms.TextBox();
            this.saveTemplateButton = new System.Windows.Forms.Button();
            this.loadTemplatesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numFieldsTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.templateTypeTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numBattlesComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // createThreadButton
            // 
            this.createThreadButton.Location = new System.Drawing.Point(256, 505);
            this.createThreadButton.Name = "createThreadButton";
            this.createThreadButton.Size = new System.Drawing.Size(110, 23);
            this.createThreadButton.TabIndex = 6;
            this.createThreadButton.TabStop = false;
            this.createThreadButton.Text = "create replay thread";
            this.createThreadButton.UseVisualStyleBackColor = true;
            this.createThreadButton.Click += new System.EventHandler(this.createThreadButton_Click);
            // 
            // resetUIButton
            // 
            this.resetUIButton.Location = new System.Drawing.Point(12, 505);
            this.resetUIButton.Name = "resetUIButton";
            this.resetUIButton.Size = new System.Drawing.Size(60, 23);
            this.resetUIButton.TabIndex = 3;
            this.resetUIButton.TabStop = false;
            this.resetUIButton.Text = "reset";
            this.resetUIButton.UseVisualStyleBackColor = true;
            this.resetUIButton.Click += new System.EventHandler(this.resetUIButton_Click);
            // 
            // uploadDownloadWorker
            // 
            this.uploadDownloadWorker.WorkerReportsProgress = true;
            // 
            // templateComboBox
            // 
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(12, 25);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(144, 21);
            this.templateComboBox.TabIndex = 0;
            this.templateComboBox.SelectedIndexChanged += new System.EventHandler(this.templateComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Template";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(12, 91);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 408);
            this.panel2.TabIndex = 2;
            this.panel2.TabStop = true;
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Location = new System.Drawing.Point(69, 9);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(87, 13);
            this.selectionLabel.TabIndex = 16;
            this.selectionLabel.Text = "Nothing selected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Youtube Embed style";
            // 
            // youtubeEmbedStartTextBox
            // 
            this.youtubeEmbedStartTextBox.Location = new System.Drawing.Point(60, 65);
            this.youtubeEmbedStartTextBox.Name = "youtubeEmbedStartTextBox";
            this.youtubeEmbedStartTextBox.ReadOnly = true;
            this.youtubeEmbedStartTextBox.Size = new System.Drawing.Size(68, 20);
            this.youtubeEmbedStartTextBox.TabIndex = 8;
            this.youtubeEmbedStartTextBox.TabStop = false;
            this.youtubeEmbedStartTextBox.Text = "null";
            // 
            // youtubeEmbedEndTextBox
            // 
            this.youtubeEmbedEndTextBox.Location = new System.Drawing.Point(134, 65);
            this.youtubeEmbedEndTextBox.Name = "youtubeEmbedEndTextBox";
            this.youtubeEmbedEndTextBox.ReadOnly = true;
            this.youtubeEmbedEndTextBox.Size = new System.Drawing.Size(59, 20);
            this.youtubeEmbedEndTextBox.TabIndex = 9;
            this.youtubeEmbedEndTextBox.TabStop = false;
            this.youtubeEmbedEndTextBox.Text = "null";
            // 
            // saveTemplateButton
            // 
            this.saveTemplateButton.Location = new System.Drawing.Point(78, 505);
            this.saveTemplateButton.Name = "saveTemplateButton";
            this.saveTemplateButton.Size = new System.Drawing.Size(83, 23);
            this.saveTemplateButton.TabIndex = 4;
            this.saveTemplateButton.TabStop = false;
            this.saveTemplateButton.Text = "save template";
            this.saveTemplateButton.UseVisualStyleBackColor = true;
            this.saveTemplateButton.Click += new System.EventHandler(this.saveTemplateButton_Click);
            // 
            // loadTemplatesButton
            // 
            this.loadTemplatesButton.Location = new System.Drawing.Point(167, 505);
            this.loadTemplatesButton.Name = "loadTemplatesButton";
            this.loadTemplatesButton.Size = new System.Drawing.Size(83, 23);
            this.loadTemplatesButton.TabIndex = 5;
            this.loadTemplatesButton.TabStop = false;
            this.loadTemplatesButton.Text = "load templates";
            this.loadTemplatesButton.UseVisualStyleBackColor = true;
            this.loadTemplatesButton.Click += new System.EventHandler(this.loadTemplatesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "# fields";
            // 
            // numFieldsTextBox
            // 
            this.numFieldsTextBox.Location = new System.Drawing.Point(12, 65);
            this.numFieldsTextBox.Name = "numFieldsTextBox";
            this.numFieldsTextBox.ReadOnly = true;
            this.numFieldsTextBox.Size = new System.Drawing.Size(23, 20);
            this.numFieldsTextBox.TabIndex = 7;
            this.numFieldsTextBox.TabStop = false;
            this.numFieldsTextBox.Text = "null";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "type";
            // 
            // templateTypeTextBox
            // 
            this.templateTypeTextBox.Location = new System.Drawing.Point(207, 66);
            this.templateTypeTextBox.Name = "templateTypeTextBox";
            this.templateTypeTextBox.ReadOnly = true;
            this.templateTypeTextBox.Size = new System.Drawing.Size(73, 20);
            this.templateTypeTextBox.TabIndex = 10;
            this.templateTypeTextBox.TabStop = false;
            this.templateTypeTextBox.Text = "null";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(298, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "# battles";
            // 
            // numBattlesComboBox
            // 
            this.numBattlesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.numBattlesComboBox.FormattingEnabled = true;
            this.numBattlesComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.numBattlesComboBox.Location = new System.Drawing.Point(301, 65);
            this.numBattlesComboBox.Name = "numBattlesComboBox";
            this.numBattlesComboBox.Size = new System.Drawing.Size(34, 21);
            this.numBattlesComboBox.TabIndex = 1;
            this.numBattlesComboBox.SelectedIndexChanged += new System.EventHandler(this.numBattlesComboBox_SelectedIndexChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 540);
            this.Controls.Add(this.numBattlesComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.templateTypeTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numFieldsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.loadTemplatesButton);
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
            this.Name = "MainWindow";
            this.Text = "99";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createThreadButton;
        private System.Windows.Forms.Button resetUIButton;
        private System.ComponentModel.BackgroundWorker uploadDownloadWorker;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label selectionLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox youtubeEmbedStartTextBox;
        private System.Windows.Forms.TextBox youtubeEmbedEndTextBox;
        private System.Windows.Forms.Button saveTemplateButton;
        private System.Windows.Forms.Button loadTemplatesButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numFieldsTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox templateTypeTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox numBattlesComboBox;
    }
}

