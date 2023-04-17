namespace TRM_Api_Viewer
{
    partial class View_Miner
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.Reconnect_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox.ForeColor = System.Drawing.SystemColors.Control;
            this.textBox.Location = new System.Drawing.Point(1, 1);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.Size = new System.Drawing.Size(797, 411);
            this.textBox.TabIndex = 0;
            // 
            // Reconnect_Button
            // 
            this.Reconnect_Button.Location = new System.Drawing.Point(4, 418);
            this.Reconnect_Button.Name = "Reconnect_Button";
            this.Reconnect_Button.Size = new System.Drawing.Size(78, 25);
            this.Reconnect_Button.TabIndex = 1;
            this.Reconnect_Button.Text = "ReConnect";
            this.Reconnect_Button.UseVisualStyleBackColor = true;
            this.Reconnect_Button.Click += new System.EventHandler(this.Reconnect_Button_Click);
            // 
            // View_Miner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Reconnect_Button);
            this.Controls.Add(this.textBox);
            this.Name = "View_Miner";
            this.Text = "View_Miner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.View_Miner_FormClosing);
            this.Load += new System.EventHandler(this.View_Miner_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button Reconnect_Button;
    }
}