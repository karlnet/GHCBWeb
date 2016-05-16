using GHCBWeb.Data.Entities;
using GHCBWeb.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace GHCBWeb.Data
{
    public class MyAPPs
    {
        public static ConcurrentDictionary<string, ApplicationUser> currentUsers = new ConcurrentDictionary<string, ApplicationUser>();

        public static string AppID = null;
        public static string AppSecret = null;

        public static string ProductID = null;
        public static string ProductSecertKey = null;

        public static string QiniuAccesskey = null;
        public static string QiniuSecretkey = null;

        public static string FogBaseURL = "http://easylink.io/";

        public static string FogLoginURL = FogBaseURL + "v2/users/login";
        public static string FogSMSVerificationURL = FogBaseURL + "v2/users/sms_verification_code";
        public static string FogRegisterURL = FogBaseURL + "v2/users";
        public static string FogResetPasswordURL = FogBaseURL + "v2/users/password/reset";
        public static string FogAuthorizeURL = FogBaseURL + "v2/devices/bind";
       
        public static string FogActiveURL = FogBaseURL + "v2/devices";

        public static void setRequest(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Application-Id", MyAPPs.AppID);
            client.DefaultRequestHeaders.Add("X-Request-Sign", MyUtils.getSign());
        }

    }
}