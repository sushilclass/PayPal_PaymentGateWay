using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Model
{
    public class PayPalSettings
    {
        public string Mode { get; set; }
        public string ConnectionTimeout { get; set; }
        public string RequestRetries { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
