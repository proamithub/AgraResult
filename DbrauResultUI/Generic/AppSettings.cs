using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DbrauResultUI.Generic
{
    public class AppSettings
    {
        #region Get Appsetting Json
        public static string GetDestinationFilePath()
        {
            return GetAppSettingsInfo("AppSettings:DestinationFilePath");
        }
		public static string GetSaveFilePath()
		{
			return GetAppSettingsInfo("AppSettings:SaveFilePath");
		}
		public static string GetDestinationOneViewFilePath()
        {
            return GetAppSettingsInfo("AppSettings:DestinationOneViewFilePath");
        }
        public static string GetDomainName()
        {
            return GetAppSettingsInfo("AppSettings:Domain");
        }
        public static string GetPhotoDomainName()
        {
            return GetAppSettingsInfo("AppSettings:PhotoDomain");
        }
        private static string GetAppSettingsInfo(string appSettingsParam)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                  .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                  .AddJsonFile("appsettings.json")
                                                  .Build();


            return configuration.GetValue<string>(appSettingsParam);
        }
        public static string GetEpasswordSecrateKey()
        {
            return "VK87A6";
        }
        #endregion
    }
}