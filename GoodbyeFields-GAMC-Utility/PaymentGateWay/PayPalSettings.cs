using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeFields_GAMC_Utility.PaymentGateWay
{
    public class PayPalSettings: IPayPalSettings
    {
        public string mode { get; set; }
        public string connectionTimeout { get; set; }
        public string requestRetries { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
       
        private List<PayPalSettings> list = new List<PayPalSettings>
            {
                new PayPalSettings { mode = "sandbox", connectionTimeout="360000", requestRetries="1", 
                    clientId="AU5360KW4Ay1fHIgvC2Kj_WkWEVsGdC17IK8uH_lgjHNjZMq9NQQN9oxrX42PSiYdvZxMKaU2ZYeS7un", 
                    clientSecret="EB-rfCFpUDBvj95BKD-E0mds67zVozL8hhEypx9Y9gkUSK-Z5YZjQFtMaBX2Pvh5ruQe9tc0ZmFseiGa"}
            };
            
        public string CreateSettings()
        {
            var configsetting = list;
            return configsetting.ToString();
        }

    }
}
