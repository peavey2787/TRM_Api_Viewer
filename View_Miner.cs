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
        Worker Worker = new Worker();
        int port = 34218;
        bool formClosing = false;
        Task _getMinerOutput;
        public View_Miner(Worker worker)
        {
            InitializeComponent();
            this.Worker = worker;
            this.Text = $"{worker.Name}-{worker.Get_Active_Setting().Display_Name}";
        }
        private void View_Miner_Load(object sender, EventArgs e)
        {
            GetMinerOutput();
        }
        private void View_Miner_FormClosing(object sender, FormClosingEventArgs e)
        {
            formClosing = true;
        }
        private void GetMinerOutput()
        {
            _getMinerOutput = Task.Run(() =>
            {
                int retries = 0;
                bool connected = false;

                while (!connected && retries < 3) // Retry up to 3 times
                {
                    try
                    {
                        using (TcpClient client = new TcpClient(Worker.IP, port))
                        {
                            using (NetworkStream stream = client.GetStream())
                            {
                                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                                {
                                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                                    {
                                        var json = JsonConvert.SerializeObject(Worker.Get_Active_Setting());
                                        writer.WriteLineAsync("view/" + json);
                                        writer.Flush();
                                        connected = true;

                                        // Keep reading from the stream while the connection is active
                                        while (client.Connected && !formClosing)
                                        {
                                            string response = reader.ReadLine();

                                            bool addResp = true;
                                            string jsonTag = "json:";
                                            if(response.StartsWith(jsonTag))
                                            {
                                                addResp = false;
                                                string statsJson = response.Substring(jsonTag.Length);
                                                var minerStats = JsonConvert.DeserializeObject<dynamic>(statsJson);
                                                var uptime = minerStats.Uptime;
                                                var elec = minerStats.Electricity;
                                                var shares = minerStats.Shares;
                                                var shares2 = minerStats.Shares2;
                                                var balance = minerStats.Balance;
                                                var eff = minerStats.Efficiency;
                                                var eff2 = minerStats.Efficiency2;
                                                var poolHash = minerStats.PoolHashrate;
                                                var poolHash2 = minerStats.PoolHashrate2;
                                                if (minerStats.Gpus != null)
                                                {
                                                    foreach (var gpu in minerStats.Gpus)
                                                    {
                                                        var id = gpu.ID;
                                                        var name = gpu.GPU;
                                                    }
                                                }
                                                // minerStats.Uptime .Electricity .Shares .Shares2 .Balance .Efficiency .Efficiency2
                                                // .PoolHashrate .PoolHashrate2

                                            }

                                            // Append the response to the text box
                                            if (addResp && textBox.InvokeRequired)
                                            {
                                                textBox.Invoke(new Action(() =>
                                                {
                                                    textBox.AppendText(response + Environment.NewLine);
                                                }));
                                            }
                                            else if (addResp)
                                            {
                                                textBox.AppendText(response + Environment.NewLine);
                                            }
                                        }
                                        // Close the connection
                                        writer.Flush();
                                        stream.Close();
                                        reader.Close();
                                        writer.Close();
                                        client.Close();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {                        
                        // Handle the exception and retry the connection
                        textBox.AppendText($"\nError connecting to the server: {ex.Message}");
                        Thread.Sleep(1000); // Wait for 1 second before retrying
                        retries++;
                    }
                }

                if (!connected)
                {
                    textBox.AppendText("Failed to connect to the server after 3 retries.");
                }
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
