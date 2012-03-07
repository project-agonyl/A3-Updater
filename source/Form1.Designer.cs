namespace A3Updator
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.a3HeadLabel = new System.Windows.Forms.Label();
            this.a3ProgressBar = new System.Windows.Forms.ProgressBar();
            this.a3UpdateButton = new System.Windows.Forms.Button();
            this.a3LaunchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.a3LinkLabel = new System.Windows.Forms.LinkLabel();
            this.a3FieldLabel = new System.Windows.Forms.Label();
            this.a3FileLabel = new System.Windows.Forms.Label();
            this.a3ProgressLabel = new System.Windows.Forms.Label();
            this.a3SizeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // a3HeadLabel
            // 
            this.a3HeadLabel.AutoSize = true;
            this.a3HeadLabel.Location = new System.Drawing.Point(220, 9);
            this.a3HeadLabel.Name = "a3HeadLabel";
            this.a3HeadLabel.Size = new System.Drawing.Size(92, 13);
            this.a3HeadLabel.TabIndex = 0;
            this.a3HeadLabel.Text = "A3 Game Updater";
            // 
            // a3ProgressBar
            // 
            this.a3ProgressBar.Location = new System.Drawing.Point(51, 150);
            this.a3ProgressBar.Name = "a3ProgressBar";
            this.a3ProgressBar.Size = new System.Drawing.Size(443, 30);
            this.a3ProgressBar.TabIndex = 1;
            // 
            // a3UpdateButton
            // 
            this.a3UpdateButton.Location = new System.Drawing.Point(51, 53);
            this.a3UpdateButton.Name = "a3UpdateButton";
            this.a3UpdateButton.Size = new System.Drawing.Size(186, 31);
            this.a3UpdateButton.TabIndex = 2;
            this.a3UpdateButton.Text = "Update Game";
            this.a3UpdateButton.UseVisualStyleBackColor = true;
            this.a3UpdateButton.Click += new System.EventHandler(this.a3UpdateButton_Click);
            // 
            // a3LaunchButton
            // 
            this.a3LaunchButton.Location = new System.Drawing.Point(315, 52);
            this.a3LaunchButton.Name = "a3LaunchButton";
            this.a3LaunchButton.Size = new System.Drawing.Size(178, 31);
            this.a3LaunchButton.TabIndex = 3;
            this.a3LaunchButton.Text = "Launch Game";
            this.a3LaunchButton.UseVisualStyleBackColor = true;
            this.a3LaunchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Created By Karthik P";
            // 
            // a3LinkLabel
            // 
            this.a3LinkLabel.AutoSize = true;
            this.a3LinkLabel.Location = new System.Drawing.Point(312, 224);
            this.a3LinkLabel.Name = "a3LinkLabel";
            this.a3LinkLabel.Size = new System.Drawing.Size(200, 13);
            this.a3LinkLabel.TabIndex = 5;
            this.a3LinkLabel.TabStop = true;
            this.a3LinkLabel.Text = "Thanks to Ragezone development forum";
            this.a3LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.a3LinkLabel_LinkClicked);
            // 
            // a3FieldLabel
            // 
            this.a3FieldLabel.AutoSize = true;
            this.a3FieldLabel.Location = new System.Drawing.Point(48, 114);
            this.a3FieldLabel.Name = "a3FieldLabel";
            this.a3FieldLabel.Size = new System.Drawing.Size(116, 13);
            this.a3FieldLabel.TabIndex = 6;
            this.a3FieldLabel.Text = "File being downloaded:";
            // 
            // a3FileLabel
            // 
            this.a3FileLabel.AutoSize = true;
            this.a3FileLabel.Location = new System.Drawing.Point(175, 116);
            this.a3FileLabel.Name = "a3FileLabel";
            this.a3FileLabel.Size = new System.Drawing.Size(0, 13);
            this.a3FileLabel.TabIndex = 7;
            // 
            // a3ProgressLabel
            // 
            this.a3ProgressLabel.AutoSize = true;
            this.a3ProgressLabel.Location = new System.Drawing.Point(285, 116);
            this.a3ProgressLabel.Name = "a3ProgressLabel";
            this.a3ProgressLabel.Size = new System.Drawing.Size(83, 13);
            this.a3ProgressLabel.TabIndex = 8;
            this.a3ProgressLabel.Text = "Progress(Bytes):";
            // 
            // a3SizeLabel
            // 
            this.a3SizeLabel.AutoSize = true;
            this.a3SizeLabel.Location = new System.Drawing.Point(374, 118);
            this.a3SizeLabel.Name = "a3SizeLabel";
            this.a3SizeLabel.Size = new System.Drawing.Size(0, 13);
            this.a3SizeLabel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(567, 262);
            this.Controls.Add(this.a3SizeLabel);
            this.Controls.Add(this.a3ProgressLabel);
            this.Controls.Add(this.a3FileLabel);
            this.Controls.Add(this.a3FieldLabel);
            this.Controls.Add(this.a3LinkLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.a3LaunchButton);
            this.Controls.Add(this.a3UpdateButton);
            this.Controls.Add(this.a3ProgressBar);
            this.Controls.Add(this.a3HeadLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "A3 Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label a3HeadLabel;
        private System.Windows.Forms.ProgressBar a3ProgressBar;
        private System.Windows.Forms.Button a3UpdateButton;
        private System.Windows.Forms.Button a3LaunchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel a3LinkLabel;
        private System.Windows.Forms.Label a3FieldLabel;
        private System.Windows.Forms.Label a3FileLabel;
        private System.Windows.Forms.Label a3ProgressLabel;
        private System.Windows.Forms.Label a3SizeLabel;
    }
}

