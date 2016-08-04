namespace ReplayTemplate
{
    partial class UpdateWindow
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
            this.updateAvailableLabel = new System.Windows.Forms.Label();
            this.updateNotesRTB = new System.Windows.Forms.RichTextBox();
            this.updateQuestionlabel = new System.Windows.Forms.Label();
            this.noButton = new System.Windows.Forms.Button();
            this.yesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // updateAvailableLabel
            // 
            this.updateAvailableLabel.AutoSize = true;
            this.updateAvailableLabel.Location = new System.Drawing.Point(12, 9);
            this.updateAvailableLabel.Name = "updateAvailableLabel";
            this.updateAvailableLabel.Size = new System.Drawing.Size(111, 13);
            this.updateAvailableLabel.TabIndex = 0;
            this.updateAvailableLabel.Text = "An update is available";
            // 
            // updateNotesRTB
            // 
            this.updateNotesRTB.Location = new System.Drawing.Point(15, 25);
            this.updateNotesRTB.Name = "updateNotesRTB";
            this.updateNotesRTB.ReadOnly = true;
            this.updateNotesRTB.Size = new System.Drawing.Size(257, 163);
            this.updateNotesRTB.TabIndex = 1;
            this.updateNotesRTB.Text = "";
            // 
            // updateQuestionlabel
            // 
            this.updateQuestionlabel.AutoSize = true;
            this.updateQuestionlabel.Location = new System.Drawing.Point(12, 191);
            this.updateQuestionlabel.Name = "updateQuestionlabel";
            this.updateQuestionlabel.Size = new System.Drawing.Size(128, 13);
            this.updateQuestionlabel.TabIndex = 2;
            this.updateQuestionlabel.Text = "would you like to update?";
            // 
            // noButton
            // 
            this.noButton.Location = new System.Drawing.Point(12, 226);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 3;
            this.noButton.Text = "No.";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(197, 226);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 4;
            this.yesButton.Text = "Yeah!";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // UpdateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.updateQuestionlabel);
            this.Controls.Add(this.updateNotesRTB);
            this.Controls.Add(this.updateAvailableLabel);
            this.Name = "UpdateWindow";
            this.Text = "UpdateWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label updateAvailableLabel;
        public System.Windows.Forms.RichTextBox updateNotesRTB;
        private System.Windows.Forms.Label updateQuestionlabel;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Button yesButton;

    }
}