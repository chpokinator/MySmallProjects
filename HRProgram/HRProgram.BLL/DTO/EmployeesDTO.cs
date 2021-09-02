using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.BLL.DTO
{
    public class EmployeesDTO
    {
        public int EmployeeId { get; set; }

        public string Fullname { get; set; }

        public int? Children { get; set; }

        public int? SummaryId { get; set; }

        public int? PositionId { get; set; }

        public int? StatusId { get; set; }

        public byte[] Photo { get; set; }

        public decimal? Salary { get; set; }

        public decimal? Premium { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
