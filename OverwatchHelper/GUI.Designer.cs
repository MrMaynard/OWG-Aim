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
            this.sensBox = new System.Windows.Forms.TextBox();
            this.shootBox = new System.Windows.Forms.CheckBox();
            this.predictionBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.aimBox = new System.Windows.Forms.ComboBox();
            this.windowBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.liveButton = new System.Windows.Forms.Button();
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
            // sensBox
            // 
            this.sensBox.Location = new System.Drawing.Point(88, 62);
            this.sensBox.Name = "sensBox";
            this.sensBox.Size = new System.Drawing.Size(100, 20);
            this.sensBox.TabIndex = 6;
            this.sensBox.Text = "8.0";
            this.sensBox.TextChanged += new System.EventHandler(this.sensBox_TextChanged);
            // 
            // shootBox
            // 
            this.shootBox.AutoSize = true;
            this.shootBox.Location = new System.Drawing.Point(12, 144);
            this.shootBox.Name = "shootBox";
            this.shootBox.Size = new System.Drawing.Size(83, 17);
            this.shootBox.TabIndex = 7;
            this.shootBox.Text = "Shoot mode";
            this.shootBox.UseVisualStyleBackColor = true;
            this.shootBox.CheckedChanged += new System.EventHandler(this.shootBox_CheckedChanged);
            // 
            // predictionBox
            // 
            this.predictionBox.AutoSize = true;
            this.predictionBox.Location = new System.Drawing.Point(12, 167);
            this.predictionBox.Name = "predictionBox";
            this.predictionBox.Size = new System.Drawing.Size(103, 17);
            this.predictionBox.TabIndex = 8;
            this.predictionBox.Text = "Prediction Mode";
            this.predictionBox.UseVisualStyleBackColor = true;
            this.predictionBox.CheckedChanged += new System.EventHandler(this.predictionBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Aim key:";
            // 
            // aimBox
            // 
            this.aimBox.FormattingEnabled = true;
            this.aimBox.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "0",
            ";",
            "\\",
            " ",
            "\"",
            ".",
            "|",
            "-",
            ",",
            "?",
            "+",
            "[",
            "]",
            "MB1",
            "MB2",
            "MB3"});
            this.aimBox.Location = new System.Drawing.Point(66, 122);
            this.aimBox.Name = "aimBox";
            this.aimBox.Size = new System.Drawing.Size(66, 21);
            this.aimBox.TabIndex = 10;
            this.aimBox.SelectedIndexChanged += new System.EventHandler(this.aimBox_SelectedIndexChanged);
            // 
            // windowBox
            // 
            this.windowBox.Location = new System.Drawing.Point(88, 33);
            this.windowBox.Name = "windowBox";
            this.windowBox.Size = new System.Drawing.Size(100, 20);
            this.windowBox.TabIndex = 12;
            this.windowBox.Text = "500";
            this.windowBox.TextChanged += new System.EventHandler(this.windowBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Lock window:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Sensitivity:";
            // 
            // liveButton
            // 
            this.liveButton.Location = new System.Drawing.Point(156, 225);
            this.liveButton.Name = "liveButton";
            this.liveButton.Size = new System.Drawing.Size(75, 23);
            this.liveButton.TabIndex = 15;
            this.liveButton.Text = "Stop capturing";
            this.liveButton.UseVisualStyleBackColor = true;
            this.liveButton.Click += new System.EventHandler(this.liveButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 288);
            this.Controls.Add(this.liveButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.windowBox);
            this.Controls.Add(this.aimBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.predictionBox);
            this.Controls.Add(this.shootBox);
            this.Controls.Add(this.sensBox);
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
        private System.Windows.Forms.TextBox sensBox;
        private System.Windows.Forms.CheckBox shootBox;
        private System.Windows.Forms.CheckBox predictionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox aimBox;
        private System.Windows.Forms.TextBox windowBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button liveButton;
    }
}

