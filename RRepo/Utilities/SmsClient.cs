using RModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace RRepo
{
    public class SmsClient
    {
        public string Url
        {
            get
            {
                return WebConfigSetting.SmsUrl;
            }
        }
        public string User
        {
            get
            {
                return WebConfigSetting.SmsUser;
            }
        }
        public string Key
        {
            get
            {
                return WebConfigSetting.SmsKey;
            }
        }
        public string Sender
        {
            get
            {
                return WebConfigSetting.SmsSender;
            }
        }
        
        public void SendMsg(string mobile, string msg)
        {
            try
            {
                string[] no = mobile.Split('-');
                if (no[0].Contains("+91"))
                {
                    mobile = no[1].ToString();
                }
               string url = Url + "user=" + User + "&key=" + Key + "&mobile=" + mobile + "&message=" + msg + "&senderid=" + Sender + "&accusage=1";

                if ((!(string.IsNullOrEmpty(mobile) & string.IsNullOrEmpty(msg))))
                {
                    send(url);
                }
                return;
            }
            catch (Exception ex)
            {

            }
        }

        public void SendEnquiryInfo(string msg, string contactNo)
        {
            try
            {
                string[] no = contactNo.Split('-');
                if (no[0].Contains("+91"))
                {
                    contactNo = no[1].ToString();
                }
                if ((!(string.IsNullOrEmpty(contactNo) && string.IsNullOrEmpty(msg))))
                {
                    string url = Url + "user=" + User + "&key=" + Key + "&mobile=" + contactNo + "&message=" + msg + "&senderid=" + Sender + "&accusage=1";
                    send(url);
                }
                return;
            }
            catch (Exception ex)
            {

            }
        }

        private void send(string url)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            respStreamReader.Close();
            myResp.Close();
        }

    }
}
