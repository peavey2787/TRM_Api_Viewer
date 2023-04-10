using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace TRM_Api_Viewer
{
    public partial class Form1 : Form
    {
        List<Worker> workerList = new List<Worker>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var json = AppSettings.LoadAndDecrypt("WorkerList");
            workerList = JsonConvert.DeserializeObject<List<Worker>>(json);
            if(workerList != null && workerList.Count > 0)
                foreach(var worker in workerList)
                    Workers_ListBox.Items.Add(worker.Name);   
            else
                workerList = new List<Worker>();
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            if (!System.Net.IPAddress.TryParse(Worker_Ip_TextBox.Text, out var ip))
            {
                MessageBox.Show("Worker IP must be a valid ip address");
                return;
            }
            if (!int.TryParse(Worker_Port_TextBox.Text, out var port))
            {
                MessageBox.Show("Worker Port must be an integer");
                return;
            }
            if (!int.TryParse(Algo2_Port_TextBox.Text, out var port2))
            {
                if (!String.IsNullOrEmpty(Algo2_Port_TextBox.Text))
                {
                    MessageBox.Show("Algo2 Port must be an integer");
                    return;
                }
            }

            // Create a new form to display the mining stats
            var worker = new Worker();
            worker.Worker_IP = ip.ToString();
            worker.Worker_Port = port;
            worker.Algo2_Port = port2;
            worker.Miner = Miner_ComboBox.Text;

            Form form = new Gpus_Form(worker);
            form.Show();
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            var worker = GetWorkerFromUserInput();
            workerList.Add(worker);
            Workers_ListBox.Items.Add(worker.Name);

            string json = JsonConvert.SerializeObject(workerList);
            AppSettings.EncryptAndSave("WorkerList", json);
        }
        private Worker GetWorkerFromUserInput()
        {
            Worker worker = new Worker();

            if(!String.IsNullOrWhiteSpace(Worker_Name_TextBox.Text))
            {
                worker.Name = Worker_Name_TextBox.Text;
            }
            else
            {
                MessageBox.Show("Worker must have a name");
                return null;
            }
            if (IPAddress.TryParse(Worker_Ip_TextBox.Text, out var ip))
            {
                worker.Worker_IP = Worker_Ip_TextBox.Text;
            }
            else
            {
                MessageBox.Show("Worker IP must be a valid IP address");
                return null;
            }
            if (int.TryParse(Worker_Port_TextBox.Text, out var port))
            {
                worker.Worker_Port = port;
            }
            else
            {
                MessageBox.Show("Worker Port must be an integer");
                return null;
            }
            if (int.TryParse(Algo2_Port_TextBox.Text, out var algo2))
            {
                worker.Algo2_Port = algo2;
            }
            else
            {
                MessageBox.Show("Algo2 Port must be an integer");
                return null;
            }
            if (!String.IsNullOrEmpty(Miner_ComboBox.Text))
            {
                worker.Miner = Miner_ComboBox.Text;
            }
            else
            {
                MessageBox.Show("Algo2 Port must be an integer");
                return null;
            }
            return worker;
        }

        private void Workers_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Workers_ListBox.SelectedItems.Count == 0) { return; }

            var workerName = Workers_ListBox.SelectedItems[0].ToString();

            var worker = workerList.Find(w => w.Name.Equals(workerName));
            Worker_Name_TextBox.Text = workerName;
            Worker_Ip_TextBox.Text = worker.Worker_IP;
            Worker_Port_TextBox.Text = worker.Worker_Port.ToString();
            Algo2_Port_TextBox.Text = worker.Algo2_Port.ToString();
            Miner_ComboBox.Text = worker.Miner;
        }
        private void Workers_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (Workers_ListBox.SelectedIndex != -1)
                {
                    var workerName = Workers_ListBox.SelectedItem.ToString();
                    Workers_ListBox.Items.RemoveAt(Workers_ListBox.SelectedIndex);

                    var worker = workerList.Find(w => w.Name.Equals(workerName));
                    workerList.Remove(worker);

                    string json = JsonConvert.SerializeObject(workerList);
                    AppSettings.EncryptAndSave("WorkerList", json);
                }
            }
        }

        private void Start_All_Button_Click(object sender, EventArgs e)
        {
            foreach(var worker in workerList)
            {
                Form form = new Gpus_Form(worker);
                form.Show();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
