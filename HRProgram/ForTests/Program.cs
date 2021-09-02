using HRProgram.BLL.DTO;
using HRProgram.BLL.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTests
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeesService service = new EmployeesService();
            SummaryService ss = new SummaryService();
            List<EmployeesDTO> employees = new List<EmployeesDTO>(service.GetAll());

            EmployeesDTO employeesDTO = service.Get(9);

            

            foreach (var a in service.GetAll())
            {
                Console.WriteLine($"{a.EmployeeId,-10}{a.Fullname}");
            }


        }
    }
}
