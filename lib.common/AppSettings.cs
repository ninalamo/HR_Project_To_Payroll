using System;

namespace lib.common
{
    public class AppSettings
    {
      
        public string Secret { get; set; }
        public string DefaultKey { get; set; }
        public string SendGridApiKey { get; set; }
        public string ApiKeyForExternal { get; set; }
      
    }
}
