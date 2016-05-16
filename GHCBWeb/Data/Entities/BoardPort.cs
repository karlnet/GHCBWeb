using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHCBWeb.Data.Entities
{
    public class BoardPort
    {
        public  BoardPort() {

            //this.board = new Board();
            //this.portDescription = new PortDescription();
        }

        public int Id { get; set; }
        public int BoardId { get; set; }
        public int Port { get; set; }
        public string Alias { get; set; }
        public int PortDescriptionId { get; set; }
        public string Control { get; set; }
        public Nullable<double> X { get; set; }
        public Nullable<double> Y { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Length { get; set; }
        public string Color { get; set; }
        public string Enable { get; set; }
        public Nullable<double> Uplimit { get; set; }
        public Nullable<double> Lowlimit { get; set; }
        public Nullable<double> Max { get; set; }
        public Nullable<double> Min { get; set; }
        public string DataType { get; set; }
        public Nullable<double> DefaultValue { get; set; }      
        public string Description { get; set; }

        //public  Board board { get; set; }
        public  PortDescription portDescription { get; set; }
    }
}