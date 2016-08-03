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
            this.SuspendLayout();
            // 
            // createThreadButton
            // 
            this.createThreadButton.Location = new System.Drawing.Point(256, 505);
            this.createThreadButton.Name = "createThreadButton";
            this.createThreadButton.Size = new System.Drawing.Size(110, 23);
            this.createThreadButton.TabIndex = 0;
            this.createThreadButton.Text = "create replay thread";
            this.createThreadButton.UseVisualStyleBackColor = true;
            this.createThreadButton.Click += new System.EventHandler(this.createThreadButton_Click);
            // 
            // resetUIButton
            // 
            this.resetUIButton.Location = new System.Drawing.Point(12, 505);
            this.resetUIButton.Name = "resetUIButton";
            this.resetUIButton.Size = new System.Drawing.Size(60, 23);
            this.resetUIButton.TabIndex = 1;
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
            this.templateComboBox.TabIndex = 2;
            this.templateComboBox.SelectedIndexChanged += new System.EventHandler(this.templateComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Template";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(12, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 447);
            this.panel2.TabIndex = 7;
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Location = new System.Drawing.Point(69, 9);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(87, 13);
            this.selectionLabel.TabIndex = 8;
            this.selectionLabel.Text = "Nothing selected";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Youtube Embed style";
            // 
            // youtubeEmbedStartTextBox
            // 
            this.youtubeEmbedStartTextBox.Location = new System.Drawing.Point(233, 26);
            this.youtubeEmbedStartTextBox.Name = "youtubeEmbedStartTextBox";
            this.youtubeEmbedStartTextBox.ReadOnly = true;
            this.youtubeEmbedStartTextBox.Size = new System.Drawing.Size(68, 20);
            this.youtubeEmbedStartTextBox.TabIndex = 10;
            this.youtubeEmbedStartTextBox.Text = "null";
            // 
            // youtubeEmbedEndTextBox
            // 
            this.youtubeEmbedEndTextBox.Location = new System.Drawing.Point(307, 26);
            this.youtubeEmbedEndTextBox.Name = "youtubeEmbedEndTextBox";
            this.youtubeEmbedEndTextBox.ReadOnly = true;
            this.youtubeEmbedEndTextBox.Size = new System.Drawing.Size(59, 20);
            this.youtubeEmbedEndTextBox.TabIndex = 11;
            this.youtubeEmbedEndTextBox.Text = "null";
            // 
            // saveTemplateButton
            // 
            this.saveTemplateButton.Enabled = false;
            this.saveTemplateButton.Location = new System.Drawing.Point(78, 505);
            this.saveTemplateButton.Name = "saveTemplateButton";
            this.saveTemplateButton.Size = new System.Drawing.Size(83, 23);
            this.saveTemplateButton.TabIndex = 12;
            this.saveTemplateButton.Text = "save template";
            this.saveTemplateButton.UseVisualStyleBackColor = true;
            this.saveTemplateButton.Click += new System.EventHandler(this.saveTemplateButton_Click);
            // 
            // loadTemplatesButton
            // 
            this.loadTemplatesButton.Enabled = false;
            this.loadTemplatesButton.Location = new System.Drawing.Point(167, 505);
            this.loadTemplatesButton.Name = "loadTemplatesButton";
            this.loadTemplatesButton.Size = new System.Drawing.Size(83, 23);
            this.loadTemplatesButton.TabIndex = 13;
            this.loadTemplatesButton.Text = "load templates";
            this.loadTemplatesButton.UseVisualStyleBackColor = true;
            this.loadTemplatesButton.Click += new System.EventHandler(this.loadTemplatesButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 540);
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
            this.Text = "Main Window";
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
    }
}

