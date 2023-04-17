using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Control = System.Windows.Forms.Control;
using Label = System.Windows.Forms.Label;
using Panel = System.Windows.Forms.Panel;

namespace TRM_Api_Viewer
{
    public partial class WorkerSummary : System.Windows.Forms.UserControl
    {
        public Panel WorkerPanel { get; set; }
        public WorkerSummary()
        {
            InitializeComponent();
            WorkerPanel = Worker_Summary_Panel;
        }
        private void Worker_Summary_Panel_Paint(object sender, PaintEventArgs e)
        {
            if(WorkerPanel.BorderStyle == BorderStyle.FixedSingle)
                ControlPaint.DrawBorder(e.Graphics, WorkerPanel.ClientRectangle, Color.Blue, ButtonBorderStyle.Solid);
        }
    }
}
