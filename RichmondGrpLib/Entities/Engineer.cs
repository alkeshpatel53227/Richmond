//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RichmondGrpLib.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Engineer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Engineer()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int EngineerId { get; set; }
        public string EngineerName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string UpdateUserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
