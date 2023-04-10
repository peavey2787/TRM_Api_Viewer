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

namespace TRM_Api_Viewer
{
    public partial class Gpus_Form : Form
    {
        public bool isMoving { get; set; }
        public bool GetIsMoving() { return isMoving; }
        public bool isDocked { get; set; }
        public bool GetIsDocked() { return isDocked; }
        public bool isSnapped {get; set;}
        public bool GetIsSnapped() { return isSnapped; }

        private Gpus_Form snappedTo = null;
        private Gpus_Form dockedTo = null;
        private Worker Worker = null;
        private Timer countdownTimer;
        private int countdownSeconds = 30;

        public Gpus_Form(Worker worker)
        {
            InitializeComponent();
            Worker = worker;
        }
        private void Gpus_Form_Load(object sender, EventArgs e)
        {
            this.Text = "GPU Stats - " + Worker.Worker_IP + ":" + Worker.Worker_Port;

            Show_Stats();
            StartCountdown();
        }
        private void StopCountdown()
        {
            countdownTimer.Stop();
            countdownTimer.Dispose();
        }
        private void Gpus_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCountdown();
            ShutDown();
        }


        private async void Show_Stats()
        {
            if (Worker.Miner == "trm")
            {
                var resps = new List<string>();
                var responseJson = Get_Stats(IPAddress.Parse(Worker.Worker_IP), Worker.Worker_Port);
                var responseJson2 = Get_Stats(IPAddress.Parse(Worker.Worker_IP), Worker.Algo2_Port);
                resps.Add(responseJson);
                resps.Add(responseJson2);
                Update_All_Gpus_and_Stats(resps);
            }
            else if (Worker.Miner == "gminer")
            {
                List<dynamic> gpus = await Get_GPU_Data_From_Gminer(Worker.Worker_IP, Worker.Worker_Port);
                Display_GPU_Data_From_Gminer(gpus);
            }
        }
        private string Get_Stats(IPAddress ip, int port)
        {
            var receiveBytes = new byte[4024];
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(ip, port);
                socket.Send(Encoding.ASCII.GetBytes("{\"command\":\"devs\"}"));
                socket.Receive(receiveBytes, receiveBytes.Length, SocketFlags.None);
            }
            var responseJson = Encoding.ASCII.GetString(receiveBytes);
            return responseJson;
        }
        private Panel Get_Gpu_Panel(string gpuIndex)
        {
            string panel_name = "Gpu_Panel";

            // Check if the panel already exists on the form
            Panel stats_panel = (Panel)this.Controls.Find("Stats_Panel", true).First();
            foreach (Panel panel in stats_panel.Controls)
                if (panel.Name == panel_name + gpuIndex)
                    return panel;            

            // Create a new instance of the GpuControl and set its name
            var gs = new Gpu_Stats();
            var newPanel = gs.GpuPanel;
            newPanel.Name = panel_name + gpuIndex;

            return newPanel;
        }
        public void Update_All_Gpus_and_Stats(List<string> stats)
        {
            JObject data = null;
            JObject data2 = null;
            string json = stats[0];
            string json2 = stats[1];
            try 
            {
                data = JObject.Parse(json);
                data2 = JObject.Parse(json2);
            }
            catch
            {
                return;
            }
            // Get the Devs array from the parsed data
            JArray devices = (JArray)data["DEVS"];
            JArray devices2 = (JArray)data2["DEVS"];

            double gpuPowerTotal = 0;
            double mhsAvTotal = 0;
            double mhsAvTotal2 = 0;
            int acceptedTotal = 0;
            int rejectedTotal = 0;
            int deviceIndex = 0;
            int xPos = 10;
            int yPos = 0;
            foreach (JObject device in devices)
            {
                // Prevent wrong gpu stats showing
                if (deviceIndex.ToString() != ((JObject)devices2[deviceIndex]).Property("GPU").Value.ToString())
                    return;

                // Get/create gpu panel
                string gpu = device.Property("GPU").Value.ToString();
                var gpu_panel = Get_Gpu_Panel(gpu);
                gpu_panel.Location = new Point(xPos, yPos);
                xPos += gpu_panel.Width + 10;


                Label gpu_label = (Label)gpu_panel.Controls.Find("GPU", true).First();
                gpu_label.Text = gpu;

                Label enabled_label = (Label)gpu_panel.Controls.Find("Enabled", true).First();
                string enabled = device.Property("Enabled").Value.ToString();
                // Only show disabled
                if (enabled == "N")
                {
                    enabled_label.Visible = true;
                    enabled_label.Text = "Disabled";
                }
                else
                    enabled_label.Visible = false;

                PictureBox status_label = (PictureBox)gpu_panel.Controls.Find("Online_Picture_Box", true).First();
                string status = device.Property("Status").Value.ToString();
                if (status == "Alive")
                    status_label.Image = Resources.online_btn;
                else
                    status_label.Image = Resources.offline_btn;

                Label temperature_label = (Label)gpu_panel.Controls.Find("Temperature", true).First();
                string temperature = device.Property("Temperature").Value.ToString();
                temperature_label.Text = temperature;

                Label fan_percent_label = (Label)gpu_panel.Controls.Find("Fan_Percent", true).First();
                string fan_percent = device.Property("Fan Percent").Value.ToString();
                fan_percent_label.Text = fan_percent;

                Label gpu_clock_label = (Label)gpu_panel.Controls.Find("GPU_Clock", true).First();
                string gpu_clock = device.Property("GPU Clock").Value.ToString();
                gpu_clock_label.Text = gpu_clock;

                Label memory_clock_label = (Label)gpu_panel.Controls.Find("Memory_Clock", true).First();
                string memory_clock = device.Property("Memory Clock").Value.ToString();
                memory_clock_label.Text = memory_clock;

                Label gpu_voltage_label = (Label)gpu_panel.Controls.Find("GPU_Volts", true).First();
                string gpu_voltage = device.Property("GPU Voltage").Value.ToString();
                gpu_voltage_label.Text = gpu_voltage;

                Label mhs_avg_label = (Label)gpu_panel.Controls.Find("MHS_av", true).First(); 
                string mhs_avg = device.Property("MHS 30s").Value.ToString(); // "MHS av" = average "MHS 30s" = last 30secs
                mhs_avg_label.Text = mhs_avg;
                mhsAvTotal += double.Parse(mhs_avg);

                Label accepted_label = (Label)gpu_panel.Controls.Find("Accepted", true).First();
                string accepted = device.Property("Accepted").Value.ToString();
                accepted_label.Text = accepted;
                acceptedTotal += int.Parse(accepted);

                Label rejected_label = (Label)gpu_panel.Controls.Find("Rejected", true).First();
                string rejected = device.Property("Rejected").Value.ToString();
                rejected_label.Text = rejected;
                rejectedTotal += int.Parse(rejected);

                Label hardware_errors_label = (Label)gpu_panel.Controls.Find("Hardware_Errors", true).First();
                PictureBox hardware_errors_picture = (PictureBox)gpu_panel.Controls.Find("Hardware_Errors_Picture", true).First();
                string hardware_errors = device.Property("Hardware Errors").Value.ToString();
                int hw_errors = int.Parse(hardware_errors);
                if (hw_errors > 0)
                {
                    hardware_errors_label.Visible = true;
                    hardware_errors_picture.Visible = true;
                    hardware_errors_label.Text = hardware_errors;
                }
                else
                {
                    hardware_errors_label.Visible = false;
                    hardware_errors_picture.Visible = false;
                }

                Label intensity_label = (Label)gpu_panel.Controls.Find("Intensity", true).First();
                string intensity = device.Property("Intensity").Value.ToString();
                intensity_label.Text = intensity;

                Label last_share_time_label = (Label)gpu_panel.Controls.Find("Last_Share_Time", true).First();
                string last_share_time = device.Property("Last Share Time").Value.ToString();
                var time = double.Parse(last_share_time);
                DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime humanTime = unixTime.AddSeconds(time).ToLocalTime();
                last_share_time_label.Text = humanTime.ToString("MM/dd hh:mm:ss tt");

                Label temperature_mem_label = (Label)gpu_panel.Controls.Find("TemperatureMem", true).First();
                string temperature_mem = device.Property("TemperatureMem").Value.ToString();
                temperature_mem_label.Text = temperature_mem;

                Label gpu_power_label = (Label)gpu_panel.Controls.Find("GPU_Power", true).First();
                string gpu_power = device.Property("GPU Power").Value.ToString();
                gpu_power_label.Text = gpu_power;
                gpuPowerTotal += int.Parse(gpu_power);

                // Algo 2
                Label mhs_avg_label2 = (Label)gpu_panel.Controls.Find("MHS_av2", true).First();
                string mhs_avg2 = ((JObject)devices2[deviceIndex]).Property("MHS 30s").Value.ToString(); // "MHS av" = average "MHS 30s" = last 30secs
                mhs_avg_label2.Text = mhs_avg2;
                mhsAvTotal2 += double.Parse(mhs_avg2);

                // Show Totals
                Total_Mhs.Text = mhsAvTotal.ToString();
                Total_Mhs2.Text = mhsAvTotal2.ToString();
                Total_Power.Text = gpuPowerTotal.ToString();
                Total_Accepted.Text = acceptedTotal.ToString();
                Total_Rejected.Text = rejectedTotal.ToString();

                // Update line chart
                Chart Gpu_Chart_label = (Chart)gpu_panel.Controls.Find("Gpu_Chart", true).First();
                DataPoint mhs_avg_dp = new DataPoint(DateTime.Now.ToOADate(), double.Parse(mhs_avg));
                DataPoint mhs_avg2_dp = new DataPoint(DateTime.Now.ToOADate(), double.Parse(mhs_avg2));
                DataPoint temperature_dp = new DataPoint(DateTime.Now.ToOADate(), double.Parse(temperature));

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

                Stats_Panel.Controls.Add(gpu_panel);
                deviceIndex++;
            }
        }




        // Call this method to start the timer
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


        private void Restart_Miner_Button_Click(object sender, EventArgs e)
        {
            Restart_Miner();
        }
        private void Restart_Miner()
        {
            var receiveBytes = new byte[4024];
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(Worker.Worker_IP, Worker.Worker_Port);
                socket.Send(Encoding.ASCII.GetBytes(@"{""command"":""restart=*""}"));
                socket.Receive(receiveBytes, receiveBytes.Length, SocketFlags.None);
            }
            var responseJson = Encoding.ASCII.GetString(receiveBytes);
            int i = 0;
        }


        // Snap to other form  snapped = form on right  docked = form on left
        private Form GetNextClosestForm()
        {
            Type formType = this.GetType(); // Get the type of the current form

            Form closestForm = null;
            int closestDistance = int.MaxValue;

            // Iterate through all open forms
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                // Check if the form is of the same type and is not the current form
                if (form.GetType() == formType && form != this)
                {
                    // Calculate the horizontal distance between the current form and this form
                    int distance = Math.Abs(form.Right - this.Left);

                    // If this form is closer than the current closest form, update the closest form
                    if (distance < closestDistance)
                    {
                        closestForm = form;
                        closestDistance = distance;
                    }
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
                if(dockedTo != null)
                {
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


            const int SNAP_DISTANCE = 200;
            const int SNAP_TOP_DISTANCE = 400;
            if (form2 != null &&
                ((this.Right <= form2.Left && form2.Left - this.Right <= SNAP_DISTANCE) ||
                (this.Right >= form2.Left && this.Right - form2.Left <= SNAP_DISTANCE)) &&
                ((this.Top >= form2.Top && Math.Abs(this.Top - form2.Top) <= SNAP_TOP_DISTANCE)  ||
                (this.Top <= form2.Top && Math.Abs(form2.Top - this.Top) <= SNAP_TOP_DISTANCE)))
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


        ChromeDriver driver = null;
        private async Task<bool> StartUp()
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
        public async Task<List<dynamic>> Get_GPU_Data_From_Gminer(string ipAddress, int port)
        {
            string url = "http://" + ipAddress + ":" + port.ToString();
            List<dynamic> gpuDataList = new List<dynamic>();

            try
            {
                var ready = await StartUp();

                driver.Navigate().GoToUrl(url);

                var exists = WaitUntilElementExists(10, "tagname", "body");
                

                // find table rows with GPU data
                var gpuRows = driver.FindElements(By.CssSelector("#device_stat tr:not(#total)"));

                // iterate over gpu rows
                foreach (var row in gpuRows)
                {
                    // create dynamic object to store gpu data
                    dynamic gpuData = new System.Dynamic.ExpandoObject();

                    // get gpu data from table cells
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

                        gpuData.Id = id;
                        gpuData.Fan = fan;
                        gpuData.Temperature = cells[3].Text;
                        gpuData.Speed = cells[4].Text; 
                        gpuData.Shares = cells[5].Text;
                        gpuData.Core = core;
                        gpuData.Memory = memory;
                        gpuData.Power = power;
                        gpuData.Efficiency = cells[9].Text;

                        // add gpu data to list
                        gpuDataList.Add(gpuData);
                    }
                }


                return gpuDataList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to " + url + ": " + ex.Message);
                return null;
            }
        }

        public void Display_GPU_Data_From_Gminer(List<dynamic> devices)
        {
            double gpuPowerTotal = 0;
            double mhsAvTotal = 0;
            double mhsAvTotal2 = 0;
            int acceptedTotal = 0;
            int rejectedTotal = 0;
            int deviceIndex = 0;
            int xPos = 10;
            int yPos = 0;
            foreach (dynamic device in devices)
            {
                if (device.Shares == "Shares")
                    continue;

                // MHS
                string[] parts = device.Speed.ToString().Split('+');
                var first = parts[0].Substring(0, parts[0].LastIndexOf(' '));
                float firstNumber = float.Parse(first.Split()[0]);
                var sec = parts[1].Substring(1, parts[1].LastIndexOf(' '));
                float secondNumber = float.Parse(sec.Split()[0]);

                // Get/create gpu panel
                string gpu = device.Id.ToString();
                var gpu_panel = Get_Gpu_Panel(gpu);
                gpu_panel.Location = new Point(xPos, yPos);
                xPos += gpu_panel.Width + 10;

                Label gpu_label = (Label)gpu_panel.Controls.Find("GPU", true).First();
                gpu_label.Text = gpu;

                Label enabled_label = (Label)gpu_panel.Controls.Find("Enabled", true).First();
                string enabled = "Y";
                // Only show disabled
                if (enabled == "N")
                {
                    enabled_label.Visible = true;
                    enabled_label.Text = "Disabled";
                }
                else
                    enabled_label.Visible = false;

                PictureBox status_label = (PictureBox)gpu_panel.Controls.Find("Online_Picture_Box", true).First();
                if (firstNumber > 0 || secondNumber > 0)
                    status_label.Image = Resources.online_btn;
                else
                    status_label.Image = Resources.offline_btn;

                // Get Temps
                string[] temps = device.Temperature.Split('/');
                temps[0] = temps[0].Replace('C', ' ').TrimStart().TrimEnd();
                if (temps.Length > 1)
                    temps[1] = temps[1].Replace('C', ' ').TrimStart().TrimEnd();


                Label temperature_label = (Label)gpu_panel.Controls.Find("Temperature", true).First();
                temperature_label.Text = temps[0];

                Label fan_percent_label = (Label)gpu_panel.Controls.Find("Fan_Percent", true).First();
                string fan_percent = device.Fan.ToString();
                fan_percent_label.Text = fan_percent;

                Label gpu_clock_label = (Label)gpu_panel.Controls.Find("GPU_Clock", true).First();
                string gpu_clock = device.Core.ToString();
                gpu_clock_label.Text = gpu_clock;

                Label memory_clock_label = (Label)gpu_panel.Controls.Find("Memory_Clock", true).First();
                string memory_clock = device.Memory.ToString();
                memory_clock_label.Text = memory_clock;

                Label gpu_voltage_label = (Label)gpu_panel.Controls.Find("GPU_Volts", true).First();
                gpu_voltage_label.Visible = false;
                Label volts_label = (Label)gpu_panel.Controls.Find("Volts_Label", true).First();
                volts_label.Visible = false;

                Label mhs_avg_label = (Label)gpu_panel.Controls.Find("MHS_av", true).First();
                mhs_avg_label.Text = firstNumber.ToString();
                mhsAvTotal += firstNumber;

                dynamic shares = new System.Dynamic.ExpandoObject();
                shares.Algo1 = new System.Dynamic.ExpandoObject();
                shares.Algo2 = new System.Dynamic.ExpandoObject();

                var share_parts = device.Shares.Split('+');
                var Algo1 = share_parts[0].Trim().Split('/');
                var Algo2 = share_parts[1].Trim().Split('/');

                shares.Algo1.Accepted = int.Parse(Algo1[0].Trim());
                shares.Algo1.Rejected = int.Parse(Algo1[1].Trim());
                shares.Algo2.Accepted = int.Parse(Algo2[0].Trim());
                shares.Algo2.Rejected = int.Parse(Algo2[1].Trim());

                Label accepted_label = (Label)gpu_panel.Controls.Find("Accepted", true).First();
                accepted_label.Text = shares.Algo1.Accepted.ToString();
                acceptedTotal += shares.Algo1.Accepted;

                Label rejected_label = (Label)gpu_panel.Controls.Find("Rejected", true).First();
                string rejected = shares.ToString();
                rejected_label.Text = shares.Algo1.Rejected.ToString();
                rejectedTotal += shares.Algo1.Rejected;

                Label hardware_errors_label = (Label)gpu_panel.Controls.Find("Hardware_Errors", true).First();
                PictureBox hardware_errors_picture = (PictureBox)gpu_panel.Controls.Find("Hardware_Errors_Picture", true).First();
                string hardware_errors = "0";
                int hw_errors = int.Parse(hardware_errors);
                if (hw_errors > 0)
                {
                    hardware_errors_label.Visible = true;
                    hardware_errors_picture.Visible = true;
                    hardware_errors_label.Text = hardware_errors;
                }
                else
                {
                    hardware_errors_label.Visible = false;
                    hardware_errors_picture.Visible = false;
                }

                Label intensity_label = (Label)gpu_panel.Controls.Find("Intensity", true).First();
                string intensity = device.Efficiency.ToString();
                intensity_label.Text = intensity;
                Label efficiency_label = (Label)gpu_panel.Controls.Find("Intensity_Label", true).First();
                efficiency_label.Text = "Efficiency";

                Label last_share_time_label = (Label)gpu_panel.Controls.Find("Last_Share_Time", true).First();
                last_share_time_label.Visible = false;
                Label last_share_label = (Label)gpu_panel.Controls.Find("Last_Share_Label", true).First();
                last_share_label.Visible = false;

                Label temperature_mem_label = (Label)gpu_panel.Controls.Find("TemperatureMem", true).First();
                if (temps.Length > 1)
                    temperature_mem_label.Text = temps[1];
                else
                    temperature_mem_label.Text = "";

                Label gpu_power_label = (Label)gpu_panel.Controls.Find("GPU_Power", true).First();
                string gpu_power = device.Power.ToString();
                gpu_power_label.Text = gpu_power;
                gpuPowerTotal += int.Parse(gpu_power);

                // Algo 2
                Label mhs_avg_label2 = (Label)gpu_panel.Controls.Find("MHS_av2", true).First();
                mhs_avg_label2.Text = secondNumber.ToString();
                mhsAvTotal2 += secondNumber;


                // Show Totals
                Total_Mhs.Text = mhsAvTotal.ToString();
                Total_Mhs2.Text = mhsAvTotal2.ToString();
                Total_Power.Text = gpuPowerTotal.ToString();
                Total_Accepted.Text = acceptedTotal.ToString();
                Total_Rejected.Text = rejectedTotal.ToString();

                // Update line chart
                Chart Gpu_Chart_label = (Chart)gpu_panel.Controls.Find("Gpu_Chart", true).First();
                DataPoint mhs_avg_dp = new DataPoint(DateTime.Now.ToOADate(), firstNumber);
                DataPoint mhs_avg2_dp = new DataPoint(DateTime.Now.ToOADate(), secondNumber);
                DataPoint temperature_dp = new DataPoint(DateTime.Now.ToOADate(), double.Parse(temps[0]));

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

                Stats_Panel.Controls.Add(gpu_panel);
                deviceIndex++;
            }
        }

    }
}
