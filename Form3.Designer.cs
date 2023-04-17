using System;

namespace TRM_Api_Viewer
{
    partial class Gpus_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gpus_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.Count_Down_Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Update_Miner_Button = new System.Windows.Forms.Button();
            this.Stop_Miner_Button = new System.Windows.Forms.Button();
            this.Start_Miner_Button = new System.Windows.Forms.Button();
            this.Restart_Miner_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Total_Mhs = new System.Windows.Forms.Label();
            this.Total_Mhs2 = new System.Windows.Forms.Label();
            this.Total_Power = new System.Windows.Forms.Label();
            this.Totals_Panel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.Total_Mhs3 = new System.Windows.Forms.Label();
            this.Worker_Name = new System.Windows.Forms.Label();
            this.Dock_Button = new System.Windows.Forms.Button();
            this.Stats_Panel = new System.Windows.Forms.Panel();
            this.View_Miner_Button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.Totals_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Refresh in ";
            // 
            // Count_Down_Label
            // 
            this.Count_Down_Label.AutoSize = true;
            this.Count_Down_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Count_Down_Label.Location = new System.Drawing.Point(116, 6);
            this.Count_Down_Label.Name = "Count_Down_Label";
            this.Count_Down_Label.Size = new System.Drawing.Size(24, 18);
            this.Count_Down_Label.TabIndex = 1;
            this.Count_Down_Label.Text = "30";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.View_Miner_Button);
            this.panel1.Controls.Add(this.Update_Miner_Button);
            this.panel1.Controls.Add(this.Stop_Miner_Button);
            this.panel1.Controls.Add(this.Start_Miner_Button);
            this.panel1.Controls.Add(this.Restart_Miner_Button);
            this.panel1.Controls.Add(this.Count_Down_Label);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 455);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1091, 28);
            this.panel1.TabIndex = 2;
            // 
            // Update_Miner_Button
            // 
            this.Update_Miner_Button.Location = new System.Drawing.Point(506, 0);
            this.Update_Miner_Button.Name = "Update_Miner_Button";
            this.Update_Miner_Button.Size = new System.Drawing.Size(79, 28);
            this.Update_Miner_Button.TabIndex = 5;
            this.Update_Miner_Button.Text = "Update Miner";
            this.Update_Miner_Button.UseVisualStyleBackColor = true;
            this.Update_Miner_Button.Click += new System.EventHandler(this.Update_Miner_Button_Click);
            // 
            // Stop_Miner_Button
            // 
            this.Stop_Miner_Button.Location = new System.Drawing.Point(272, 0);
            this.Stop_Miner_Button.Name = "Stop_Miner_Button";
            this.Stop_Miner_Button.Size = new System.Drawing.Size(69, 28);
            this.Stop_Miner_Button.TabIndex = 4;
            this.Stop_Miner_Button.Text = "Stop Miner";
            this.Stop_Miner_Button.UseVisualStyleBackColor = true;
            this.Stop_Miner_Button.Click += new System.EventHandler(this.Stop_Miner_Button_Click);
            // 
            // Start_Miner_Button
            // 
            this.Start_Miner_Button.Location = new System.Drawing.Point(177, 0);
            this.Start_Miner_Button.Name = "Start_Miner_Button";
            this.Start_Miner_Button.Size = new System.Drawing.Size(70, 28);
            this.Start_Miner_Button.TabIndex = 3;
            this.Start_Miner_Button.Text = "Start Miner";
            this.Start_Miner_Button.UseVisualStyleBackColor = true;
            this.Start_Miner_Button.Click += new System.EventHandler(this.Start_Miner_Button_Click);
            // 
            // Restart_Miner_Button
            // 
            this.Restart_Miner_Button.Location = new System.Drawing.Point(380, 0);
            this.Restart_Miner_Button.Name = "Restart_Miner_Button";
            this.Restart_Miner_Button.Size = new System.Drawing.Size(79, 28);
            this.Restart_Miner_Button.TabIndex = 2;
            this.Restart_Miner_Button.Text = "Restart Miner";
            this.Restart_Miner_Button.UseVisualStyleBackColor = true;
            this.Restart_Miner_Button.Click += new System.EventHandler(this.Restart_Miner_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(246, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total MH/s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(424, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "2nd Algo MH/s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(848, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Power";
            // 
            // Total_Mhs
            // 
            this.Total_Mhs.AutoSize = true;
            this.Total_Mhs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_Mhs.Location = new System.Drawing.Point(349, 11);
            this.Total_Mhs.Name = "Total_Mhs";
            this.Total_Mhs.Size = new System.Drawing.Size(49, 20);
            this.Total_Mhs.TabIndex = 8;
            this.Total_Mhs.Text = "1000";
            // 
            // Total_Mhs2
            // 
            this.Total_Mhs2.AutoSize = true;
            this.Total_Mhs2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_Mhs2.Location = new System.Drawing.Point(556, 11);
            this.Total_Mhs2.Name = "Total_Mhs2";
            this.Total_Mhs2.Size = new System.Drawing.Size(49, 20);
            this.Total_Mhs2.TabIndex = 9;
            this.Total_Mhs2.Text = "1000";
            // 
            // Total_Power
            // 
            this.Total_Power.AutoSize = true;
            this.Total_Power.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_Power.Location = new System.Drawing.Point(912, 11);
            this.Total_Power.Name = "Total_Power";
            this.Total_Power.Size = new System.Drawing.Size(49, 20);
            this.Total_Power.TabIndex = 10;
            this.Total_Power.Text = "1000";
            // 
            // Totals_Panel
            // 
            this.Totals_Panel.Controls.Add(this.label5);
            this.Totals_Panel.Controls.Add(this.Total_Mhs3);
            this.Totals_Panel.Controls.Add(this.Worker_Name);
            this.Totals_Panel.Controls.Add(this.Dock_Button);
            this.Totals_Panel.Controls.Add(this.label2);
            this.Totals_Panel.Controls.Add(this.Total_Power);
            this.Totals_Panel.Controls.Add(this.label3);
            this.Totals_Panel.Controls.Add(this.Total_Mhs2);
            this.Totals_Panel.Controls.Add(this.label4);
            this.Totals_Panel.Controls.Add(this.Total_Mhs);
            this.Totals_Panel.Location = new System.Drawing.Point(16, 3);
            this.Totals_Panel.Name = "Totals_Panel";
            this.Totals_Panel.Size = new System.Drawing.Size(1091, 42);
            this.Totals_Panel.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(638, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "3rd Algo MH/s";
            // 
            // Total_Mhs3
            // 
            this.Total_Mhs3.AutoSize = true;
            this.Total_Mhs3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_Mhs3.Location = new System.Drawing.Point(770, 11);
            this.Total_Mhs3.Name = "Total_Mhs3";
            this.Total_Mhs3.Size = new System.Drawing.Size(49, 20);
            this.Total_Mhs3.TabIndex = 16;
            this.Total_Mhs3.Text = "1000";
            // 
            // Worker_Name
            // 
            this.Worker_Name.AutoSize = true;
            this.Worker_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Worker_Name.Location = new System.Drawing.Point(3, 11);
            this.Worker_Name.Name = "Worker_Name";
            this.Worker_Name.Size = new System.Drawing.Size(117, 20);
            this.Worker_Name.TabIndex = 14;
            this.Worker_Name.Text = "Worker Name";
            // 
            // Dock_Button
            // 
            this.Dock_Button.Location = new System.Drawing.Point(1007, 9);
            this.Dock_Button.Name = "Dock_Button";
            this.Dock_Button.Size = new System.Drawing.Size(81, 26);
            this.Dock_Button.TabIndex = 13;
            this.Dock_Button.Text = "Dock";
            this.Dock_Button.UseVisualStyleBackColor = true;
            this.Dock_Button.Click += new System.EventHandler(this.Dock_Button_Click);
            // 
            // Stats_Panel
            // 
            this.Stats_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stats_Panel.AutoScroll = true;
            this.Stats_Panel.Location = new System.Drawing.Point(16, 44);
            this.Stats_Panel.Name = "Stats_Panel";
            this.Stats_Panel.Size = new System.Drawing.Size(1091, 405);
            this.Stats_Panel.TabIndex = 14;
            // 
            // View_Miner_Button
            // 
            this.View_Miner_Button.Location = new System.Drawing.Point(628, 0);
            this.View_Miner_Button.Name = "View_Miner_Button";
            this.View_Miner_Button.Size = new System.Drawing.Size(68, 28);
            this.View_Miner_Button.TabIndex = 6;
            this.View_Miner_Button.Text = "View Miner";
            this.View_Miner_Button.UseVisualStyleBackColor = true;
            this.View_Miner_Button.Click += new System.EventHandler(this.View_Miner_Button_Click);
            // 
            // Gpus_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 483);
            this.Controls.Add(this.Stats_Panel);
            this.Controls.Add(this.Totals_Panel);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Gpus_Form";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Gpus_Form_FormClosing);
            this.Load += new System.EventHandler(this.Gpus_Form_Load);
            this.LocationChanged += new System.EventHandler(this.Gpus_Form_LocationChanged);
            this.Resize += new System.EventHandler(this.Gpus_Form_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Totals_Panel.ResumeLayout(false);
            this.Totals_Panel.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Count_Down_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Restart_Miner_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Total_Mhs;
        private System.Windows.Forms.Label Total_Mhs2;
        private System.Windows.Forms.Panel Totals_Panel;
        private System.Windows.Forms.Panel Stats_Panel;
        private System.Windows.Forms.Button Dock_Button;
        private System.Windows.Forms.Label Worker_Name;
        private System.Windows.Forms.Label Total_Power;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Total_Mhs3;
        private System.Windows.Forms.Button Stop_Miner_Button;
        private System.Windows.Forms.Button Start_Miner_Button;
        private System.Windows.Forms.Button Update_Miner_Button;
        private System.Windows.Forms.Button View_Miner_Button;
    }
}