using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TRM_Api_Viewer
{
    public class MinerSettings
    {
        public string Path { get; set; } = string.Empty;
        public bool RunAs { get; set; } = false;
        public int ProcessId { get; set; } = -1;
        public string Name { get; set; } = string.Empty;
        public string Display_Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Download_URL { get; set; } = string.Empty;
        public bool Active { get; set; } = false;
        public string Worker_Name { get; set; } = string.Empty;
        public string Wallet1 { get; set; } = string.Empty;
        public string Wallet2 { get; set; } = string.Empty;
        public string Wallet3 { get; set; } = string.Empty;
        public string Coin1 { get; set; } = string.Empty;
        public string Coin2 { get; set; } = string.Empty;
        public string Coin3 { get; set; } = string.Empty;
        public string Algo1 { get; set; } = string.Empty;
        public string Algo2 { get; set; } = string.Empty;
        public string Algo3 { get; set; } = string.Empty;
        public string Pool1 { get; set; } = string.Empty;
        public string Pool2 { get; set; } = string.Empty;
        public string Pool3 { get; set; } = string.Empty;
        public int Port1 { get; set; } = 0;
        public int Port2 { get; set; } = 0;
        public int Port3 { get; set; } = 0;
        public bool SSL1 { get; set; } = false;
        public bool SSL2 { get; set; } = false;
        public bool SSL3 { get; set; } = false;
        public Dictionary<string, string> Arguments_Dictionary { get; set; } = new Dictionary<string, string>();
        public string Arguments_String { get; set; } = string.Empty;

        public Dictionary<string, string> algorithmToCoin = new Dictionary<string, string>()
        {
            { "Etchash", "ETC" },
            { "Kawpow", "RVN" },
            { "Firopow", "FIRO" },
            { "Mtp_firopow", "FIRO" },
            { "Autolykos2", "ERGO" },
            { "Verthash", "VTC" },
            { "Mtp", "FIRO" },
            { "Cnr", "XMR" },
            { "Kheavyhash", "KAS" },
            { "Cortex", "CTXC" },
            { "Beamhash", "BEAM" },
            { "Equihash144_5", "ZELCASH" },
            { "Equihash125_4", "ZCASH" },
            { "Equihash210_9", "AION" },
            { "Cuckoo29", "AE" },
            { "Blake3", "ALPH" },
            { "SHA512256d", "RXD" },
            { "Octopus", "CFX" },
            { "NexaPow", "NEXA" },
            { "Ubqhash", "UBQ" },
            { "ZelHash", "FLUX" },
            { "sha512_256d_radiant", "RXD" },
            { "Zilliqa", "ZIL" }
        };
        public string GenerateArguments()
        {
            string args = Path + " ";

            if (Name.StartsWith("SRBMiner"))
            {
                args += "--algorithm " + Algo1 + " ";      
                
                args += "--pool " + Pool1 + ":" + Port1 + " ";

                if (SSL1)
                    args += "--tls true ";
                else
                    args += "--tls false ";

                args += "--wallet " + Wallet1 + "." + Worker_Name + " ";
            }        

            else if(Name.StartsWith("gminer"))
            {
                args += "--algo " + Algo1 + " ";

                args += "--server " + Pool1 + ":" + Port1 + " ";

                if (SSL1)
                    args += "--ssl 1 ";
                else
                    args += "--ssl 0 ";
                
                args += "--user " + Wallet1 + "." + Worker_Name + " ";
            }

            else if (Name.StartsWith("trex"))
            {
                args += "-a " + Algo1 + " ";

                if (String.IsNullOrWhiteSpace(Coin2) && String.IsNullOrWhiteSpace(Coin3))
                    args += "--coin " + Coin1 + " ";
                else if (!String.IsNullOrWhiteSpace(Coin2))
                    args += "--coin " + Coin1 + "+" + Coin2 + " ";

                if (SSL1)
                    args += "-o stratum+ssl://" + Pool1 + ":" + Port1 + " ";
                else
                    args += "-o stratum+tcp://" + Pool1 + ":" + Port1 + " ";
                
                args += "-u " + Wallet1 + "." + Worker_Name + " ";

                if(!String.IsNullOrWhiteSpace(Coin2))
                {
                    if(SSL2)
                        args += "--url2 stratum+ssl://" + Pool2 + ":" + Port2 + " ";
                    else
                        args += "--url2 stratum+tcp://" + Pool2 + ":" + Port2 + " ";

                    args += "--user2 " + Wallet2 + "." + Worker_Name + " ";
                }
            }

            else if (Name.StartsWith("teamredminer"))
            {
                args += "-a " + Algo1 + " ";

                if (SSL1)
                    args += "-o stratum+ssl://" + Pool1 + ":" + Port1 + " ";
                else
                    args += "-o stratum+tcp://" + Pool1 + ":" + Port1 + " ";

                args += "-u " + Wallet1 + "." + Worker_Name + " ";

                if (!String.IsNullOrWhiteSpace(Coin2) && Coin2 == "KAS")
                {
                    args += "--kas ";
                    if (SSL2)
                        args += "-o stratum+ssl://" + Pool2 + ":" + Port2 + " ";
                    else
                        args += "-o stratum+tcp://" + Pool2 + ":" + Port2 + " ";

                    args += "-u " + Wallet2 + "." + Worker_Name + " ";
                    args += "--kas_end ";
                }

                if (!String.IsNullOrWhiteSpace(Coin2) && Coin2 == "ZIL")
                {
                    args += "--zil ";
                    if (SSL2)
                        args += "-o stratum+ssl://" + Pool2 + ":" + Port2 + " ";
                    else
                        args += "-o stratum+tcp://" + Pool2 + ":" + Port2 + " ";

                    args += "-u " + Wallet2 + "." + Worker_Name + " ";
                    args += "--zil_end ";
                }
            }


            // Add additional arguments
            foreach (KeyValuePair<string, string> arg in Arguments_Dictionary)
            {
                args += arg.Key + " " + arg.Value + " ";
            }

            return args;
        }
        public void GetCoinFromArguments()
        {
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> arg in Arguments_Dictionary)
            {
                if (arg.Key == "algo")
                    Coin1 = ConvertAlgorithmToCoin(arg.Value);
                if (arg.Key == "kas")
                    Coin1 = "KAS";
            }
        }

        public string ConvertAlgorithmToCoin(string algorithm)
        {
            string coin = "";
            if (algorithmToCoin.TryGetValue(algorithm, out coin))
            {
                return coin;
            }
            return coin;
        }
        public string ConvertCoinToAlgorithm(string coin)
        {
            foreach (KeyValuePair<string, string> pair in algorithmToCoin)
            {
                if (pair.Value == coin)
                {
                    return pair.Key;
                }
            }
            return "";
        }
    }
}
