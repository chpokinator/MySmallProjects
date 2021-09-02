using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.BLL.DTO
{
    public class StatusesDTO
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public override string ToString()
        {
            return StatusName;
        }
    }
}
