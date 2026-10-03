using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace RRepo
{
    public static class Utility
    {
        public static string GenerateID()
        {
            char[] chars = new char[62];
            chars = "1234567890abcDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[4];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(4);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }


            string var = DateTime.Today.Day.ToString();
            switch (var)
            {
                case "1": var = "01"; break;
                case "2": var = "02"; break;
                case "3": var = "03"; break;
                case "4": var = "04"; break;
                case "5": var = "05"; break;
                case "6": var = "06"; break;
                case "7": var = "07"; break;
                case "8": var = "08"; break;
                case "9": var = "09"; break;

            }

            string strgetdate = var;
            var = DateTime.Today.Month.ToString();
            switch (var)
            {
                case "1": var = "01"; break;
                case "2": var = "02"; break;
                case "3": var = "03"; break;
                case "4": var = "04"; break;
                case "5": var = "05"; break;
                case "6": var = "06"; break;
                case "7": var = "07"; break;
                case "8": var = "08"; break;
                case "9": var = "09"; break;

            }
            strgetdate += var;

            var = DateTime.Today.Year.ToString();

            var = var.Substring(2);
            switch (var)
            {
                case "1": var = "01"; break;
                case "2": var = "02"; break;
                case "3": var = "03"; break;
                case "4": var = "04"; break;
                case "5": var = "05"; break;
                case "6": var = "06"; break;
                case "7": var = "07"; break;
                case "8": var = "08"; break;
                case "9": var = "09"; break;

            }

            strgetdate += var;
            result.Append(strgetdate);
            return (result.ToString());
        }
        public static DateTime GetCurrentIndianDateTime()
        {
            string zoneId = "India Standard Time";
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(zoneId);
            DateTime submitDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi);
            return submitDate;
        }

        public static DateTime? GetDateInIndianDateTime(DateTime? _toConvertDate)
        {
            try
            {
                if (_toConvertDate != null)
                {
                    DateTime toConvertDate = Convert.ToDateTime(_toConvertDate);
                    TimeZoneInfo timeZone1 = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    TimeZoneInfo timeZone2 = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime _convertedIndiantime = TimeZoneInfo.ConvertTime(toConvertDate, timeZone1, timeZone2);
                    return _convertedIndiantime;
                }
            }
            catch (Exception)
            { }
            return null;
        }

        public static DateTime GetDateInIndianDateTime(DateTime _toConvertDate)
        {
            try
            {
                DateTime _convertedIndiantime;
                TimeZoneInfo IST_TimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                _convertedIndiantime = TimeZoneInfo.ConvertTimeFromUtc(_toConvertDate, IST_TimeZone);
                return _convertedIndiantime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime ConvertDateToUTC(DateTime date)
        {
            return TimeZoneInfo.ConvertTimeToUtc(date);
        }

        public static DateTime ConvertDateIST_ToUTC(DateTime date)
        {
            TimeZoneInfo IST_TimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(date, IST_TimeZone);
        }

        public static DateTime ConvertDateFrom_UTC_to_IST(DateTime UTCDate)
        {
            try
            {
                DateTime ISTDate;
                TimeZoneInfo IST_TimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                ISTDate = TimeZoneInfo.ConvertTimeFromUtc(UTCDate, IST_TimeZone);
                return ISTDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime? ConvertDateFrom_UTC_to_IST(DateTime? UTCDate)
        {
            try
            {
                if (UTCDate != null)
                {
                    DateTime _ISTDate;
                    DateTime _convertedUTCDate = Convert.ToDateTime(UTCDate);
                    TimeZoneInfo IST_TimeZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    _ISTDate = TimeZoneInfo.ConvertTimeFromUtc(_convertedUTCDate, IST_TimeZone);
                    return _ISTDate;
                }
            }
            catch (Exception ex)
            { }
            return null;
        }

        public static string GenerateUniqueId()
        {
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + DateTime.Now.Millisecond.ToString());
            string id = strHash.ToString().Substring(0, 20).ToUpper();
            return id;
        }
        private static string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
        public static string GenerateUniqueNo()
        {
            DateTime date = DateTime.Now;
            string uniqueID = String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            return uniqueID.Substring(2, uniqueID.Length - 2); //15 digit number : 171014072704579

            //Random rnd = new Random();
            //string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
            //string invoice_No = strHash.ToString().Substring(0, 20).ToUpper();
            //return invoice_No; //20 chars hash : AB85A7225F00AD974AF2
        }

        public static string FormatDurationString(string Duration)
        {
            if (!string.IsNullOrEmpty(Duration))
            {
                string[] dt = Duration.Split(':');
                if (dt.Length == 1)
                {
                    Duration = "00:00:" + dt[0];
                }
                else if (dt.Length == 2)
                {
                    Duration = "00:" + dt[0] + ":" + dt[1];
                }
                else
                {
                    Duration = dt[0] + ":" + dt[1] + ":" + dt[2];
                }
                return Duration;
            }
            else
            {
                return "00:00:00";
            }
        }
        public static string DurationInString(string Duration)
        {
            if (!string.IsNullOrEmpty(Duration))
            {
                string[] dt = Duration.Split(':');
                if (dt.Length == 1)
                {
                    Duration = "00:" + dt[0];
                }
                else
                {
                    Duration = dt[0] + ":" + dt[1];
                }
                return Duration;
            }
            else
            {
                return "00:00";
            }
        }
        public static string DurationInHHMMSS(string durationStr)
        {
            if (!string.IsNullOrEmpty(durationStr))
            {
                durationStr = FormatDurationString(durationStr);
                List<string> lstDuration = durationStr.Split(':').ToList();
                string strDuration = "";
                if (lstDuration[0] != "00")
                {
                    strDuration = lstDuration[0] + "h ";
                }
                strDuration = strDuration + lstDuration[1] + "m " + lstDuration[2] + "s ";
                return strDuration;
            }
            else
            {
                return "00m 00s";
            }
        }
        public static string DurationInHHMM(string durationStr)
        {
            if (!string.IsNullOrEmpty(durationStr))
            {
                durationStr = FormatDurationString(durationStr);
                List<string> lstDuration = durationStr.Split(':').ToList();
                string strDuration = "";
                if (lstDuration[0] != "00")
                {
                    strDuration = lstDuration[0] + "h ";
                }
                strDuration = strDuration + lstDuration[1] + "m ";
                return strDuration;
            }
            else
            {
                return "00m 00s";
            }
        }

        public static string GenerateOtp()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[5];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString; 
        }
    }
}
