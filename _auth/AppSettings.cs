using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth.api
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string DefaultKey { get;set ;}
        public string SendGridApiKey { get; set; }
        public string ApiKeyForExternal { get; set; }
    }
}
