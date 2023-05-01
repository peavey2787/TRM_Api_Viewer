using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRM_Api_Viewer.Properties;

namespace TRM_Api_Viewer
{
    public partial class View_Miner : Form
    {
        ClientManager clientManager;
        Worker Worker;
        Task _getMinerOutput;
        public View_Miner(Worker worker, ref ClientManager clientManager)
        {
            InitializeComponent();
            this.Worker = worker;
            this.Text = $"{worker.Name}-{worker.Get_Active_Setting().Display_Name}";
            this.clientManager = clientManager;
        }
        private void View_Miner_Load(object sender, EventArgs e)
        {
            GetMinerOutput();
        }
        private void View_Miner_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientManager.Stop_Viewing_Miner(Worker.IP, Worker.Get_Active_Setting());
        }
        private void GetMinerOutput()
        {
            _getMinerOutput = Task.Run(() =>
            {
                clientManager.View_Miner(Worker.IP, Worker.Get_Active_Setting(), ref textBox);
            });
        }
        private void Reconnect_Button_Click(object sender, EventArgs e)
        {
            if (_getMinerOutput == null || _getMinerOutput.Status != TaskStatus.Running)
            {
                GetMinerOutput();
            }
        }
    }
}
