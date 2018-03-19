using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommonEntities
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }
        public string ClientName { get; set; }
        public int ExpiryInMinutes { get; set; }
    }
}
