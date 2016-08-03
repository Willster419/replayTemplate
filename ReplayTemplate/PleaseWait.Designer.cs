namespace ReplayTemplate
{
    partial class PleaseWait
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
            this.loadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.loadingMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadingProgressBar
            // 
            this.loadingProgressBar.Location = new System.Drawing.Point(12, 46);
            this.loadingProgressBar.Name = "loadingProgressBar";
            this.loadingProgressBar.Size = new System.Drawing.Size(260, 23);
            this.loadingProgressBar.TabIndex = 0;
            // 
            // loadingMessage
            // 
            this.loadingMessage.AutoSize = true;
            this.loadingMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingMessage.Location = new System.Drawing.Point(13, 13);
            this.loadingMessage.Name = "loadingMessage";
            this.loadingMessage.Size = new System.Drawing.Size(86, 17);
            this.loadingMessage.TabIndex = 1;
            this.loadingMessage.Text = "Sample Text";
            // 
            // PleaseWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 81);
            this.Controls.Add(this.loadingMessage);
            this.Controls.Add(this.loadingProgressBar);
            this.Name = "PleaseWait";
            this.Text = "PleaseWait";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar loadingProgressBar;
        private System.Windows.Forms.Label loadingMessage;
    }
}