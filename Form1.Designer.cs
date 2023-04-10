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
            this.Start_Button = new System.Windows.Forms.Button();
            this.Worker_Ip_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Worker_Port_TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Algo2_Port_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Worker_Name_TextBox = new System.Windows.Forms.TextBox();
            this.Workers_ListBox = new System.Windows.Forms.ListBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Start_All_Button = new System.Windows.Forms.Button();
            this.Miner_ComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Location = new System.Drawing.Point(19, 156);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(72, 27);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "Start";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // Worker_Ip_TextBox
            // 
            this.Worker_Ip_TextBox.Location = new System.Drawing.Point(79, 42);
            this.Worker_Ip_TextBox.Name = "Worker_Ip_TextBox";
            this.Worker_Ip_TextBox.Size = new System.Drawing.Size(83, 20);
            this.Worker_Ip_TextBox.TabIndex = 3;
            this.Worker_Ip_TextBox.Text = "192.168.1.000";
            this.Worker_Ip_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Worker Ip:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Worker Port:";
            // 
            // Worker_Port_TextBox
            // 
            this.Worker_Port_TextBox.Location = new System.Drawing.Point(123, 70);
            this.Worker_Port_TextBox.Name = "Worker_Port_TextBox";
            this.Worker_Port_TextBox.Size = new System.Drawing.Size(39, 20);
            this.Worker_Port_TextBox.TabIndex = 5;
            this.Worker_Port_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Algo2 Port:";
            // 
            // Algo2_Port_TextBox
            // 
            this.Algo2_Port_TextBox.Location = new System.Drawing.Point(123, 96);
            this.Algo2_Port_TextBox.Name = "Algo2_Port_TextBox";
            this.Algo2_Port_TextBox.Size = new System.Drawing.Size(39, 20);
            this.Algo2_Port_TextBox.TabIndex = 7;
            this.Algo2_Port_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name:";
            // 
            // Worker_Name_TextBox
            // 
            this.Worker_Name_TextBox.Location = new System.Drawing.Point(79, 13);
            this.Worker_Name_TextBox.Name = "Worker_Name_TextBox";
            this.Worker_Name_TextBox.Size = new System.Drawing.Size(83, 20);
            this.Worker_Name_TextBox.TabIndex = 9;
            this.Worker_Name_TextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Workers_ListBox
            // 
            this.Workers_ListBox.FormattingEnabled = true;
            this.Workers_ListBox.Location = new System.Drawing.Point(227, 16);
            this.Workers_ListBox.Name = "Workers_ListBox";
            this.Workers_ListBox.Size = new System.Drawing.Size(101, 108);
            this.Workers_ListBox.TabIndex = 11;
            this.Workers_ListBox.SelectedIndexChanged += new System.EventHandler(this.Workers_ListBox_SelectedIndexChanged);
            this.Workers_ListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Workers_ListBox_KeyDown);
            // 
            // Save_Button
            // 
            this.Save_Button.Location = new System.Drawing.Point(113, 156);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(49, 27);
            this.Save_Button.TabIndex = 12;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Start_All_Button
            // 
            this.Start_All_Button.Location = new System.Drawing.Point(241, 156);
            this.Start_All_Button.Name = "Start_All_Button";
            this.Start_All_Button.Size = new System.Drawing.Size(72, 27);
            this.Start_All_Button.TabIndex = 13;
            this.Start_All_Button.Text = "Start All";
            this.Start_All_Button.UseVisualStyleBackColor = true;
            this.Start_All_Button.Click += new System.EventHandler(this.Start_All_Button_Click);
            // 
            // Miner_ComboBox
            // 
            this.Miner_ComboBox.FormattingEnabled = true;
            this.Miner_ComboBox.Items.AddRange(new object[] {
            "trm",
            "gminer"});
            this.Miner_ComboBox.Location = new System.Drawing.Point(79, 126);
            this.Miner_ComboBox.Name = "Miner_ComboBox";
            this.Miner_ComboBox.Size = new System.Drawing.Size(83, 21);
            this.Miner_ComboBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Miner:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 196);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Miner_ComboBox);
            this.Controls.Add(this.Start_All_Button);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Workers_ListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Worker_Name_TextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Algo2_Port_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Worker_Port_TextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Worker_Ip_TextBox);
            this.Controls.Add(this.Start_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mining Stats Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.TextBox Worker_Ip_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Worker_Port_TextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Algo2_Port_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Worker_Name_TextBox;
        private System.Windows.Forms.ListBox Workers_ListBox;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button Start_All_Button;
        private System.Windows.Forms.ComboBox Miner_ComboBox;
        private System.Windows.Forms.Label label5;
    }
}

