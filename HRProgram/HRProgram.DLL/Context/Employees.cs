namespace HRProgram.DLL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        [StringLength(100)]
        public string Fullname { get; set; }

        public int? Children { get; set; }

        public int? SummaryId { get; set; }

        public int? PositionId { get; set; }

        public int? StatusId { get; set; }

        public byte[] Photo { get; set; }

        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        [Column(TypeName = "money")]
        public decimal? Premium { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public virtual Position Position { get; set; }

        public virtual Statuses Statuses { get; set; }

        public virtual Summary Summary { get; set; }
    }
}
