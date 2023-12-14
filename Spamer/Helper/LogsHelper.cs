using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Helper
{
    public class LogsHelper
    {
        string cfg_path = "conf/conf.cfg";
        public void AddLog(string TYPE, string MESSAGE, string STATUS)
        {

            string json = File.ReadAllText(cfg_path);
            Roots.Root_cfg cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);
            string logs_path = cfg.log_path;

            if (File.Exists(logs_path))
            {
                string allText = File.ReadAllText(logs_path);
                File.WriteAllText(logs_path, allText + $"\n{TYPE} | {MESSAGE} | {STATUS} | {DateTime.Now}");
            }
            else
                return;
            
        }
    }
}
