//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GHCBWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class board
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public board()
        {
            this.boardinterfaces = new HashSet<boardinterface>();
            this.userboards = new HashSet<userboard>();
        }
    
        public int Id { get; set; }
        public string serialno { get; set; }
        public string alias { get; set; }
        public string mac { get; set; }
        public string romversion { get; set; }
        public string description { get; set; }
        public string privateip { get; set; }
        public string publicip { get; set; }
        public string ssid { get; set; }
        public string bssid { get; set; }
        public string token { get; set; }
        public string deviceid { get; set; }
        public string status { get; set; }
        public string offtime { get; set; }
        public string onlinetime { get; set; }
        public string createtime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<boardinterface> boardinterfaces { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userboard> userboards { get; set; }
    }
}
