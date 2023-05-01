using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TRM_Api_Viewer
{
    public partial class EditSettings : Form
    {
        public MinerSettings miner_settings;
        Worker Worker = new Worker();
        List<string> installed_miners = new List<string>();
        ClientManager client_manager;
        public EditSettings(ref ClientManager client_manager, MinerSettings settings, Worker worker)
        {
            InitializeComponent();
            this.client_manager = client_manager;
            this.miner_settings = settings == null ?  new MinerSettings() : settings;
            this.Worker = worker;
        }
        private async void EditSettings_Load(object sender, EventArgs e)
        {
            // Populate Coin comboboxes 
            foreach (var item in miner_settings.algorithmToCoin)
            {
                Coin1_ComboBox.Items.Add(item.Value);
                Coin2_ComboBox.Items.Add(item.Value);
                Coin3_ComboBox.Items.Add(item.Value);
            }

            //await Task.Run(() =>
            //{
                // Populate installed miners
                installed_miners = client_manager.Get_Installed_Miners(Worker.IP);
                if (installed_miners != null)
                    foreach (var miner in installed_miners)                    
                        Installed_Miners_ComboBox.AddItem(miner);   
            //});

            // Populate settings to edit
            if (miner_settings == null) { return; }

            Settings_Name_TextBox.Text = miner_settings.Display_Name;
            Active_CheckBox.Checked = miner_settings.Active;
            Installed_Miners_ComboBox.Text = miner_settings.Name;
            Run_As_CheckBox.Checked = miner_settings.RunAs;
            Version_TextBox.Text = miner_settings.Version;
            URL_TextBox.Text = miner_settings.Download_URL;
            Wallet1_TextBox.Text = miner_settings.Wallet1;
            Wallet2_TextBox.Text = miner_settings.Wallet2;
            Wallet3_TextBox.Text = miner_settings.Wallet3;
            Coin1_ComboBox.Text = miner_settings.Coin1;
            Coin2_ComboBox.Text = miner_settings.Coin2;
            Coin3_ComboBox.Text = miner_settings.Coin3;
            Algo1_TextBox.Text = miner_settings.Algo1;
            Algo2_TextBox.Text = miner_settings.Algo2;
            Algo3_TextBox.Text = miner_settings.Algo3;
            Pool1_TextBox.Text = miner_settings.Pool1;
            Pool2_TextBox.Text = miner_settings.Pool2;
            Pool3_TextBox.Text = miner_settings.Pool3;
            Port1_TextBox.Text = miner_settings.Port1.ToString();
            Port2_TextBox.Text = miner_settings.Port2.ToString();
            Port3_TextBox.Text = miner_settings.Port3.ToString();
            SSL1_CheckBox.Checked = miner_settings.SSL1;
            SSL2_CheckBox.Checked = miner_settings.SSL2;
            SSL3_CheckBox.Checked = miner_settings.SSL3;

            Arguments_TextBox.Text = miner_settings.Arguments_String;
        }
        bool close = true;
        private void Save_Button_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Settings_Name_TextBox.Text))
            {
                MessageBox.Show("Settings must have a name");
                close = false;
                return;
            }
            if (String.IsNullOrWhiteSpace(Arguments_TextBox.Text))
            {
                MessageBox.Show("The .bat file textbox must not be empty");
                close = false;
                return;
            }

            miner_settings.Display_Name = Settings_Name_TextBox.Text;
            miner_settings.Active = Active_CheckBox.Checked;
            miner_settings.Name = Installed_Miners_ComboBox.Text;
            miner_settings.RunAs = Run_As_CheckBox.Checked;
            miner_settings.Version = Version_TextBox.Text;
            miner_settings.Download_URL = URL_TextBox.Text;
            miner_settings.Wallet1 = Wallet1_TextBox.Text;
            miner_settings.Wallet2 = Wallet2_TextBox.Text;
            miner_settings.Wallet3 = Wallet3_TextBox.Text;
            miner_settings.Coin1 = Coin1_ComboBox.Text;
            miner_settings.Coin2 = Coin2_ComboBox.Text;
            miner_settings.Coin3 = Coin3_ComboBox.Text;
            miner_settings.Algo1 = Algo1_TextBox.Text;
            miner_settings.Algo2 = Algo2_TextBox.Text;
            miner_settings.Algo3 = Algo3_TextBox.Text;
            miner_settings.Pool1 = Pool1_TextBox.Text;
            miner_settings.Pool2 = Pool2_TextBox.Text;
            miner_settings.Pool3 = Pool3_TextBox.Text;
            miner_settings.Port1 = int.TryParse(Port1_TextBox.Text, out int port1) ? port1 : 0;
            miner_settings.Port2 = int.TryParse(Port2_TextBox.Text, out int port2) ? port2 : 0;
            miner_settings.Port3 = int.TryParse(Port3_TextBox.Text, out int port3) ? port3 : 0;
            miner_settings.SSL1 = SSL1_CheckBox.Checked;
            miner_settings.SSL2 = SSL2_CheckBox.Checked;
            miner_settings.SSL3 = SSL3_CheckBox.Checked;

            miner_settings.Arguments_String = Arguments_TextBox.Text;            

            // Update worker/server
            bool added = client_manager.Add_Miner_Setting(Worker.IP, miner_settings);
            if(added)
                close = true;
            else
            {
                close = false;
                MessageBox.Show("Miner settings were not saved");
            }
        }
        private void Download_Button_Click(object sender, EventArgs e)
        {
            bool downloaded = client_manager.Download_Miner(Worker.IP, miner_settings);
            if (downloaded)
            {
                MessageBox.Show("Miner was downloaded successfully");
            }
            else
                MessageBox.Show("Miner was not downloaded, please check the URL and try again");
        }
        private void Generate_Button_Click(object sender, EventArgs e)
        {
            Arguments_TextBox.Text = miner_settings.GenerateArguments();
        }



        private void Coin1_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Algo1_TextBox.Text = miner_settings.ConvertCoinToAlgorithm(Coin1_ComboBox.Text);
        }
        private void Coin2_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Algo2_TextBox.Text = miner_settings.ConvertCoinToAlgorithm(Coin2_ComboBox.Text);
        }
        private void Coin3_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Algo3_TextBox.Text = miner_settings.ConvertCoinToAlgorithm(Coin3_ComboBox.Text);
        }
        private void Installed_Miners_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var miner = installed_miners.Find(i => i.StartsWith(Installed_Miners_ComboBox.Text));
            var miner_version = "";
            int firstDigitIndex = -1;
            for (int i = 0; i < miner.Length; i++)
            {
                if (char.IsDigit(miner[i]))
                {
                    firstDigitIndex = i;
                    break;
                }
            }
            miner_version = miner.Substring(firstDigitIndex);
            Version_TextBox.Text = miner_version;            
        }

        private void EditSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!close)
                e.Cancel = true;
        }
    }
}
