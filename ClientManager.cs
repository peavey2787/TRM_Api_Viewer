using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TRM_Api_Viewer
{
    public class ClientManager
    {
        private Dictionary<string, Client> _Clients = new Dictionary<string, Client>();
        public MinerStats Miner_Stats = new MinerStats();
        public ClientManager() { }

        #region Add/Remove Clients
        public void Add_Client(string ip, int port)
        {
            // If this ip isn't a client already, add it
            if (!_Clients.ContainsKey(ip))
            { 
                var client = new Client(ip, port);
                if(client != null && client.Is_Connected())
                    _Clients.Add(ip, client);
            }
        }
        public void Remove_Client(string ip)
        {
            if (_Clients.ContainsKey(ip))
                _Clients.Remove(ip);
        }
        #endregion

        #region REST
        public bool Client_is_Online (string ip)
        {
            string response = Send_Command(ip, "ping");
            if (response == "pong")
                return true;
            return false;
        }

        public int Start_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            var resp = Send_Command(ip, "start/" + json);
            if (resp != null)
            {
                resp = resp.Replace("proc_id:", "").TrimStart().TrimEnd();
                if (int.TryParse(resp, out int proc_id))
                {
                    return proc_id;                    
                }
            }
            return -1;
        }
        public bool Stop_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            var resp = Send_Command(ip, "stop/" + json);
            if (resp != null && resp == "stopping miner")
                return true;
            return false;
        }
        public bool Restart_Miner(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            var resp = Send_Command(ip, "restart/" + json);
            if (resp != null && resp == "restarting miner")
                return true;
            return false;
        }
        /// <summary>
        /// Get all running miners. Process Name / Process Id
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public Dictionary<string, int> Get_Running_Miners(string ip, MinerSettings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            var resp = Send_Command(ip, "running_miners/" + json);
            if (resp != null && resp.StartsWith("running_miners:"))
            {
                resp = resp.Replace("proc_id:", "").TrimStart().TrimEnd();
                var miners = JsonConvert.DeserializeObject<Dictionary<string, int>>(resp);
                return miners;
            }
            return null;
        }
        public List<string> Get_Installed_Miners(string ip)
        {
            var resp = Send_Command(ip, "installed_miners");
            if (resp != null && resp.StartsWith("installed_miners:"))
            {
                resp = resp.Replace("installed_miners:", "").TrimStart().TrimEnd();
                var miners = JsonConvert.DeserializeObject<List<string>>(resp);
                return miners;
            }
            return null;
        }
        public bool Download_Miner(string ip, MinerSettings settings)
        {
            var resp = Send_Command(ip, "download_miner");
            if (resp != null && resp.StartsWith("download_miner:"))
            {
                resp = resp.Replace("download_miner:", "").TrimStart().TrimEnd();
                if(bool.TryParse(resp, out var downloaded))
                    return downloaded;
            }
            return false;
        }
        public bool Delete_Miner(string ip, MinerSettings settings)
        {
            var resp = Send_Command(ip, "delete_miner");
            if (resp != null && resp.StartsWith("delete_miner:"))
            {
                resp = resp.Replace("delete_miner:", "").TrimStart().TrimEnd();
                if (bool.TryParse(resp, out var downloaded))
                    return downloaded;
            }
            return false;
        }
        public void View_Miner(string ip, MinerSettings settings)
        {
            var resp = Send_Command(ip, "view_miner");
            Client client = null;
            do
            {
                if (resp != null && resp.StartsWith("view_miner:"))
                {
                    // Get miner output
                    resp = resp.Replace("view_miner:", "").TrimStart().TrimEnd();

                    // Get json stats
                    if (resp != null && resp.StartsWith("json:"))
                    {
                        var stats = resp.Replace("json:", "").TrimStart().TrimEnd();
                        Miner_Stats = JsonConvert.DeserializeObject<MinerStats>(stats);
                    }
                }
                if (_Clients.TryGetValue(ip, out client))
                {
                    client.Receive();
                }
            } while (client.Is_Connected());
        }
        public List<string> Get_Devices(string ip)
        {
            var resp = Send_Command(ip, "list_devices");
            if (resp != null && resp.StartsWith("list_devices:"))
            {
                resp = resp.Replace("list_devices:", "").TrimStart().TrimEnd();
                var devices = JsonConvert.DeserializeObject<List<string>>(resp);
                return devices;
            }
            return null;
        }

        private string Send_Command(string ip, string command)
        {
            // Get the corresponding client and send command
            string response = "";
            if (_Clients.TryGetValue(ip, out Client client))
            {
                client.Send(command);
                response = client.Receive();
            }
            return response;
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
                //Send("ping");
                //if(Receive() == "pong")
                    return true;
            }
            catch (Exception ex)
            {                

            }
            return false;
        }
        public bool Is_Connected()
        {
            try { return client.Connected; } 
            catch { return false; }
        }
        public void Send(string message)
        {
            if (stream == null) return;
            if (!client.Connected) Connect_To_Client();
            try { writer.WriteLine(message); writer.Flush(); } 
            catch { }
        }
        public string Receive()
        {
            if (stream == null) return "";
            if (!client.Connected) Connect_To_Client();
            try
            {
                string message = reader.ReadLine();
                message = message.TrimStart('\uFEFF'); // Remove the BOM
                MessageReceived?.Invoke(this, message);
                return message;

            }
            catch (SocketException exc)
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
