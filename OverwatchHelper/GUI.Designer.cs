namespace OverwatchHelper
{
    partial class GUI
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
            this.captureButton = new System.Windows.Forms.Button();
            this.debugButton = new System.Windows.Forms.Button();
            this.xBox = new System.Windows.Forms.TextBox();
            this.yBox = new System.Windows.Forms.TextBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.sensButton = new System.Windows.Forms.Button();
            this.sensBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(12, 254);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(151, 22);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "Capture";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // debugButton
            // 
            this.debugButton.Location = new System.Drawing.Point(12, 225);
            this.debugButton.Name = "debugButton";
            this.debugButton.Size = new System.Drawing.Size(92, 23);
            this.debugButton.TabIndex = 1;
            this.debugButton.Text = "Debug";
            this.debugButton.UseVisualStyleBackColor = true;
            this.debugButton.Click += new System.EventHandler(this.debugButton_Click);
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(93, 196);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(74, 20);
            this.xBox.TabIndex = 2;
            // 
            // yBox
            // 
            this.yBox.Location = new System.Drawing.Point(173, 196);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(74, 20);
            this.yBox.TabIndex = 3;
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(12, 196);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(75, 23);
            this.moveButton.TabIndex = 4;
            this.moveButton.Text = "Move to:";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // sensButton
            // 
            this.sensButton.Location = new System.Drawing.Point(12, 67);
            this.sensButton.Name = "sensButton";
            this.sensButton.Size = new System.Drawing.Size(75, 23);
            this.sensButton.TabIndex = 5;
            this.sensButton.Text = "Update Sensitivity";
            this.sensButton.UseVisualStyleBackColor = true;
            this.sensButton.Click += new System.EventHandler(this.sensButton_Click);
            // 
            // sensBox
            // 
            this.sensBox.Location = new System.Drawing.Point(93, 67);
            this.sensBox.Name = "sensBox";
            this.sensBox.Size = new System.Drawing.Size(100, 20);
            this.sensBox.TabIndex = 6;
            this.sensBox.Text = "8.0";
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 288);
            this.Controls.Add(this.sensBox);
            this.Controls.Add(this.sensButton);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.debugButton);
            this.Controls.Add(this.captureButton);
            this.Name = "GUI";
            this.Text = "Overwatch Helper 0.2b";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Button debugButton;
        private System.Windows.Forms.TextBox xBox;
        private System.Windows.Forms.TextBox yBox;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button sensButton;
        private System.Windows.Forms.TextBox sensBox;
    }
}

