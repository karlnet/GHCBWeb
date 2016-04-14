using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace GHCBWeb.Models
{
    [MetadataType(typeof(boardinterfaceMetadata))]
    public partial class boardinterface
    {
        public class boardinterfaceMetadata
        {
            [Required,JsonIgnore]
            public int interfaceDescriptionId { get; set; }
            [Required, JsonIgnore]
            public int boardId { get; set; }
            [Required]
            public int slot { get; set; }
            [JsonIgnore]
            public board board { get; set; }

        }
    }
}