using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHCBWeb.Data.Entities
{
    public class PortDescription
    {
        public PortDescription()
        {
            //this.boardPorts = new HashSet<BoardPort>();
        }

        public int Id { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceType { get; set; }
        public string Direction { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }

        //public virtual ICollection<BoardPort> boardPorts { get; set; }
    }
}