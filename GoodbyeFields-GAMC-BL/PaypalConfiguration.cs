using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Options;
using GoodbyeFields_GAMC_Model;

namespace GoodbyeFields_GAMC_BL
{
    public class PaypalConfiguration
    {
        public readonly IConfiguration _configuration;

        //Variables for storing the clientID and clientSecret key
        public static readonly string Mode;
        public static readonly string ConnectionTimeout;
        public static readonly string RequestRetries;
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        //Constructor
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }
        public IConfiguration Configuration { get; }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            var configvalue = new Dictionary<string, string>();
            configvalue.Add("clientId", "ARdke0T_M7NpLxpIgff40Rui9aJuvWa4OSwQFxIAKU1wYoDNRTqAv76hBq5JGXyWPiHWFoFbDbDQuqfq");
            configvalue.Add("clientSecret", "ELT95XyXSWW9xRx5YqdUFIwFletEmY8p0JAMSEaNjT52jB_qOSFyWsvAhos6HM5XfTQfdpiC6wyM5Xxe");
            configvalue.Add("mode", "sandbox");
            configvalue.Add("connectionTimeout", "360000");
            configvalue.Add("requestRetries", "1");
            return configvalue;
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal                
            string accessToken = new OAuthTokenCredential
        (ClientId, ClientSecret, GetConfig()).GetAccessToken();

            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}
