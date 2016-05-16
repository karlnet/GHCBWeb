using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHCBWeb.Models
{
    public class BoardModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string Alias { get; set; }
        public string MAC { get; set; }
        public string ROMVersion { get; set; }
        public string Description { get; set; }
        public string Privateip { get; set; }
        public string Publicip { get; set; }
        public string SSID { get; set; }
        public string BSSID { get; set; }
        public string Deviceid { get; set; }
        public string Status { get; set; }
        public DateTime? Offtime { get; set; }
        public DateTime? Onlinetime { get; set; }
        public DateTime? Createtime { get; set; }


    }
}