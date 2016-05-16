using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHCBWeb.Data.Entities
{
    public class Board
    {
        public Board()
        {
            this.boardPorts = new HashSet<BoardPort>();
            this.applicationUserBoards = new HashSet<Board>();
          
        }

        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string Name { get; set; }
        public string MAC { get; set; }
        public string ROMVersion { get; set; }
        public string Description { get; set; }
        public string Privateip { get; set; }
        public string Publicip { get; set; }
        public string SSID { get; set; }
        public string BSSID { get; set; }
        public string Token { get; set; }
        public string Deviceid { get; set; }
        public string Status { get; set; }
      
        public DateTime? Offtime { get; set; }
        public DateTime? Onlinetime { get; set; }
        public DateTime? Createtime { get; set; }

    
        public  ICollection<BoardPort> boardPorts { get; set; }   

        public  ICollection<Board> applicationUserBoards { get; set; }
      
    }
}