using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GHCBWeb.Models
{
    [MetadataType(typeof(interfaceDescriptionMetadata))]
    public partial class interfaceDescription
    {
        public class interfaceDescriptionMetadata
        {
            [ScaffoldColumn(false), JsonIgnore]
            public int Id { get; set; }
            [JsonIgnore]
            public ICollection<boardinterface> boardinterfaces { get; set; }
        }

    }
}