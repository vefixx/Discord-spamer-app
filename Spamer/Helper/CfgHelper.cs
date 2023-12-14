using Newtonsoft.Json;
using Spamer.Roots;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Spamer.Helper
{
    public class CfgHelper
    {
        string cfg_path = "conf/conf.cfg";

        public Dictionary<string,string> GetPaths()
        {
            Dictionary<string, string> paths = new Dictionary<string, string>();

            string json = File.ReadAllText(cfg_path);
            Roots.Root_cfg cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);
            paths.Add("py_path", cfg.py_path);
            paths.Add("cfg_path", cfg.cfg_path);
            paths.Add("acc_path", cfg.acc_path);
            paths.Add("txt_path", cfg.txt_path);
            paths.Add("log_path", cfg.log_path);

            return paths;
        }

        public Dictionary<string, object> GetCFGInfo()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            string json = File.ReadAllText(cfg_path);
            Roots.Root_cfg cfg = JsonConvert.DeserializeObject<Roots.Root_cfg>(json);

            data.Add("config_name", cfg.config_name);
            data.Add("valid_accounts", cfg.valid_accounts);
            data.Add("sent_messages", cfg.sent_messages);
            data.Add("add_servers", cfg.add_servers);
            data.Add("config_use", cfg.config_use);
            data.Add("script_active", cfg.script_active);
            data.Add("images", cfg.images);
            data.Add("delay", cfg.delay);

            return data;
        }

        public void UpdateScriptStatus()
        {
            bool new_status;
            bool old_status = bool.Parse(GetCFGInfo()["script_active"].ToString());

            if (old_status)
                new_status = false;
            else
                new_status = true;
            

            string json = File.ReadAllText(cfg_path);
            Root_cfg cfg = JsonConvert.DeserializeObject<Root_cfg>(json);

            cfg.script_active = new_status;

            string updatedJson = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            File.WriteAllText(cfg_path, updatedJson);
        }

        public void UpdateImages(string new_images)
        {
            string json = File.ReadAllText(cfg_path);
            Root_cfg cfg = JsonConvert.DeserializeObject<Root_cfg>(json);

            cfg.images = new_images;

            string updatedJson = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            File.WriteAllText(cfg_path, updatedJson);
        }
    }
}
