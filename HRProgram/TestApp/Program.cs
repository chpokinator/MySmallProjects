using HRProgram.BLL.DTO;
using HRProgram.BLL.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PositionService ps = new PositionService();

            PositionDTO position = new PositionDTO() { PostitionName = "suka blyat rabotay" };

            ps.CreateOrUpdate(position);


        }
    }
}
