using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace GHCBWeb.Models
{
    [MetadataType(typeof(boardMetadata))]
    public partial class board
    {
        public class boardMetadata
        {
            [ScaffoldColumn(false)]
            public int Id { get; set; }
            [JsonProperty("IPAddress")]
            public string privateip { get; set; }
            //[JsonIgnore]
            public string token { get; set; }

            [JsonIgnore]
            public ICollection<boardinterface> boardinterfaces { get; set; }
            [JsonIgnore]
            public ICollection<userboard> userboards { get; set; }
        }
    }
}