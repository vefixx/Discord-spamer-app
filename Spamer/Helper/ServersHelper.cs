using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Helper
{
    public class ServersHelper
    {
        public string[] ServersList()
        {
            CfgHelper cfg = new CfgHelper();
            string path = cfg.GetPaths()["txt_path"];

            return File.ReadAllLines(path);
        }
    }
}
