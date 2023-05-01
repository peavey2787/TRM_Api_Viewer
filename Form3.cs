using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Timer = System.Windows.Forms.Timer;
using Size = System.Drawing.Size;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using TRM_Api_Viewer.Properties;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Windows.Media.Media3D;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Policy;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using System.Windows.Shapes;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime;

namespace TRM_Api_Viewer
{
    public partial class Gpus_Form : Form
    {
        public bool isMoving { get; set; }
        public bool GetIsMoving() { return isMoving; }
        public bool isDocked { get; set; }
        public bool GetIsDocked() { return isDocked; }
        public bool isSnapped { get; set; }
        public bool GetIsSnapped() { return isSnapped; }

        ClientManager clientManager;
        ChromeDriver driver = null;
        private Gpus_Form snappedTo = null;
        private Gpus_Form dockedTo = null;
        private Worker Worker = null;
        private List<Worker> Workers = null;
        private Timer countdownTimer;
        private int countdownSeconds = 30;

        public Gpus_Form(Worker worker, ClientManager clientManager)
        {
            InitializeComponent();
            Worker = worker;
            this.clientManager = clientManager;
        }
        public Gpus_Form(List<Worker> workers, ClientManager clientManager)
        {
            InitializeComponent();
            Workers = workers;
            this.clientManager = clientManager;
        }



        // Start/Stop
        private void Gpus_Form_Load(object sender, EventArgs e)
        {
            if (Worker != null)
                this.Text = "GPU Stats - " + Worker.Name + " - " + Worker.IP;
            else
                this.Text = "GPU Stats - Totals";

            Show_Stats();
            StartCountdown();
        }
        private bool StartUp()
        {
            if (driver != null) { return true; }

            string chromeDriverPath = Directory.GetCurrentDirectory() + "\\chromedriver.exe";

            // disable notifications and alerts
            var options = new ChromeOptions();
            options.AddArgument("--mute-audio");
            options.AddArgument("--disable-notifications");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--headless");

            // Set up the ChromeDriverService options to hide the command prompt window
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            // Create Chrome User Data dir (this can't be shared for multiple instances of ChromeDriver
            var userDataDir = Directory.GetCurrentDirectory() + "\\ChromeUserData";
            if (!Directory.Exists(userDataDir))
                Directory.CreateDirectory(userDataDir);

            driver = new ChromeDriver(service, options);

            return true;
        }
        private void StartCountdown()
        {
            // Create a new Timer with an interval of 1 second
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;

            // Start the timer
            countdownTimer.Start();
        }
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdownSeconds--;

            if (countdownSeconds == 0)
            {
                // Restart the countdown if it reaches 0
                countdownSeconds = 30;

                Show_Stats();
            }

            Count_Down_Label.Text = countdownSeconds.ToString();
        }
        private void StopCountdown()
        {
            countdownTimer.Stop();
            countdownTimer.Dispose();
        }
        public void ShutDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }

            // get a list of all running processes
            Process[] processes = Process.GetProcesses();

            // loop through the list and find the chromedriver process
            foreach (Process process in processes)
            {
                if (process.ProcessName.StartsWith("chromedriver"))
                {
                    // kill the chromedriver process
                    process.Kill();
                }
            }
        }
        private void Gpus_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Cancel the form closing event
            e.Cancel = true;

            // Hide the form instead of closing it
            this.Hide();

            //StopCountdown();
            //ShutDown();            
        }



        // GUI Updated
        private void Show_Stats()
        {
            MinerStats stats = clientManager.Get_Stats(Worker.IP, Worker.Get_Active_Setting());
            if (stats != null)
            {
                Display_Gpus(stats.Gpus);

                // Update Main form stats
                Form1 parentForm = (Form1)this.Owner;
                if(parentForm.InvokeRequired)
                {
                    parentForm.Invoke((MethodInvoker)delegate
                    {
                        parentForm.UpdateMinerStats(stats, false);
                    });
                }
                else
                    parentForm.UpdateMinerStats(stats, false);
            }
        }
        private void oldShow_Stats()
        {
            if (Workers != null && Workers.Count > 0)
            {
                var all_gpus = new List<Gpu>();

                foreach (Worker worker in Workers)
                {
                    var gpus = new List<Gpu>();

                    if (worker.Get_Active_Setting().Name == "gminer")
                    {
                        gpus = Get_Gpus_from_Gminer(worker);
                    }
                    else if (worker.Get_Active_Setting().Name == "trm")
                    {
                        gpus = Get_Gpus_from_TRM(worker, 1);
                        var gpus2 = Get_Gpus_from_TRM(worker, 2);

                        if (gpus != null && gpus.Count > 0)
                            for (int i = 0; i < gpus.Count; i++)
                                gpus[i].Speed2 = gpus2[i].Speed1;
                    }
                    if (gpus != null)
                        all_gpus.AddRange(gpus);
                }

                Display_Gpus(all_gpus);
            }
            else if (Worker.Get_Active_Setting().Name == "trm")
            {
                var gpus = Get_Gpus_from_TRM(Worker, 1);
                var gpus2 = Get_Gpus_from_TRM(Worker, 2);

                if (gpus != null && gpus.Count > 0)
                    for (int i = 0; i < gpus.Count; i++)
                        gpus[i].Speed2 = gpus2[i].Speed1;

                Display_Gpus(gpus);
            }
            else if (Worker.Get_Active_Setting().Name == "gminer")
            {
                List<Gpu> gpus = Get_Gpus_from_Gminer(Worker);
                Display_Gpus(gpus);
            }
        }
        public void Display_Gpus(List<Gpu> gpus)
        {
            if (gpus == null || gpus.Count == 0)
                return;

            int xPos = 10;
            int yPos = 0;
            var scroll = Stats_Panel.HorizontalScroll.Value;
            Stats_Panel.HorizontalScroll.Value = 0;

            foreach (Gpu gpu in gpus)
            {
                // Get/create gpu panel
                var gpu_panel = Get_Gpu_Panel(gpu.Worker_Name + gpu.Name + gpu.Id);
                gpu_panel.Location = new Point(xPos, yPos);
                xPos += gpu_panel.Width + 10;

                var bg_color = Color.Red;
                if (Worker != null && Worker.Get_Active_Setting().Name == "gminer")
                {
                    Worker_Name.Text = Worker.Name;
                    bg_color = Color.Green;
                }
                else if (Worker == null)
                {
                    Worker_Name.Text = "Totals";
                    bg_color = Color.Gray;
                }
                this.BackColor = bg_color;
                gpu_panel.BackColor = SystemColors.Control;

                Label gpu_label = (Label)gpu_panel.Controls.Find("GPU", true).First();
                gpu_label.Text = gpu.Worker_Name + " - " + gpu.Name + " - " + gpu.Id;

                PictureBox status_label = (PictureBox)gpu_panel.Controls.Find("Online_Picture_Box", true).First();
                if (gpu.Online)
                    status_label.Image = Resources.online_btn;
                else
                    status_label.Image = Resources.offline_btn;

                Label temperature_label = (Label)gpu_panel.Controls.Find("Temperature", true).First();
                temperature_label.Text = gpu.Temperature.ToString();

                Label fan_percent_label = (Label)gpu_panel.Controls.Find("Fan_Percent", true).First();
                fan_percent_label.Text = gpu.Fan_Percent.ToString();

                Label gpu_clock_label = (Label)gpu_panel.Controls.Find("GPU_Clock", true).First();
                gpu_clock_label.Text = gpu.Core_Clock.ToString();

                Label memory_clock_label = (Label)gpu_panel.Controls.Find("Memory_Clock", true).First();
                memory_clock_label.Text = gpu.Mem_Clock.ToString();

                Label gpu_voltage_label = (Label)gpu_panel.Controls.Find("GPU_Volts", true).First();
                gpu_voltage_label.SetProperty("Text", gpu.Core_Mv.ToString());

                Label mhs_avg_label = (Label)gpu_panel.Controls.Find("MHS_av", true).First();
                mhs_avg_label.SetProperty("Text", gpu.Speed1.ToString("0.00"));

                Label accepted_label = (Label)gpu_panel.Controls.Find("Accepted", true).First();
                accepted_label.SetProperty("Text", gpu.Accepted1.ToString());

                Label rejected_label = (Label)gpu_panel.Controls.Find("Rejected", true).First();
                rejected_label.Text = gpu.Rejected1.ToString();

                Label hardware_errors_label = (Label)gpu_panel.Controls.Find("Hardware_Errors", true).First();
                PictureBox hardware_errors_picture = (PictureBox)gpu_panel.Controls.Find("Hardware_Errors_Picture", true).First();
                if (gpu.Hardware_Errors > 0)
                {
                    hardware_errors_label.Visible = true;
                    hardware_errors_picture.Visible = true;
                    hardware_errors_label.Text = gpu.Hardware_Errors.ToString();
                }
                else
                {
                    hardware_errors_label.Visible = false;
                    hardware_errors_picture.Visible = false;
                }

                Label intensity_label = (Label)gpu_panel.Controls.Find("Intensity", true).First();
                if (gpu.Intensity > 0)
                {
                    intensity_label.Text = gpu.Intensity.ToString();
                }
                else if (gpu.Efficiency > 0)
                {
                    intensity_label.Text = gpu.Efficiency.ToString("0.00") + " " + gpu.Efficiency_Unit;
                    Label efficiency_label = (Label)gpu_panel.Controls.Find("Intensity_Label", true).First();
                    efficiency_label.Text = "Efficiency";
                }

                Label last_share_time_label = (Label)gpu_panel.Controls.Find("Last_Share_Time", true).First();
                Label last_share_label = (Label)gpu_panel.Controls.Find("Last_Share_Label", true).First();
                if (gpu.Last_Share_Time == 0)
                {
                    last_share_label.Visible = false;
                    last_share_time_label.Text = "";
                }
                else
                {
                    last_share_label.Visible = true;
                    last_share_time_label.Text = Gpu.Get_Human_Readable_Date_Time(gpu.Last_Share_Time);
                }

                Label temperature_mem_label = (Label)gpu_panel.Controls.Find("TemperatureMem", true).First();
                temperature_mem_label.Text = gpu.Mem_Temp.ToString();

                Label gpu_power_label = (Label)gpu_panel.Controls.Find("GPU_Power", true).First();
                gpu_power_label.Text = gpu.Power.ToString();

                // Algo 2
                Label mhs_avg_label2 = (Label)gpu_panel.Controls.Find("MHS_av2", true).First();
                mhs_avg_label2.SetProperty("Text", gpu.Speed2.ToString("0.00"));

                // Algo 3
                Label mhs_avg_label3 = (Label)gpu_panel.Controls.Find("MHS_av3", true).First();
                mhs_avg_label3.SetProperty("Text", gpu.Speed3.ToString());

                // Update line chart
                Chart Gpu_Chart_label = (Chart)gpu_panel.Controls.Find("Gpu_Chart", true).First();
                DataPoint mhs_avg_dp = new DataPoint(DateTime.Now.ToOADate(), gpu.Speed1);
                DataPoint mhs_avg2_dp = new DataPoint(DateTime.Now.ToOADate(), gpu.Speed2);
                DataPoint temperature_dp = new DataPoint(DateTime.Now.ToOADate(), gpu.Temperature);

                Gpu_Chart_label.Series[0].Points.Add(mhs_avg_dp);
                Gpu_Chart_label.Series[1].Points.Add(mhs_avg2_dp);
                Gpu_Chart_label.Series[2].Points.Add(temperature_dp);

                // Set the scale view to show the last 24 hours of data
                Gpu_Chart_label.ChartAreas[0].AxisX.ScaleView.Zoom(DateTime.Now.AddDays(-1).ToOADate(), DateTime.Now.ToOADate());

                // Hide the major and minor gridlines
                Gpu_Chart_label.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                Gpu_Chart_label.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                Gpu_Chart_label.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                Gpu_Chart_label.ChartAreas[0].AxisY.MinorGrid.Enabled = false;

                // Always show or hide the scrollbar
                //chartArea.AxisX.ScrollBar.Enabled = true; // always show
                Gpu_Chart_label.ChartAreas[0].AxisX.ScrollBar.Enabled = false; // always hide

                var found = Stats_Panel.Controls.Find("Gpu_Panel" + gpu.Worker_Name + gpu.Name + gpu.Id, true);
                if (found.Count() == 0)
                    Stats_Panel.Controls.Add(gpu_panel);
            }


            // Scroll the panel to the saved position
            Stats_Panel.HorizontalScroll.Value = scroll;
            Stats_Panel.ResumeLayout();
            Stats_Panel.AutoScrollPosition = new Point(scroll, 0);

            double totalPower = 0, totalSpeed1 = 0, totalSpeed2 = 0, totalSpeed3 = 0;
            foreach (Gpu gpu in gpus)
            {
                totalPower += gpu.Power;
                totalSpeed1 += gpu.Speed1;
                totalSpeed2 += gpu.Speed2;
                totalSpeed3 += gpu.Speed3;
            }

            // Show Totals
            Add_Docked_to_Totals(totalSpeed1, totalSpeed2, totalSpeed3, totalPower);
        }
        private void Add_Docked_to_Totals(double mhs1, double mhs2, double mhs3, double power)
        {
            if (isDocked)
            {
                // Add docked form's totals
                Label total_mhs_label_docked = (Label)dockedTo.Controls.Find("Total_Mhs", true).First();
                if (total_mhs_label_docked != null)
                    mhs1 += double.Parse(total_mhs_label_docked.Text);

                Label total_mhs2_label_docked = (Label)dockedTo.Controls.Find("Total_Mhs2", true).First();
                if (total_mhs2_label_docked != null)
                    mhs2 += double.Parse(total_mhs2_label_docked.Text);

                Label total_mhs3_label_docked = (Label)dockedTo.Controls.Find("Total_Mhs3", true).First();
                if (total_mhs3_label_docked != null)
                    mhs3 += double.Parse(total_mhs3_label_docked.Text);

                Label gpu_power_total_label_docked = (Label)dockedTo.Controls.Find("Total_Power", true).First();
                if (gpu_power_total_label_docked != null)
                    power += double.Parse(gpu_power_total_label_docked.Text);
            }
            Total_Mhs.Text = mhs1.ToString("0.00");
            Total_Mhs2.Text = mhs2.ToString("0.00");
            Total_Mhs3.Text = mhs3.ToString("0.00");
            Total_Power.Text = power.ToString("0.00");
        }
        private Panel Get_Gpu_Panel(string name)
        {
            string panel_name = "Gpu_Panel";

            // Check if the panel already exists on the form
            Panel stats_panel = (Panel)this.Controls.Find("Stats_Panel", true).First();
            foreach (Panel panel in stats_panel.Controls)
                if (panel.Name == panel_name + name)
                    return panel;

            // Create a new instance of the GpuControl and set its name
            var gs = new Gpu_Stats_Form();
            var newPanel = gs.GpuPanel;
            newPanel.Name = panel_name + name;

            return newPanel;
        }



        // TRM
        public List<Gpu> Get_Gpus_from_TRM(Worker worker, int algo)
        {
            JObject data = null;
            try
            {
                var receiveBytes = new byte[4024];
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    /*int port = worker.Get_Active_Setting().API_Port;
                    if (algo == 2)
                        port = worker.Get_Active_Setting().API2_Port;
                    socket.Connect(IPAddress.Parse(worker.IP), port);
                    socket.Send(Encoding.ASCII.GetBytes("{\"command\":\"devs\"}"));
                    socket.Receive(receiveBytes, receiveBytes.Length, SocketFlags.None);*/
                }
                var responseJson = Encoding.ASCII.GetString(receiveBytes);

                data = JObject.Parse(responseJson);
            }
            catch { return null; }

            var gpus = new List<Gpu>();

            JArray devices = (JArray)data["DEVS"];
            if (devices == null)
                return gpus;

            foreach (JObject device in devices)
            {
                var gpu = new Gpu();
                gpu.Id = int.Parse(device.Property("GPU").Value.ToString());
                gpu.Worker_Name = worker.Name;

                string status = device.Property("Status").Value.ToString();
                if (status == "Alive")
                    gpu.Online = true;
                else
                    gpu.Online = false;

                gpu.Temperature = int.Parse(device.Property("Temperature").Value.ToString());
                gpu.Fan_Percent = int.Parse(device.Property("Fan Percent").Value.ToString());
                gpu.Core_Clock = int.Parse(device.Property("GPU Clock").Value.ToString());
                gpu.Mem_Clock = int.Parse(device.Property("Memory Clock").Value.ToString());
                gpu.Core_Mv = double.Parse(device.Property("GPU Voltage").Value.ToString());
                gpu.Speed1 = double.Parse(device.Property("MHS 30s").Value.ToString()); // "MHS av" = average "MHS 30s" = last 30secs
                gpu.Accepted1 = int.Parse(device.Property("Accepted").Value.ToString());
                gpu.Rejected1 = int.Parse(device.Property("Rejected").Value.ToString());
                gpu.Hardware_Errors = int.Parse(device.Property("Hardware Errors").Value.ToString());
                gpu.Intensity = int.Parse(device.Property("Intensity").Value.ToString());
                gpu.Last_Share_Time = double.Parse(device.Property("Last Share Time").Value.ToString());
                gpu.Mem_Temp = int.Parse(device.Property("TemperatureMem").Value.ToString());
                gpu.Power = double.Parse(device.Property("GPU Power").Value.ToString());

                gpus.Add(gpu);
            }
            return gpus;
        }



        // Gminer
        private bool WaitUntilElementExists(int retriesInSeconds, string bySelector, string selector)
        {
            // set the initial timeout and retry interval
            TimeSpan retryInterval = TimeSpan.FromSeconds(1);

            // create a new timer to track the timeout
            DateTime start = DateTime.Now;

            // create a new By object based on the bySelector string
            By by = null;

            switch (bySelector.ToLower())
            {
                case "id":
                    by = By.Id(selector);
                    break;
                case "name":
                    by = By.Name(selector);
                    break;
                case "classname":
                    by = By.ClassName(selector);
                    break;
                case "cssselector":
                    by = By.CssSelector(selector);
                    break;
                case "linktext":
                    by = By.LinkText(selector);
                    break;
                case "partiallinktext":
                    by = By.PartialLinkText(selector);
                    break;
                case "tagname":
                    by = By.TagName(selector);
                    break;
                case "xpath":
                    by = By.XPath(selector);
                    break;
                default:
                    return false; // Invalid selector type
            }

            // loop until the element is found or the max retries are reached
            int retries = 0;
            while (retries < retriesInSeconds)
            {
                // define the CSS selector for the checkbox
                string checkboxSelector = "input[type='checkbox']";

                // try to find the checkbox in the main document
                IWebElement checkbox = null;

                try
                {
                    checkbox = driver.FindElement(By.CssSelector(checkboxSelector));
                }
                catch (NoSuchElementException)
                {
                    // checkbox not found in main document, continue to search in shadow roots
                }

                if (checkbox != null)
                {
                    // checkbox found in main document, interact with it
                    checkbox.Click();
                }

                var elements = driver.FindElements(by);
                var el = driver.FindElements(By.CssSelector(selector));

                // if we found the element, return true
                if (elements != null && elements.Count > 0)
                {
                    return true;
                }

                // wait for the retry interval before trying again
                Thread.Sleep(retryInterval);
                retries++;
            }

            // if we didn't find the element after all retries, return false
            return false;
        }
        public List<Gpu> Get_Gpus_from_Gminer(Worker worker)
        {
            string url = "http://" + worker.IP + ":" + worker.Get_Active_Setting().Port1.ToString();
            var gpus = new List<Gpu>();

            try
            {
                var ready = StartUp();
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(500);
                var exists = WaitUntilElementExists(10, "tagname", "body");

                // find table rows with GPU data
                var gpuRows = driver.FindElements(By.CssSelector("#device_stat tr:not(#total)"));

                foreach (var row in gpuRows)
                {
                    var gpu = new Gpu();
                    gpu.Worker_Name = worker.Name;
                    var cells = row.FindElements(By.TagName("td"));
                    if (cells != null && cells.Count > 1)
                    {
                        if (cells[4].Text == "Speed")
                            continue;

                        int.TryParse(cells[0].Text, out int id);
                        int.TryParse(cells[2].Text.TrimEnd('%'), out int fan);
                        int.TryParse(cells[6].Text, out int core);
                        int.TryParse(cells[7].Text, out int memory);
                        int.TryParse(cells[8].Text.TrimEnd('W'), out int power);

                        // MHS
                        string[] parts = cells[4].Text.Split('+');
                        var first = parts[0].Substring(0, parts[0].LastIndexOf(' '));
                        double firstNumber = float.Parse(first.Split()[0]);
                        var sec = parts[1].Substring(1, parts[1].LastIndexOf(' '));
                        float secondNumber = float.Parse(sec.Split()[0]);

                        // Online status
                        if (firstNumber > 0 || secondNumber > 0)
                            gpu.Online = true;
                        else
                            gpu.Online = false;

                        // Get Temps
                        string[] temps = cells[3].Text.Split('/');
                        var temp = int.Parse(temps[0].Replace('C', ' ').TrimStart().TrimEnd());
                        var memTemp = 0;
                        if (temps.Length > 1)
                            memTemp = int.Parse(temps[1].Replace('C', ' ').TrimStart().TrimEnd());

                        // Shares 
                        var share_parts = cells[5].Text.Split('+');
                        var Algo1 = share_parts[0].Trim().Split('/');
                        var Algo2 = share_parts[1].Trim().Split('/');

                        gpu.Accepted1 = int.Parse(Algo1[0].Trim());
                        gpu.Rejected1 = int.Parse(Algo1[1].Trim());
                        gpu.Accepted2 = int.Parse(Algo2[0].Trim());
                        gpu.Rejected2 = int.Parse(Algo2[1].Trim());

                        gpu.Id = id;
                        gpu.Name = cells[1].Text;
                        gpu.Fan_Percent = fan;
                        gpu.Temperature = temp;
                        gpu.Mem_Temp = memTemp;
                        gpu.Speed1 = firstNumber;
                        gpu.Speed2 = secondNumber; ;
                        gpu.Core_Clock = core;
                        gpu.Mem_Clock = memory;
                        gpu.Power = power;
                        if(double.TryParse(cells[9].Text, out double eff))
                            gpu.Efficiency = eff;

                        // add gpu data to list
                        gpus.Add(gpu);
                    }
                }
                return gpus;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error connecting to " + url + ": " + ex.Message);
                return null;
            }
        }



        private MinerSettings Temp_Create_Settings()
        {
            MinerSettings settings = new MinerSettings();

            settings.Name = "gminer";
            settings.Version = "3.31";
            settings.Active = true;

            settings.Arguments_Dictionary.Add("algo", "autolykos2");
            settings.Arguments_Dictionary.Add("server", "us-erg.2miners.com");
            settings.Arguments_Dictionary.Add("port", "18888");
            settings.Arguments_Dictionary.Add("user", "9ew6Esi2QeEv1V1FzQ6h1QwxFG1FaTXnEv8zMobH7HmXMyQRkwR.CC3080ti");
            settings.Arguments_Dictionary.Add("worker", "CC3080ti");
            settings.Arguments_Dictionary.Add("ssl", "1");

            settings.Arguments_Dictionary.Add("dalgo", "kheavyhash");
            settings.Arguments_Dictionary.Add("dserver", "us2.kaspa.herominers.com");
            settings.Arguments_Dictionary.Add("dport", "1206");
            settings.Arguments_Dictionary.Add("duser", "kaspa:qrcpzchd5luqa268djhewsxn7v8yspcf3e7uck5rkklqm88my2td6avyjc9pk.CC3080ti");
            settings.Arguments_Dictionary.Add("dworker", "CC3080ti");
            settings.Arguments_Dictionary.Add("dssl", "1");

            settings.Arguments_Dictionary.Add("zilserver", "us.crazypool.org");
            settings.Arguments_Dictionary.Add("zilport", "5005");
            settings.Arguments_Dictionary.Add("ziluser", "zil1m6nhq2exhmd2tc9tmwkt8svhnh0q6qhzqzupnr.CC3080ti");

            settings.Arguments_Dictionary.Add("share_check", "0");
            settings.Arguments_Dictionary.Add("dual_intensity", "2");
            settings.Arguments_Dictionary.Add("api", "8888");
            return settings;
        }
        // Companion app Commands
        private void Restart_Miner_Button_Click(object sender, EventArgs e)
        {
            clientManager.Restart_Miner(Worker.IP, Worker.Get_Active_Setting());
        }
        private void Start_Miner_Button_Click(object sender, EventArgs e)
        {
            MinerSettings settings = Worker.Get_Active_Setting();
            settings.ProcessId = clientManager.Start_Miner(Worker.IP, Worker.Get_Active_Setting());
        }
        private void Stop_Miner_Button_Click(object sender, EventArgs e)
        {
            clientManager.Stop_Miner(Worker.IP, Worker.Get_Active_Setting());
        }
        private void Update_Miner_Button_Click(object sender, EventArgs e)
        {

        }
        private void View_Miner_Button_Click(object sender, EventArgs e)
        {
           //var settings = Temp_Create_Settings();
           var settings = Worker.Get_Active_Setting();
            //Worker.Add_Setting(settings);

            var viewMinerTitle = $"{Worker.Name}-{settings.Display_Name}";
            
            // Check if there's already a form with the same title
            var existingForm = System.Windows.Forms.Application.OpenForms.OfType<View_Miner>().FirstOrDefault(f => f.Text == viewMinerTitle);

            if (existingForm == null)
            {
                var view_miner = new View_Miner(Worker, ref clientManager);
                view_miner.Show();
            }
        }



        // Snap to other form  snapped = form on right  docked = form on left
        private void Dock_Button_Click(object sender, EventArgs e)
        {
            if (isSnapped || isDocked)
            {
                isSnapped = false;
                isDocked = false;
                Dock_Button.Text = "Dock";

                if (snappedTo != null)
                {
                    snappedTo.isDocked = false;
                    snappedTo.isSnapped = false;
                    snappedTo = null;
                }
                if (dockedTo != null)
                {
                    double mhs1 = 0, mhs2 = 0, power = 0;
                    
                    Label total_mhs_label_docked = (Label)dockedTo.Controls.Find("Total_Mhs", true).First();
                    if (total_mhs_label_docked != null)
                        mhs1 = double.Parse(total_mhs_label_docked.Text);

                    Label total_mhs2_label_docked = (Label)dockedTo.Controls.Find("Total_Mhs2", true).First();
                    if (total_mhs2_label_docked != null)
                        mhs2 = double.Parse(total_mhs2_label_docked.Text);

                    Label gpu_power_total_label_docked = (Label)dockedTo.Controls.Find("Total_Power", true).First();
                    if (gpu_power_total_label_docked != null)
                        power = double.Parse(gpu_power_total_label_docked.Text);

                    double curr_mhs = double.Parse(Total_Mhs.Text);
                    double curr_mhs2 = double.Parse(Total_Mhs2.Text);
                    double curr_pow = double.Parse(Total_Power.Text);
                    Total_Mhs.Text = (curr_mhs - mhs1).ToString("0.00");
                    Total_Mhs2.Text = (curr_mhs2 - mhs2).ToString("0.00");
                    Total_Power.Text = (curr_pow - power).ToString("0.00");

                    dockedTo.isDocked = false;
                    dockedTo.isSnapped = false;
                    dockedTo = null;
                }
            }
            else
            {
                SnapToNearestForm();
            }
        }
        private void SnapToNearestForm()
        {
            Gpus_Form form2 = (Gpus_Form)GetNextClosestForm();

            if (form2 != null)
            {
                Dock_Button.Text = "Undock";

                // Set the location of the current form to be snapped to the right of the nearest form
                form2.Left = this.Right;
                form2.Top = this.Top;

                // Set the snapped form flag and store the snapping form
                form2.isSnapped = true;
                form2.snappedTo = this;
                form2.isDocked = false;

                this.isSnapped = false;
                this.isDocked = true;
                this.dockedTo = form2;

                Add_Docked_to_Totals(double.Parse(Total_Mhs.Text), double.Parse(Total_Mhs2.Text), 0, double.Parse(Total_Power.Text));
            }
        }
        private Form GetNextClosestForm()
        {
            Type formType = this.GetType(); // Get the type of the current form
            const int SNAP_DISTANCE = 200;
            const int SNAP_TOP_DISTANCE = 400;
            Form closestForm = null;

            // Iterate through all open forms
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                // Check if the form is of the same type and is not the current form
                if (form.GetType() == formType && form != this)
                {

                    // If this form is closer than the current closest form, update the closest form
                    if (form != null &&
                        ((this.Right <= form.Left && form.Left - this.Right <= SNAP_DISTANCE) ||
                        (this.Right >= form.Left && this.Right - form.Left <= SNAP_DISTANCE)) &&
                        ((this.Top >= form.Top && Math.Abs(this.Top - form.Top) <= SNAP_TOP_DISTANCE) ||
                        (this.Top <= form.Top && Math.Abs(form.Top - this.Top) <= SNAP_TOP_DISTANCE)))
                        closestForm = form;
                    
                }
            }
            return closestForm;
        }
        private void Gpus_Form_LocationChanged(object sender, EventArgs e)
        {
            if (!isMoving && isSnapped && snappedTo != null)
            {
                snappedTo.isMoving = true;
                snappedTo.Left = this.Left - snappedTo.Width;
                snappedTo.Top = this.Top;                
                snappedTo.isMoving = false;
            }
            else if (!isMoving && isDocked && dockedTo != null)
            {
                dockedTo.isMoving = true;
                dockedTo.Left = this.Right;
                dockedTo.Top = this.Top;
                dockedTo.isMoving = false;
            }
        }
        private void Gpus_Form_Resize(object sender, EventArgs e)
        {
            if(dockedTo != null) 
            {
                dockedTo.isMoving = true;
                dockedTo.Left = this.Right;
                dockedTo.Top = this.Top;
                dockedTo.Height = this.Height;
                dockedTo.isMoving = false;
            }
        }


    }
}
