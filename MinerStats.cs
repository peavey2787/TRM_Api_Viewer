using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace TRM_Api_Viewer
{
    public class MinerStats
    {
        public string Balance { get; set; }
        public string Shares { get; set; }
        public string Shares2 { get; set; }
        public string Uptime { get; set; }
        public string Electricity { get; set; }
        public string Efficiency { get; set; }
        public string Efficiency2 { get; set; }
        public string PoolHashrate { get; set; }
        public string PoolHashrate2 { get; set; }
        public List<Gpu> Gpus { get; set; }

        public MinerStats()
        {
            Gpus = new List<Gpu>();
        }
    }

    public class Gpu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WorkerName { get; set; }
        public bool Online { get; set; }
        public double Speed1 { get; set; }
        public double Speed2 { get; set; }
        public double Speed3 { get; set; }
        public int Temperature { get; set; }
        public int MemTemp { get; set; }
        public int FanPercent { get; set; }
        public double Power { get; set; }
        public int CoreClock { get; set; }
        public int MemClock { get; set; }
        public double CoreMv { get; set; }
        public int Intensity { get; set; }
        public string Efficiency { get; set; }
        public int Accepted1 { get; set; }
        public int Accepted2 { get; set; }
        public int Accepted3 { get; set; }
        public int Rejected1 { get; set; }
        public int Rejected2 { get; set; }
        public int Rejected3 { get; set; }
        public double LastShareTime { get; set; }
        public int HardwareErrors { get; set; }

        public static string GetMiningSpeedUnit(double miningSpeed)
        {
            if (miningSpeed < 1)
            {
                return "H/s"; // Hashes per second
            }
            else if (miningSpeed < 1000)
            {
                return "KH/s"; // Kilohashes per second
            }
            else if (miningSpeed < 1000000)
            {
                return "MH/s"; // Megahashes per second
            }
            else if (miningSpeed < 1000000000)
            {
                return "GH/s"; // Gigahashes per second
            }
            else if (miningSpeed < 1000000000000)
            {
                return "TH/s"; // Terahashes per second
            }
            else if (miningSpeed < 1000000000000000)
            {
                return "PH/s"; // Petahashes per second
            }
            else if (miningSpeed < 1000000000000000000)
            {
                return "EH/s"; // Exahashes per second
            }
            return "IDK";
        }
        public static double ConvertMiningSpeed(double miningSpeed, string targetUnit)
        {
            switch (targetUnit)
            {
                case "H/s":
                    return miningSpeed;
                case "KH/s":
                    return miningSpeed / 1000;
                case "MH/s":
                    return miningSpeed / 1000000;
                case "GH/s":
                    return miningSpeed / 1000000000;
                case "TH/s":
                    return miningSpeed / 1000000000000;
                case "PH/s":
                    return miningSpeed / 1000000000000000;
                case "EH/s":
                    return miningSpeed / 1000000000000000000;
                default:
                    throw new ArgumentException("Invalid target unit: " + targetUnit);
            }
        }
        public static string Get_Human_Readable_Date_Time(double unix_time)
        {
            DateTime unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime humanTime = unixTime.AddSeconds(unix_time).ToLocalTime();
            return humanTime.ToString("MM/dd hh:mm:ss tt");
        }
    }
}
