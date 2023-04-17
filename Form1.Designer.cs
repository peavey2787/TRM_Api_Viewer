namespace TRM_Api_Viewer
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
            this.View_Button = new System.Windows.Forms.Button();
            this.Worker_Ip_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Worker_Name_TextBox = new System.Windows.Forms.TextBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.View_All_Button = new System.Windows.Forms.Button();
            this.Edit_Settings_Button = new System.Windows.Forms.Button();
            this.Miner_Settings_ListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Add_Settings_Button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Delete_Button = new System.Windows.Forms.Button();
            this.Network_Scan_Count_Label = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Scan_for_Workers_Button = new System.Windows.Forms.Button();
            this.Workers_Panel = new System.Windows.Forms.Panel();
            this.Auto_Start_CheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // View_Button
            // 
            this.View_Button.Location = new System.Drawing.Point(23, 259);
            this.View_Button.Name = "View_Button";
            this.View_Button.Size = new System.Drawing.Size(40, 27);
            this.View_Button.TabIndex = 7;
            this.View_Button.Text = "View";
            this.View_Button.UseVisualStyleBackColor = true;
            this.View_Button.Click += new System.EventHandler(this.View_Button_Click);
            // 
            // Worker_Ip_TextBox
            // 
            this.Worker_Ip_TextBox.Location = new System.Drawing.Point(85, 66);
            this.Worker_Ip_TextBox.Name = "Worker_Ip_TextBox";
            this.Worker_Ip_TextBox.Size = new System.Drawing.Size(79, 20);
            this.Worker_Ip_TextBox.TabIndex = 1;
            this.Worker_Ip_TextBox.Text = "192.168.1.000";
            this.Worker_Ip_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name";
            // 
            // Worker_Name_TextBox
            // 
            this.Worker_Name_TextBox.Location = new System.Drawing.Point(52, 37);
            this.Worker_Name_TextBox.Name = "Worker_Name_TextBox";
            this.Worker_Name_TextBox.Size = new System.Drawing.Size(112, 20);
            this.Worker_Name_TextBox.TabIndex = 0;
            this.Worker_Name_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(72, 259);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(41, 27);
            this.Save_Button.TabIndex = 5;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // View_All_Button
            // 
            this.View_All_Button.Location = new System.Drawing.Point(72, 259);
            this.View_All_Button.Name = "View_All_Button";
            this.View_All_Button.Size = new System.Drawing.Size(52, 27);
            this.View_All_Button.TabIndex = 8;
            this.View_All_Button.Text = "View All";
            this.View_All_Button.UseVisualStyleBackColor = true;
            this.View_All_Button.Click += new System.EventHandler(this.View_All_Button_Click);
            // 
            // Edit_Settings_Button
            // 
            this.Edit_Settings_Button.Location = new System.Drawing.Point(94, 215);
            this.Edit_Settings_Button.Name = "Edit_Settings_Button";
            this.Edit_Settings_Button.Size = new System.Drawing.Size(81, 27);
            this.Edit_Settings_Button.TabIndex = 4;
            this.Edit_Settings_Button.Text = "Edit";
            this.Edit_Settings_Button.UseVisualStyleBackColor = true;
            this.Edit_Settings_Button.Click += new System.EventHandler(this.Edit_Settings_Button_Click);
            // 
            // Miner_Settings_ListBox
            // 
            this.Miner_Settings_ListBox.FormattingEnabled = true;
            this.Miner_Settings_ListBox.Location = new System.Drawing.Point(14, 140);
            this.Miner_Settings_ListBox.Name = "Miner_Settings_ListBox";
            this.Miner_Settings_ListBox.Size = new System.Drawing.Size(161, 69);
            this.Miner_Settings_ListBox.TabIndex = 2;
            this.Miner_Settings_ListBox.SelectedIndexChanged += new System.EventHandler(this.Miner_Settings_ListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Worker";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Miner Settings";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Workers";
            // 
            // Add_Settings_Button
            // 
            this.Add_Settings_Button.Location = new System.Drawing.Point(14, 215);
            this.Add_Settings_Button.Name = "Add_Settings_Button";
            this.Add_Settings_Button.Size = new System.Drawing.Size(74, 27);
            this.Add_Settings_Button.TabIndex = 3;
            this.Add_Settings_Button.Text = "Add";
            this.Add_Settings_Button.UseVisualStyleBackColor = true;
            this.Add_Settings_Button.Click += new System.EventHandler(this.Add_Settings_Button_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Add_Settings_Button);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Miner_Settings_ListBox);
            this.panel1.Controls.Add(this.Edit_Settings_Button);
            this.panel1.Controls.Add(this.Save_Button);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Worker_Name_TextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Worker_Ip_TextBox);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 294);
            this.panel1.TabIndex = 22;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Auto_Start_CheckBox);
            this.panel2.Controls.Add(this.Delete_Button);
            this.panel2.Controls.Add(this.Network_Scan_Count_Label);
            this.panel2.Controls.Add(this.progressBar);
            this.panel2.Controls.Add(this.Scan_for_Workers_Button);
            this.panel2.Controls.Add(this.Workers_Panel);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.View_All_Button);
            this.panel2.Controls.Add(this.View_Button);
            this.panel2.Location = new System.Drawing.Point(221, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(633, 294);
            this.panel2.TabIndex = 23;
            // 
            // Delete_Button
            // 
            this.Delete_Button.Location = new System.Drawing.Point(211, 259);
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(47, 27);
            this.Delete_Button.TabIndex = 20;
            this.Delete_Button.Text = "Delete";
            this.Delete_Button.UseVisualStyleBackColor = true;
            this.Delete_Button.Click += new System.EventHandler(this.Delete_Button_Click);
            // 
            // Network_Scan_Count_Label
            // 
            this.Network_Scan_Count_Label.AutoSize = true;
            this.Network_Scan_Count_Label.Location = new System.Drawing.Point(487, 266);
            this.Network_Scan_Count_Label.Name = "Network_Scan_Count_Label";
            this.Network_Scan_Count_Label.Size = new System.Drawing.Size(36, 13);
            this.Network_Scan_Count_Label.TabIndex = 24;
            this.Network_Scan_Count_Label.Text = "0/255";
            this.Network_Scan_Count_Label.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(354, 261);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(123, 21);
            this.progressBar.TabIndex = 23;
            this.progressBar.Visible = false;
            // 
            // Scan_for_Workers_Button
            // 
            this.Scan_for_Workers_Button.Location = new System.Drawing.Point(527, 259);
            this.Scan_for_Workers_Button.Name = "Scan_for_Workers_Button";
            this.Scan_for_Workers_Button.Size = new System.Drawing.Size(101, 27);
            this.Scan_for_Workers_Button.TabIndex = 22;
            this.Scan_for_Workers_Button.Text = "Scan for Workers";
            this.Scan_for_Workers_Button.UseVisualStyleBackColor = true;
            this.Scan_for_Workers_Button.Click += new System.EventHandler(this.Scan_for_Workers_Button_Click);
            // 
            // Workers_Panel
            // 
            this.Workers_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.Workers_Panel.AutoScroll = true;
            this.Workers_Panel.Location = new System.Drawing.Point(3, 37);
            this.Workers_Panel.Name = "Workers_Panel";
            this.Workers_Panel.Size = new System.Drawing.Size(625, 205);
            this.Workers_Panel.TabIndex = 21;
            // 
            // Auto_Start_CheckBox
            // 
            this.Auto_Start_CheckBox.AutoSize = true;
            this.Auto_Start_CheckBox.Location = new System.Drawing.Point(479, 7);
            this.Auto_Start_CheckBox.Name = "Auto_Start_CheckBox";
            this.Auto_Start_CheckBox.Size = new System.Drawing.Size(149, 17);
            this.Auto_Start_CheckBox.TabIndex = 25;
            this.Auto_Start_CheckBox.Text = "Auto Start with WIndows?";
            this.Auto_Start_CheckBox.UseVisualStyleBackColor = true;
            this.Auto_Start_CheckBox.CheckedChanged += new System.EventHandler(this.Auto_Start_CheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 306);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mining Stats Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button View_Button;
        private System.Windows.Forms.TextBox Worker_Ip_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Worker_Name_TextBox;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button View_All_Button;
        private System.Windows.Forms.Button Edit_Settings_Button;
        private System.Windows.Forms.ListBox Miner_Settings_ListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Add_Settings_Button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Workers_Panel;
        private System.Windows.Forms.Button Scan_for_Workers_Button;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label Network_Scan_Count_Label;
        private System.Windows.Forms.Button Delete_Button;
        private System.Windows.Forms.CheckBox Auto_Start_CheckBox;
    }
}

