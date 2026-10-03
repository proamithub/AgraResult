
using RModel;
using Microsoft.Extensions.Configuration;

namespace DbrauResultUI.PaymentGateway
{
    public class PayPal
    {
        IConfiguration config;
        public PayPal(IConfiguration _config)
        {
            config = _config;
        }
        public string Url
        {
            get
            {
                return "";//WebConfigSetting.PaypalUrl;
            }
        }

        public string SuccessUrl
        {
            get
            {
                return "";//WebConfigSetting.PaypalSuccessUrl;
            }
        }

        public string FailUrl
        {
            get
            {
                return "";//WebConfigSetting.PaypalFailUrl;
            }
        }

        public string BusinessEmail
        {
            get
            {
                return "";//WebConfigSetting.PaypalEmail;
            }
        }
    }
}