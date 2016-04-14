using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace GHCBWeb.Models
{
    [MetadataType(typeof(appUserMetadata))]
    public partial class appUser
    {
        public static ConcurrentDictionary<string, int> usertoken= new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        public class appUserMetadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            [DataType(DataType.EmailAddress)]
            public string email { get; set; }
            [DataType(DataType.PhoneNumber)]
            public string mobile { get; set; }
            //[JsonIgnore]
            public string token { get; set; }
           
            public string userid { get; set; }
            //[JsonIgnore]
            public string password { get; set; }
            [JsonIgnore]
            public ICollection<userboard> userboards { get; set; }
        }

    }
}