namespace HRProgram.DLL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Summary")]
    public partial class Summary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Summary()
        {
            Employees = new HashSet<Employees>();
        }


        [Key]
        public int SummaryId { get; set; }

        public string Position { get; set; }

        public string ContactInfo { get; set; }

        public string Experience { get; set; }

        public string Education { get; set; }

        public string Skills { get; set; }

        public string Recommendations { get; set; }

        [StringLength(100)]
        public string Fullname { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
