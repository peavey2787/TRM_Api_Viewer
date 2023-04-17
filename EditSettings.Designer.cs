namespace TRM_Api_Viewer
{
    partial class EditSettings
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
            this.Arguments_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Settings_Name_TextBox = new System.Windows.Forms.TextBox();
            this.Active_CheckBox = new System.Windows.Forms.CheckBox();
            this.Save_Button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Installed_Miners_ComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Version_TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Download_Button = new System.Windows.Forms.Button();
            this.URL_TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Coin1_ComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Coin2_ComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Coin3_ComboBox = new System.Windows.Forms.ComboBox();
            this.Algo1_TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Algo2_TextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Algo3_TextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Pool1_TextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.Pool2_TextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.Pool3_TextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Port3_TextBox = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Port2_TextBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Port1_TextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SSL1_CheckBox = new System.Windows.Forms.CheckBox();
            this.SSL2_CheckBox = new System.Windows.Forms.CheckBox();
            this.SSL3_CheckBox = new System.Windows.Forms.CheckBox();
            this.Wallet3_TextBox = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.Wallet2_TextBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Wallet1_TextBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.Generate_Button = new System.Windows.Forms.Button();
            this.Run_As_CheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Arguments_TextBox
            // 
            this.Arguments_TextBox.Location = new System.Drawing.Point(9, 315);
            this.Arguments_TextBox.Multiline = true;
            this.Arguments_TextBox.Name = "Arguments_TextBox";
            this.Arguments_TextBox.Size = new System.Drawing.Size(687, 87);
            this.Arguments_TextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(306, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = ".Bat File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // Settings_Name_TextBox
            // 
            this.Settings_Name_TextBox.Location = new System.Drawing.Point(52, 10);
            this.Settings_Name_TextBox.Name = "Settings_Name_TextBox";
            this.Settings_Name_TextBox.Size = new System.Drawing.Size(104, 20);
            this.Settings_Name_TextBox.TabIndex = 3;
            // 
            // Active_CheckBox
            // 
            this.Active_CheckBox.AutoSize = true;
            this.Active_CheckBox.Location = new System.Drawing.Point(177, 14);
            this.Active_CheckBox.Name = "Active_CheckBox";
            this.Active_CheckBox.Size = new System.Drawing.Size(62, 17);
            this.Active_CheckBox.TabIndex = 4;
            this.Active_CheckBox.Text = "Active?";
            this.Active_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Save_Button
            // 
            this.Save_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Save_Button.Location = new System.Drawing.Point(383, 408);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(67, 26);
            this.Save_Button.TabIndex = 5;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = true;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(244, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Installed_Miners_ComboBox
            // 
            this.Installed_Miners_ComboBox.FormattingEnabled = true;
            this.Installed_Miners_ComboBox.Location = new System.Drawing.Point(286, 9);
            this.Installed_Miners_ComboBox.Name = "Installed_Miners_ComboBox";
            this.Installed_Miners_ComboBox.Size = new System.Drawing.Size(99, 21);
            this.Installed_Miners_ComboBox.TabIndex = 7;
            this.Installed_Miners_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Installed_Miners_ComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Miner";
            // 
            // Version_TextBox
            // 
            this.Version_TextBox.Location = new System.Drawing.Point(446, 8);
            this.Version_TextBox.Name = "Version_TextBox";
            this.Version_TextBox.Size = new System.Drawing.Size(46, 20);
            this.Version_TextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(398, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Version";
            // 
            // Download_Button
            // 
            this.Download_Button.Location = new System.Drawing.Point(640, 5);
            this.Download_Button.Name = "Download_Button";
            this.Download_Button.Size = new System.Drawing.Size(68, 25);
            this.Download_Button.TabIndex = 11;
            this.Download_Button.Text = "Download";
            this.Download_Button.UseVisualStyleBackColor = true;
            this.Download_Button.Click += new System.EventHandler(this.Download_Button_Click);
            // 
            // URL_TextBox
            // 
            this.URL_TextBox.Location = new System.Drawing.Point(541, 8);
            this.URL_TextBox.Name = "URL_TextBox";
            this.URL_TextBox.Size = new System.Drawing.Size(90, 20);
            this.URL_TextBox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(506, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "URL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Coin";
            // 
            // Coin1_ComboBox
            // 
            this.Coin1_ComboBox.FormattingEnabled = true;
            this.Coin1_ComboBox.Location = new System.Drawing.Point(85, 114);
            this.Coin1_ComboBox.Name = "Coin1_ComboBox";
            this.Coin1_ComboBox.Size = new System.Drawing.Size(95, 21);
            this.Coin1_ComboBox.TabIndex = 14;
            this.Coin1_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Coin1_ComboBox_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(274, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Coin";
            // 
            // Coin2_ComboBox
            // 
            this.Coin2_ComboBox.FormattingEnabled = true;
            this.Coin2_ComboBox.Location = new System.Drawing.Point(326, 114);
            this.Coin2_ComboBox.Name = "Coin2_ComboBox";
            this.Coin2_ComboBox.Size = new System.Drawing.Size(93, 21);
            this.Coin2_ComboBox.TabIndex = 16;
            this.Coin2_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Coin2_ComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(529, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Coin";
            // 
            // Coin3_ComboBox
            // 
            this.Coin3_ComboBox.FormattingEnabled = true;
            this.Coin3_ComboBox.Location = new System.Drawing.Point(576, 114);
            this.Coin3_ComboBox.Name = "Coin3_ComboBox";
            this.Coin3_ComboBox.Size = new System.Drawing.Size(93, 21);
            this.Coin3_ComboBox.TabIndex = 18;
            this.Coin3_ComboBox.SelectedIndexChanged += new System.EventHandler(this.Coin3_ComboBox_SelectedIndexChanged);
            // 
            // Algo1_TextBox
            // 
            this.Algo1_TextBox.Location = new System.Drawing.Point(85, 154);
            this.Algo1_TextBox.Name = "Algo1_TextBox";
            this.Algo1_TextBox.Size = new System.Drawing.Size(95, 20);
            this.Algo1_TextBox.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(35, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Algo";
            // 
            // Algo2_TextBox
            // 
            this.Algo2_TextBox.Location = new System.Drawing.Point(326, 155);
            this.Algo2_TextBox.Name = "Algo2_TextBox";
            this.Algo2_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Algo2_TextBox.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(274, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Algo";
            // 
            // Algo3_TextBox
            // 
            this.Algo3_TextBox.Location = new System.Drawing.Point(576, 154);
            this.Algo3_TextBox.Name = "Algo3_TextBox";
            this.Algo3_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Algo3_TextBox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(524, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Algo";
            // 
            // Pool1_TextBox
            // 
            this.Pool1_TextBox.Location = new System.Drawing.Point(85, 193);
            this.Pool1_TextBox.Name = "Pool1_TextBox";
            this.Pool1_TextBox.Size = new System.Drawing.Size(95, 20);
            this.Pool1_TextBox.TabIndex = 27;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 197);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Pool";
            // 
            // Pool2_TextBox
            // 
            this.Pool2_TextBox.Location = new System.Drawing.Point(326, 194);
            this.Pool2_TextBox.Name = "Pool2_TextBox";
            this.Pool2_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Pool2_TextBox.TabIndex = 29;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(274, 198);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Pool";
            // 
            // Pool3_TextBox
            // 
            this.Pool3_TextBox.Location = new System.Drawing.Point(576, 194);
            this.Pool3_TextBox.Name = "Pool3_TextBox";
            this.Pool3_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Pool3_TextBox.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(524, 198);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Pool";
            // 
            // Port3_TextBox
            // 
            this.Port3_TextBox.Location = new System.Drawing.Point(576, 231);
            this.Port3_TextBox.Name = "Port3_TextBox";
            this.Port3_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Port3_TextBox.TabIndex = 37;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(524, 235);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Port";
            // 
            // Port2_TextBox
            // 
            this.Port2_TextBox.Location = new System.Drawing.Point(326, 231);
            this.Port2_TextBox.Name = "Port2_TextBox";
            this.Port2_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Port2_TextBox.TabIndex = 35;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(274, 235);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Port";
            // 
            // Port1_TextBox
            // 
            this.Port1_TextBox.Location = new System.Drawing.Point(85, 230);
            this.Port1_TextBox.Name = "Port1_TextBox";
            this.Port1_TextBox.Size = new System.Drawing.Size(95, 20);
            this.Port1_TextBox.TabIndex = 33;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(35, 235);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(26, 13);
            this.label17.TabIndex = 32;
            this.label17.Text = "Port";
            // 
            // SSL1_CheckBox
            // 
            this.SSL1_CheckBox.AutoSize = true;
            this.SSL1_CheckBox.Location = new System.Drawing.Point(85, 260);
            this.SSL1_CheckBox.Name = "SSL1_CheckBox";
            this.SSL1_CheckBox.Size = new System.Drawing.Size(52, 17);
            this.SSL1_CheckBox.TabIndex = 38;
            this.SSL1_CheckBox.Text = "SSL?";
            this.SSL1_CheckBox.UseVisualStyleBackColor = true;
            // 
            // SSL2_CheckBox
            // 
            this.SSL2_CheckBox.AutoSize = true;
            this.SSL2_CheckBox.Location = new System.Drawing.Point(339, 260);
            this.SSL2_CheckBox.Name = "SSL2_CheckBox";
            this.SSL2_CheckBox.Size = new System.Drawing.Size(52, 17);
            this.SSL2_CheckBox.TabIndex = 39;
            this.SSL2_CheckBox.Text = "SSL?";
            this.SSL2_CheckBox.UseVisualStyleBackColor = true;
            // 
            // SSL3_CheckBox
            // 
            this.SSL3_CheckBox.AutoSize = true;
            this.SSL3_CheckBox.Location = new System.Drawing.Point(588, 260);
            this.SSL3_CheckBox.Name = "SSL3_CheckBox";
            this.SSL3_CheckBox.Size = new System.Drawing.Size(52, 17);
            this.SSL3_CheckBox.TabIndex = 40;
            this.SSL3_CheckBox.Text = "SSL?";
            this.SSL3_CheckBox.UseVisualStyleBackColor = true;
            // 
            // Wallet3_TextBox
            // 
            this.Wallet3_TextBox.Location = new System.Drawing.Point(576, 72);
            this.Wallet3_TextBox.Name = "Wallet3_TextBox";
            this.Wallet3_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Wallet3_TextBox.TabIndex = 46;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(524, 76);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(37, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Wallet";
            // 
            // Wallet2_TextBox
            // 
            this.Wallet2_TextBox.Location = new System.Drawing.Point(326, 73);
            this.Wallet2_TextBox.Name = "Wallet2_TextBox";
            this.Wallet2_TextBox.Size = new System.Drawing.Size(93, 20);
            this.Wallet2_TextBox.TabIndex = 44;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(274, 77);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 13);
            this.label19.TabIndex = 43;
            this.label19.Text = "Wallet";
            // 
            // Wallet1_TextBox
            // 
            this.Wallet1_TextBox.Location = new System.Drawing.Point(85, 72);
            this.Wallet1_TextBox.Name = "Wallet1_TextBox";
            this.Wallet1_TextBox.Size = new System.Drawing.Size(95, 20);
            this.Wallet1_TextBox.TabIndex = 42;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(35, 76);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 13);
            this.label20.TabIndex = 41;
            this.label20.Text = "Wallet";
            // 
            // Generate_Button
            // 
            this.Generate_Button.Location = new System.Drawing.Point(231, 286);
            this.Generate_Button.Name = "Generate_Button";
            this.Generate_Button.Size = new System.Drawing.Size(69, 26);
            this.Generate_Button.TabIndex = 47;
            this.Generate_Button.Text = "Generate";
            this.Generate_Button.UseVisualStyleBackColor = true;
            this.Generate_Button.Click += new System.EventHandler(this.Generate_Button_Click);
            // 
            // Run_As_CheckBox
            // 
            this.Run_As_CheckBox.AutoSize = true;
            this.Run_As_CheckBox.Location = new System.Drawing.Point(286, 36);
            this.Run_As_CheckBox.Name = "Run_As_CheckBox";
            this.Run_As_CheckBox.Size = new System.Drawing.Size(99, 17);
            this.Run_As_CheckBox.TabIndex = 48;
            this.Run_As_CheckBox.Text = "Run As Admin?";
            this.Run_As_CheckBox.UseVisualStyleBackColor = true;
            // 
            // EditSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 441);
            this.Controls.Add(this.Run_As_CheckBox);
            this.Controls.Add(this.Generate_Button);
            this.Controls.Add(this.Wallet3_TextBox);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.Wallet2_TextBox);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.Wallet1_TextBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.SSL3_CheckBox);
            this.Controls.Add(this.SSL2_CheckBox);
            this.Controls.Add(this.SSL1_CheckBox);
            this.Controls.Add(this.Port3_TextBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Port2_TextBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Port1_TextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Pool3_TextBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Pool2_TextBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Pool1_TextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Algo3_TextBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Algo2_TextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Algo1_TextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Coin3_ComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Coin2_ComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Coin1_ComboBox);
            this.Controls.Add(this.URL_TextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Download_Button);
            this.Controls.Add(this.Version_TextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Installed_Miners_ComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Active_CheckBox);
            this.Controls.Add(this.Settings_Name_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Arguments_TextBox);
            this.Name = "EditSettings";
            this.Text = "Edit Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditSettings_FormClosing);
            this.Load += new System.EventHandler(this.EditSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Arguments_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Settings_Name_TextBox;
        private System.Windows.Forms.CheckBox Active_CheckBox;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox Installed_Miners_ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Version_TextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Download_Button;
        private System.Windows.Forms.TextBox URL_TextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Coin1_ComboBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Coin2_ComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Coin3_ComboBox;
        private System.Windows.Forms.TextBox Algo1_TextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Algo2_TextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox Algo3_TextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Pool1_TextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Pool2_TextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox Pool3_TextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox Port3_TextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox Port2_TextBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox Port1_TextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox SSL1_CheckBox;
        private System.Windows.Forms.CheckBox SSL2_CheckBox;
        private System.Windows.Forms.CheckBox SSL3_CheckBox;
        private System.Windows.Forms.TextBox Wallet3_TextBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox Wallet2_TextBox;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox Wallet1_TextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button Generate_Button;
        private System.Windows.Forms.CheckBox Run_As_CheckBox;
    }
}