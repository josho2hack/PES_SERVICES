//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Point01Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class Offices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Offices()
        {
            this.DataForEvaluations = new HashSet<DataForEvaluations>();
            this.PointOfEvaluations = new HashSet<PointOfEvaluations>();
            this.PointOfEvaluations1 = new HashSet<PointOfEvaluations>();
            this.Scores = new HashSet<Scores>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string ApplicationUserId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataForEvaluations> DataForEvaluations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointOfEvaluations> PointOfEvaluations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PointOfEvaluations> PointOfEvaluations1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scores> Scores { get; set; }
    }
}
