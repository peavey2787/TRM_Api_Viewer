using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TRM_Api_Viewer
{
    public class Worker
    {
        public string Name { get; set; } = "";
        public string IP { get; set; } = "";
        public bool Online { get; set; } = false;
        public List<MinerSettings> Settings { get; set; } = new List<MinerSettings>();
        public MinerSettings Get_Active_Setting()
        {
            var active_setting = new MinerSettings();
            foreach (var setting in Settings)
                if (setting.Active)
                    active_setting = setting;
            return active_setting;
        }
        public void Set_Active_Setting(MinerSettings setting)
        {
            // Mark all settings as inactive except the given
            for(int i =0; i < Settings.Count; i++)
            {
                Settings[i].Active = false;

                if (Settings[i].Display_Name == setting.Display_Name)
                    Settings[i].Active = true;
            }
        }
        public MinerSettings Get_Setting(string display_name)
        {
           return Settings.Find(s => s.Display_Name.Equals(display_name));
        }
        public List<MinerSettings> Get_Miner_Settings()
        {
            return Settings;
        }
        public void Add_Setting(MinerSettings setting)
        {
            // Overwrite existing setting
            for (int i = 0; i < Settings.Count; i++)
            {
                if (Settings[i].Display_Name == setting.Display_Name)
                {
                    Settings[i] = setting;
                    return;
                }
            }
            // Add new setting
            Settings.Add(setting);
        }
        public void Remove_Setting(MinerSettings setting)
        {
            for (int i = 0; i < Settings.Count; i++)
            {
                if (Settings[i].Display_Name == setting.Display_Name)
                {
                    Settings.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
