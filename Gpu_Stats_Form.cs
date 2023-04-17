using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRM_Api_Viewer
{
    public partial class Gpu_Stats_Form : UserControl
    {
        public Panel GpuPanel { get; set; } 
        public Gpu_Stats_Form()
        {
            InitializeComponent();
            GpuPanel = Gpu_Panel;
        }
    }
}
