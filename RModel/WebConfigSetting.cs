using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace RModel
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Config;
        public static void Initialize(IConfiguration Configuration)
        {
            Config = Configuration;
        }
    }
    public class WebConfigSetting
    {
        public static string BaseURL { get { return ConfigurationManager.AppSettings["baseUrl"].ToString(); } }

        public static string SmsUrl { get { return ConfigurationManager.AppSettings["SmsUrl"].ToString(); } }
        public static string SmsUser { get { return ConfigurationManager.AppSettings["SmsUser"].ToString(); } }
        public static string SmsKey { get { return ConfigurationManager.AppSettings["SmsKey"].ToString(); } }
        public static string SmsSender { get { return ConfigurationManager.AppSettings["SmsSender"].ToString(); } }
        public static string univName { get { return ConfigurationManager.AppSettings["univName"].ToString(); } }


        public static int PortNo { get { return Convert.ToInt32(ConfigurationManager.AppSettings["PortNo"]); } }
        public static string GEmailFrom { get { return ConfigurationManager.AppSettings["GEmailFrom"].ToString(); } }
        public static string GEmailUsername { get { return ConfigurationManager.AppSettings["GEmailUsername"].ToString(); } }
        public static string GEmailPassword { get { return ConfigurationManager.AppSettings["GEmailPassword"].ToString(); } }
        public static string ReplyEmail { get { return ConfigurationManager.AppSettings["ReplyEmail"].ToString(); } }
        public static string DbConnection1 { get { return ConfigurationManager.AppSettings["DbConnection"].ToString(); } }
        public static string DbConnection { get { return ConfigurationHelper.Config["ConnectionStrings:DefaultConnection"].ToString(); } }
        public static string DbConnectionAGRA_SEMESTER_RW { get { return ConfigurationManager.AppSettings["DbConnectionAGRA_SEMESTER_RW"].ToString(); } }

        public static string ImgCloudPath { get { return ConfigurationManager.AppSettings["ImgCloudPath"].ToString(); } }
        public static string SignPath { get { return ConfigurationManager.AppSettings["SignPath"].ToString(); } }
        public static string DocPath { get { return ConfigurationManager.AppSettings["DocPath"].ToString(); } }
        public static string PhotoPath { get { return ConfigurationManager.AppSettings["PhotoPath"].ToString(); } }

        public static string PayuUrl { get { return ConfigurationHelper.Config["AppSettings:PayuUrl"].ToString(); } }
        public static string PayuSuccessUrl { get { return ConfigurationHelper.Config["AppSettings:PayuSuccessUrl"].ToString(); } }
        public static string PayuFailUrl { get { return ConfigurationHelper.Config["AppSettings:PayuFailUrl"].ToString(); } }
        public static string PayuHash_seq { get { return ConfigurationHelper.Config["AppSettings:PayuHash_seq"].ToString(); } }
        
    }
}
