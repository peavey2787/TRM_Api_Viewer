using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using TRM_Api_Viewer.Properties;
using TextBox = System.Windows.Forms.TextBox;

namespace TRM_Api_Viewer
{
    public class ClientManager
    {
        private string _Other_Response = "";
        private Dictionary<string, Client> _Clients = new Dictionary<string, Client>();        
        public Dictionary<string, Dictionary<string, MinerStats>> Miner_Stats = new Dictionary<string, Dictionary<string, MinerStats>>();
        public event EventHandler<string> New_View_Miner_Event;
        private TextBox _View_Miner_TextBox;
        private string _Response = "";
        // Method to raise the custom event
        public void Raise_View_Miner_Event(string data)
        {
            // Check if there are any subscribers to the event
            if (New_View_Miner_Event != null)
            {
                // Raise the event by invoking all subscribers and passing the event arguments
                New_View_Miner_Event(this, data);
            }
        }
        public ClientManager() 
        {
            this.New_View_Miner_Event += Trigger_New_View_Miner_Event;
        }

        private void Trigger_New_View_Miner_Event(object sender, string output)
        {
            if(_View_Miner_TextBox != null) 
                _View_Miner_TextBox.AppendTexts(Environment.NewLine + output);
        }

        #region Add/Remove/Shutdown Clients
        public bool Add_Client(string ip, int port)
        {
            // If this ip isn't a client already, add it
            if (!_Clients.ContainsKey(ip))
            { 
                var client = new Client(ip, port);
                if (client != null && client.Is_Connected())
                {
                    _Clients.Add(ip, client);
                    Task.Run(() => { Read_Stream(ip); });
                    return true;
                }
            }
            else
            {
                // If this ip is already added, confirm its still connected
                _Clients.TryGetValue(ip, out var client);
                if(client != null && client.Is_Connected())
                    return true;
            }
            return false;
        }
        public void Remove_Client(string ip)
        {
            if (_Clients.ContainsKey(ip))
                _Clients.Remove(ip);
        }
        public void Shutdown_Clients()
        {
            foreach (var client in _Clients.Values)
                client.Dispose();            
        }
        #endregion

        #region REST
        public bool Client_is_Online (string ip)
        {
            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("ping");

            while (!_Response.StartsWith("pong:")) {; }
            var resp = Get_Response("pong:");
            if (bool.TryParse(resp, out var online))
                return online;

            return false;
        }
        public int Start_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("start/" + json);
            
            var resp = Get_Response("proc_id:");
            if (int.TryParse(resp, out int proc_id))
                return proc_id;
            
            return -1;
        }
        public bool Stop_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("stop/" + json);

            var resp = Get_Response("stopping_miner:");
            if (bool.TryParse(resp, out var stopped))
                return stopped;

            return false;
        }
        public bool Restart_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("restart/" + json);

            var resp = Get_Response("restarting_miner:");
            if (bool.TryParse(resp, out var stopped))
                return stopped;

            return false;
        }
        /// <summary>
        /// Get all running miners. Process Name / Process Id
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Dictionary<string, int> Get_Running_Miners(string ip)
        {
            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("running_miners");
            
            var resp = Get_Response("running_miners:");
            var miners = JsonConvert.DeserializeObject<Dictionary<string, int>>(resp);
            return miners;
        }
        public List<string> Get_Installed_Miners(string ip)
        {
            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("installed_miners");

            var resp = Get_Response("installed_miners:");
            var miners = JsonConvert.DeserializeObject<List<string>>(resp);
            return miners;
        }
        public bool Download_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("download_miner/" + json);
            
            var resp = Get_Response("download_miner:");
            if (bool.TryParse(resp, out var downloaded))
                return downloaded;
            return false;
        }
        public bool Delete_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("delete_miner/" + json);
                        
            var resp = Get_Response("delete_miner:");
            if (bool.TryParse(resp, out var removed))
                return removed;
            return false;
        }
        public void View_Miner(string ip, MinerSettings settings, ref TextBox textbox)
        {
            _View_Miner_TextBox = textbox;
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("view_miner/" + json);
        }
        public void Stop_Viewing_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("stop_viewing_miner/" + json);
        }
        public MinerStats Get_Stats(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("get_stats/" + json);
                        
            var resp = Get_Response("get_stats:");
            var miner_stats = JsonConvert.DeserializeObject<MinerStats>(resp);
            return miner_stats;
        }
        public List<string> Get_Devices(string ip)
        {
            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("list_devices");
                        
            var resp = Get_Response("list_devices:");
            var devices = JsonConvert.DeserializeObject<List<string>>(resp);
            return devices;
        }
        public bool Add_Miner_Setting(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);

            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("add_minerSettings/" + json);

            while (!_Response.StartsWith("add_minerSettings:")) {; }
            var resp = _Response.Replace("add_minerSettings:", "").TrimStart().TrimEnd();
            if (bool.TryParse(resp, out var added))
                return added;
            return false;
        }
        public bool Remove_Miner_Setting(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            
            if (_Clients.TryGetValue(ip, out Client client))
                client.Send("remove_minerSettings/" + json);

            var resp = Get_Response("remove_minerSettings:");
            if (bool.TryParse(resp, out var removed))
                return removed;
            return false;
        }
        private string Get_Response(string resp_tag)
        {
            int retries = 20;
            while (retries > 0)
            {
                if (_Response.StartsWith(resp_tag))
                {
                 
                    string resp = _Response.Replace(resp_tag, "").TrimStart().TrimEnd();
                    _Response = "";
                    return resp;
                }
                retries--;
                Thread.Sleep(50);
            }
            return "";
        }
        private async void Read_Stream(string ip)
        {
            Client client = null;
            do
            {
                string response = "";
                if (_Clients.TryGetValue(ip, out client))
                    response = await client.Receive();

                if (response.StartsWith("view_miner:"))
                {
                    response = response.Replace("view_miner:", "").TrimStart().TrimEnd();
                    this.Raise_View_Miner_Event(response);
                }
                else
                    _Response = response;
            } while (client.Is_Connected());
        }
        #endregion
    }
    class Client : IDisposable
    {
        private TcpClient client;
        private NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;
        public string IpAddress;
        public int Port;

        public delegate void MessageReceivedHandler(object sender, string message);
        public event MessageReceivedHandler MessageReceived;

        public Client(string ipAddress, int port)
        {
            IpAddress = ipAddress;
            Port = port;
            Connect_To_Client();
        }
        public bool Connect_To_Client()
        {
            client = new TcpClient();

            try
            {
                client.Connect(IpAddress, Port);
                stream = client.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8);
                reader = new StreamReader(stream, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public bool Is_Connected()
        {
            if (client != null)
                return client.Connected;
            return false;
        }
        public void Send(string message)
        {
            int retries = 3;
            if (stream == null) return;
            while (retries > 0)
            {
                if (!client.Connected) Connect_To_Client();
                try 
                { 
                    writer.WriteLine(message); writer.Flush();
                    break;
                }
                catch
                {
                }
                retries--;
                Thread.Sleep(500);
            }
        }
        public async Task<string> Receive()
        {            
            if (stream == null || client == null || !client.Connected) 
                Connect_To_Client();
            try
            {
                string message = await reader.ReadLineAsync();
                if (message == null) return "";
                message = message.TrimStart('\uFEFF'); // Remove the BOM
                MessageReceived?.Invoke(this, message);
                return message;
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains(""))
                {

                }
            }
            return "";
        }
        public void Dispose()
        {
            if (client != null)
            {
                try { client.Close(); } catch { }
                client = null;
            }
        }
    }
}
