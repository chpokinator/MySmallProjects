using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.BLL.DTO
{
    public class SummaryDTO
    {
        public int SummaryId { get; set; }

        public string Position { get; set; }

        public string ContactInfo { get; set; }

        public string Experience { get; set; }

        public string Education { get; set; }

        public string Skills { get; set; }

        public string Recommendations { get; set; }
        
        [StringLength(100)]
        public string Fullname { get; set; }

        public override string ToString()
        {
            return Fullname;
        }
    }
}
