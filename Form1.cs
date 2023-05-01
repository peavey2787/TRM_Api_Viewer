using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TRM_Api_Viewer.Properties;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Control = System.Windows.Forms.Control;
using Keys = System.Windows.Forms.Keys;
using Label = System.Windows.Forms.Label;
using Panel = System.Windows.Forms.Panel;
using TextBox = System.Windows.Forms.TextBox;

namespace TRM_Api_Viewer
{
    public partial class Form1 : Form
    {
        List<Worker> worker_list = new List<Worker>();
        ClientManager client_manager = new ClientManager();
        private int companionPort = 34218;
        private int worker_yPos = 0;
        private int worker_margin = 10;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var json = AppSettings.LoadAndDecrypt("WorkerList");
            worker_list = JsonConvert.DeserializeObject<List<Worker>>(json);
            if (worker_list != null && worker_list.Count > 0)
            {
                foreach (var worker in worker_list)
                    Create_Worker_Form(worker);

                // Connect to all known workers
                Task.Run(() =>
                {
                    foreach (var worker in worker_list)
                    {
                        Add_Worker_Panel_to_Display(Convert_Worker_to_Panel(worker));

                        worker.Online = client_manager.Add_Client(worker.IP, companionPort);
                        if (worker.Online)
                        {
                            MinerStats stats = client_manager.Get_Stats(worker.IP, worker.Get_Active_Setting());
                            UpdateMinerStats(stats, false);
                        }
                    }

                    // If only one worker, select it
                    if (worker_list.Count == 1)
                    {
                        Panel worker_panel = (Panel)Workers_Panel.Controls[0];
                        worker_panel.SetProperty("BorderStyle", BorderStyle.FixedSingle);
                        Update_Worker_Settings(worker_list[0]);
                    }
                });                
            }
            else
                worker_list = new List<Worker>();
        }
        // Start with Windows
        public static void Set_Auto_Start_Registry(bool enable)
        {
            string name = Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string path = Assembly.GetEntryAssembly().Location;

            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enable)
            {
                key.SetValue(name, path);
            }
            else
            {
                key.DeleteValue(name, false);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client_manager.Shutdown_Clients();
        }


        private bool Show_Worker_Form(string formName)
        {
            // Iterate through the open forms to find a form with the given name
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form.Text == formName)
                {
                    // If a form with the given name is found, show it and return
                    form.Show();
                    return true;
                }
            }
            return false;
        }
        private void Create_Worker_Form(Worker worker)
        {
            Form form = new Gpus_Form(worker, client_manager);
            form.Owner = this;
            form.Show();
            form.Hide();
        }
        private void Create_Totals_Form()
        {
            Form form = new Gpus_Form(worker_list, client_manager);
            form.Owner = this;
            form.Show();
            form.Hide();
        }



        // User Actions
        private void View_Button_Click(object sender, EventArgs e)
        {
            var worker = Get_Selected_Worker();
            if (worker == null)
            {
                MessageBox.Show("Please select a worker to view");
                return;
            }

            if (worker.Name != "Total")
            {
                if (!Show_Worker_Form("GPU Stats - " + worker.Name + " - " + worker.IP))
                    Create_Worker_Form(worker);
            }
            else
                if (!Show_Worker_Form("GPU Stats - Totals"))
                    Create_Totals_Form();            
        }
        private void View_All_Button_Click(object sender, EventArgs e)
        {
            foreach (var worker in worker_list)
            {
                Form form = new Gpus_Form(worker, client_manager);
                form.Show();
            }
        }
        private void Delete_Button_Click(object sender, EventArgs e)
        {
            var worker = Get_Selected_Worker();
            if (worker == null)
                MessageBox.Show("Please select a worker to delete");

            worker_list.Remove(worker);
            var worker_panel = Workers_Panel.Controls.Find(worker.Name, true).First();
            Workers_Panel.Controls.Remove(worker_panel);

            string json = JsonConvert.SerializeObject(worker_list);
            AppSettings.EncryptAndSave("WorkerList", json);
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            var worker = Get_Worker_From_User_Input();
            if (worker == null)
                return;

            string json = JsonConvert.SerializeObject(worker_list);
            AppSettings.EncryptAndSave("WorkerList", json);
        }
        private void Scan_for_Workers_Button_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                progressBar.SetProperty("Visible", true);
                progressBar.SetProperty("Style", ProgressBarStyle.Marquee);

                await Scan_Network_for_All_Workers(true);

                progressBar.SetProperty("Visible", false);
                progressBar.SetProperty("Style", ProgressBarStyle.Blocks);
            });
        }
        private void Add_Settings_Button_Click(object sender, EventArgs e)
        {
            var worker = Get_Selected_Worker();
            if (worker == null)
            {
                MessageBox.Show("Please select a worker to add new settings to");
                return;
            }

            Task.Run(() =>
            {
                var miner_settings = new MinerSettings();
                miner_settings.Worker_Name = worker.Name;
                try
                {
                    var settings_form = new EditSettings(ref client_manager, miner_settings, worker);
                    var result = settings_form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var new_settings = settings_form.miner_settings;
                        worker.Add_Setting(new_settings);                                                

                        Add_Miner_Setting_To_ListBox(new_settings);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }

            });
        }
        private void Edit_Settings_Button_Click(object sender, EventArgs e)
        {
            if (Miner_Settings_ListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a setting to edit");
                return;
            }

            var worker = Get_Selected_Worker();
            var miner_settings = worker.Get_Setting(Miner_Settings_ListBox.SelectedItems[0].ToString());
            if(miner_settings == null)
            {
                MessageBox.Show("Miner settings cannot be empty");
            }
            var settings_form = new EditSettings(ref client_manager, miner_settings, worker);

            try
            {
                var result = settings_form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    worker.Add_Setting(settings_form.miner_settings);
                }
            }
            catch(Exception ex) { MessageBox.Show("Error adding settings to worker. " + ex.Message); }

            string json = JsonConvert.SerializeObject(worker_list);
            AppSettings.EncryptAndSave("WorkerList", json);
        }
        private void Miner_Settings_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Miner_Settings_ListBox.Items.Count == 0) return;

        }
        private void Miner_Settings_ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && Miner_Settings_ListBox.SelectedItems.Count > 0)
            {
                var worker = Get_Selected_Worker();
                List<MinerSettings> settings = worker.Get_Miner_Settings();
                if (settings != null && settings.Count > 0)
                {
                    var existing = settings.Find(s => s.Display_Name.Equals(Miner_Settings_ListBox.SelectedItem));
                    if (existing != null)
                    {
                        client_manager.Remove_Miner_Setting(worker.IP, existing);
                        Miner_Settings_ListBox.Items.Remove(Miner_Settings_ListBox.SelectedItem);
                    }
                }
            }
        }
        private void Auto_Start_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Set_Auto_Start_Registry(Auto_Start_CheckBox.Checked);
        }



        // Get/Set Worker GUI
        private Worker Get_Worker_From_User_Input()
        {
            Worker worker = new Worker();

            if (!String.IsNullOrWhiteSpace(Worker_Name_TextBox.Text))
            {
                foreach (char c in Worker_Name_TextBox.Text)
                {
                    if (!char.IsLetterOrDigit(c))
                    {
                        MessageBox.Show("Worker name can only contain letters or numbers");
                        return null;
                    }
                }
                worker.Name = Worker_Name_TextBox.Text;
            }
            else
            {
                MessageBox.Show("Worker must have a name");
                return null;
            }
            if (IPAddress.TryParse(Worker_Ip_TextBox.Text, out var ip))
            {
                worker.IP = Worker_Ip_TextBox.Text;
            }
            else
            {
                MessageBox.Show("Worker IP must be a valid IP address");
                return null;
            }
            var minerSettings = worker.Get_Miner_Settings();
            if (minerSettings == null || Miner_Settings_ListBox.Items.Count == 0)
            {
                MessageBox.Show("Worker must have at least 1 miner setting");
                return null;
            }

            // Get corresponding worker from worker list
            var existing = worker_list.Find(w => w.IP.Equals(worker.IP));
            if (existing != null)
            {
                existing.Name = worker.Name;
                return existing;
            }
            
            return worker;
        }
        private void Add_Worker_Panel_to_Display(Panel panel)
        {
            if (Workers_Panel.Controls.Count > 0)
            {
                var existing_panel = Workers_Panel.Controls.Find(panel.Name, true);
                if (existing_panel != null && existing_panel.Count() > 0)
                {
                    Workers_Panel.Controls.Remove(existing_panel[0]);
                    worker_yPos += existing_panel[0].Height + worker_margin;
                    existing_panel[0].Location = new Point(0, worker_yPos);
                }
            }
            if(Workers_Panel.InvokeRequired)
            {
                Workers_Panel.BeginInvoke(new Action(() =>
                {
                    Workers_Panel.Controls.Add(panel);
                }));
            }
            else
                Workers_Panel.Controls.Add(panel);
        }
        private void Update_Worker_Settings(Worker worker)
        {
            Worker_Name_TextBox.SetProperty("Text", worker.Name);
            Worker_Ip_TextBox.SetProperty("Text", worker.IP);
            foreach (var setting in worker.Get_Miner_Settings())
                Miner_Settings_ListBox.AddItem(setting.Display_Name);
        }
        private Panel Convert_Worker_to_Panel(Worker worker, double hash1 = -1, double hash2 = -1, double hash3 = -1)
        {
            var worker_summary = new WorkerSummary();
            var worker_panel = worker_summary.WorkerPanel;

            worker_panel.Name = worker.Name;

            // Allow user to click child controls and still select this panel
            foreach (Control control in worker_panel.Controls)
            {
                control.Click += (sender, e) =>
                {
                    // Cast the sender object to a Control
                    Control clickedControl = sender as Control;

                    // Get WorkerPanel's parent panel control
                    Panel workers_panel = worker_panel.Parent as Panel;

                    // Deselect all other WorkerSummary panels
                    foreach (Control ctrl in workers_panel.Controls)
                        if (ctrl is Panel workerSummary)
                            workerSummary.BorderStyle = BorderStyle.None;

                    // Select this panel
                    worker_panel.BorderStyle = BorderStyle.FixedSingle;

                    Update_Worker_Settings(worker);                    
                };
            }

            // Select/Deselct panels
            worker_panel.MouseClick += new MouseEventHandler((sender, e) =>
            {
                // Get WorkerPanel's parent panel control
                Panel workers_panel = worker_panel.Parent as Panel;

                // Deselect all other WorkerSummary panels
                foreach (Control control in workers_panel.Controls)
                    if (control is Panel workerSummary)
                        workerSummary.BorderStyle = BorderStyle.None;                    
                

                // Select this panel
                worker_panel.BorderStyle = BorderStyle.FixedSingle;

                Update_Worker_Settings(worker); 
            });

            worker_panel.Location = new Point(0, worker_yPos);
            worker_yPos += worker_panel.Height + worker_margin;

            PictureBox online_pic = (PictureBox)worker_panel.Controls.Find("Online_PictureBox", true).First();
            if (worker.Online)
                online_pic.Image = Resources.online_btn;
            else
                online_pic.Image = Resources.offline_btn;

            Label name = (Label)worker_panel.Controls.Find("Worker_Name", true).First();
            name.Text = worker.Name;

            if (hash1 > 0)
            {
                Label hashrate1 = (Label)worker_panel.Controls.Find("Hashrate1", true).First();
                hashrate1.Text = hash1.ToString("0.00");

                Label hashrate2 = (Label)worker_panel.Controls.Find("Hashrate2", true).First();
                hashrate2.Text = hash2.ToString("0.00");

                Label hashrate3 = (Label)worker_panel.Controls.Find("Hashrate3", true).First();
                hashrate3.Text = hash3.ToString("0.00");
            }
            return worker_panel;
        }
        private Worker Get_Selected_Worker()
        {
            string workerName = "";         

            // Find the selected worker
            foreach (Control ctrl in Workers_Panel.Controls)
            {
                if (ctrl is Panel && ((Panel)ctrl).BorderStyle != BorderStyle.None)
                {
                    Control workerNameCtrl = ctrl.Controls.Find("Worker_Name", true).FirstOrDefault();
                    if (workerNameCtrl != null && workerNameCtrl is Label)
                    {
                        workerName = ((Label)workerNameCtrl).Text;
                        break;
                    }
                }
            }

            return worker_list.Find(w => w.Name.Equals(workerName));
        }
        private void Add_Miner_Setting_To_ListBox(MinerSettings settings)
        {
            var display_name = settings.Display_Name;
            if (settings.Active)
                display_name = $"***{display_name}***";
            Miner_Settings_ListBox.AddItem(display_name);
            Miner_Settings_ListBox.SetProperty("SelectedIndex", Miner_Settings_ListBox.Items.Count - 1);
        }



        // Update stats
        public void UpdateMinerStats(MinerStats stats, bool only_selected = true)
        {
            if (stats == null) return;

            foreach (Control worker_panel in Workers_Panel.Controls)
            {
                if (worker_panel is Panel)
                {
                    if (only_selected && ((Panel)worker_panel).BorderStyle != BorderStyle.None)
                        continue;

                    var hashrate1 = worker_panel.Controls.Find("Hashrate1", true).FirstOrDefault();
                    hashrate1.SetProperty("Text", stats.Hashrate1.ToString());

                    var hashrate1_unit = worker_panel.Controls.Find("Hashrate1_Unit", true).FirstOrDefault();
                    hashrate1_unit.SetProperty("Text", "MH/s");

                    var hashrate2 = worker_panel.Controls.Find("Hashrate2", true).FirstOrDefault();
                    hashrate2.SetProperty("Text", stats.Hashrate2.ToString());

                    var hashrate2_unit = worker_panel.Controls.Find("Hashrate2_Unit", true).FirstOrDefault();
                    hashrate2_unit.SetProperty("Text", "MH/s");

                    var hashrate3 = worker_panel.Controls.Find("Hashrate3", true).FirstOrDefault();
                    hashrate3.SetProperty("Text", stats.Hashrate3.ToString());

                    var hashrate3_unit = worker_panel.Controls.Find("Hashrate3_Unit", true).FirstOrDefault();
                    hashrate3_unit.SetProperty("Text", "MH/s");
                }
            }
        }



        // Scan For All Workers
        private async Task<List<string>> Scan_Network_for_All_Workers(bool show_found_workers = false)
        {
            var found_servers = new List<string>();
            IPAddress clientIP = GetLocalIPAddress();
            var tasks = new List<Task>();

            // Iterate over all possible IP addresses on the local network
            for (int i = 1; i < 255; i++)
            {
                IPAddress ipAddress = IPAddress.Parse(clientIP.ToString().Substring(0, clientIP.ToString().LastIndexOf('.')) + "." + i.ToString());

                var task = Task.Run(() =>
                {
                    try
                    {
                        client_manager.Add_Client(ipAddress.ToString(), companionPort);
                        var exists = client_manager.Client_is_Online(ipAddress.ToString());
                        if (exists)
                        {
                            found_servers.Add(ipAddress.ToString());

                            if (show_found_workers)
                            {
                                var worker = new Worker();
                                worker.Name = "Worker" + found_servers.Count;
                                worker.IP = ipAddress.ToString();
                                worker.Online = true;
                                worker_list.Add(worker);
                                Add_Worker_Panel_to_Display(Convert_Worker_to_Panel(worker));
                            }
                        }

                    }
                    catch (Exception)
                    {
                    }
                });
                tasks.Add(task);
            }

            Task[] taskArray = tasks.ToArray();
            while (taskArray.Length > 0)
            {
                Task completedTask = await Task.WhenAny(taskArray);
                taskArray = taskArray.Where(t => t != completedTask).ToArray();

                // Update the UI
                Network_Scan_Count_Label.SetProperty("Text", (tasks.Count - taskArray.Length).ToString() + " / 255");
                Network_Scan_Count_Label.SetProperty("Visible", true);
            }

            Network_Scan_Count_Label.SetProperty("Visible", false);
            Network_Scan_Count_Label.SetProperty("Text", "");

            return found_servers;
        }
        private IPAddress GetLocalIPAddress()
        {
            IPAddress[] addresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        


    }

}
