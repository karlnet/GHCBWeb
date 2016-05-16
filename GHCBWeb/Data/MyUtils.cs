using GHCBWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace GHCBWeb.Data
{
    public class MyUtils
    {

        public static string GetUnixTimeStamp()
        {
            return ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
        }
        public static string MD5ForPHP(string t)
        {
            byte[] data = Encoding.ASCII.GetBytes(t);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
          
            StringBuilder sb = new StringBuilder();
            foreach (var b in data)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static string getSign()
        {
            string timeStamp = GetUnixTimeStamp();
            return MD5ForPHP(MyAPPs.AppSecret+timeStamp) + ", " + timeStamp;

        }
        public static string getActiveToken(string mac)
        {
            return MD5ForPHP(mac + MyAPPs.ProductSecertKey);
        }
    }
}