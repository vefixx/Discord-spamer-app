using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spamer.Roots
{
    public class Root_JsonUserData
    {
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
        public int flags { get; set; }
        public object banner { get; set; }
        public object accent_color { get; set; }
        public string global_name { get; set; }
        public object avatar_decoration_data { get; set; }
        public object banner_color { get; set; }
        public bool mfa_enabled { get; set; }
        public string locale { get; set; }
        public int premium_type { get; set; }
        public string email { get; set; }
        public bool verified { get; set; }
        public object phone { get; set; }

        public List<object> linked_users { get; set; }
        public string bio { get; set; }
        public List<object> authenticator_types { get; set; }
    }
}
