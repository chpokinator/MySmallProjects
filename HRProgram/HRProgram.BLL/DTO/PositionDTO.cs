using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.BLL.DTO
{
    public class PositionDTO
    {
        public int PositionId { get; set; }

        public string PostitionName { get; set; }

        public override string ToString()
        {
            return PostitionName;
        }
    }
}
