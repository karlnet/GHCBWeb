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
    
    public partial class interfaceDescription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public interfaceDescription()
        {
            this.boardinterfaces = new HashSet<boardinterface>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string direction { get; set; }
        public string description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<boardinterface> boardinterfaces { get; set; }
    }
}
