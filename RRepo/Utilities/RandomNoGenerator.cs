using System;
using System.Security.Cryptography;
using System.Text;

namespace RRepo.Utilities
{
    public static class RandomNoGenerator
    {
        private static string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);
            byte[] hashValue;

            string hex = "";
            var hashString = SHA512.Create();
            hashValue = hashString.ComputeHash(message);

            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        //public string GenerateUniqueNo()
        //{
        //    DateTime date = DateTime.Now;
        //    string uniqueID = String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
        //    return uniqueID.Substring(2, uniqueID.Length - 2); //15 digit number : 171014072704579
        //}

        public static string GenerateUniqueNo()
        {
            DateTime date = DateTime.Now;
            Random rnd = new Random();
            string strHash = Generatehash512(rnd.ToString() + date);
            string randStr = strHash.ToString().Substring(0, 6).ToUpper(); //6 chars

            string dateStr = String.Format("{0:0000}{1:00}{2:00}", date.Year, date.Month, date.Day); //8 chars
            dateStr = dateStr.Substring(2, dateStr.Length - 2); //6 chars

            //mixing random and date chars
            string certNo = randStr.Substring(0, 2) + dateStr.Substring(0, 2) + randStr.Substring(2, 2) + dateStr.Substring(2, 2) + randStr.Substring(4, 2) + dateStr.Substring(4, 2);

            return certNo; //12 chars
        }
    }
}
