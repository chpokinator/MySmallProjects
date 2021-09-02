using HRProgram.BLL.DTO;
using HRProgram.BLL.Sevices;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core.Service
{
    static public class FiltersService
    {

        static public IEnumerable<EmployeesDTO> FilterByStatus(int index, MvxObservableCollection<EmployeesDTO> employees)
        {

            return employees.Where(x => x.StatusId == index);

        }

        static public IEnumerable<EmployeesDTO> FilterByPosition(int index, MvxObservableCollection<EmployeesDTO> employees)
        {

            return employees.Where(x => x.StatusId == index);


        }
    }
}
