using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Roots
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Root_cfg
    {
        public string config_name { get; set; }
        public string py_path { get; set; }
        public string cfg_path { get; set; }
        public string acc_path { get; set; }
        public string txt_path { get; set; }
        public string log_path { get; set; }
        public int valid_accounts { get; set; }
        public int sent_messages { get; set; }
        public int add_servers { get; set; }
        public string config_use { get; set; }
        public bool script_active { get; set; }
        public string images { get; set; }
        public int delay { get; set; }
    }


}
