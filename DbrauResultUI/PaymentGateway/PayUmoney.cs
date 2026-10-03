

using RModel;

namespace DbrauResultUI.PaymentGateway
{
    public class PayUmoney
    {
        public string Url
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuUrl"];
            }
        }

        public string SuccessUrl
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuSuccessUrl"];
            }
        }

        public string InsSuccessUrl
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuInsSuccessUrl"];
            }
        }
        
        public string FailUrl
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuFailUrl"];
            }
        }
        public string InsFailUrl
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuInsFailUrl"];
            }
        }
        public string Key
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuKey"];
            }
        }

        public string Salt
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuSalt"];
            }
        }
        public string Hash_Seq
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuHash_seq"];
            }
        }        

        public string ServiceProvider
        {
            get
            {
                return ConfigurationHelper.Config["AppSettings:PayuServiceProvider"];
            }
        }
    }
}