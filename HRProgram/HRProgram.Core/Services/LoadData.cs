using HRProgram.BLL.DTO;
using HRProgram.BLL.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.Core.Service
{
    static public class LoadData
    {
        static public async Task<IEnumerable<PositionDTO>> LoadPositions(PositionService service)
        {

            return await Task.Run(() =>  service.GetAll()).ConfigureAwait(false);

        }

        static public async Task<IEnumerable<StatusesDTO>> LoadStatuses(StatusesService service)
        {

            return await Task.Run(() => service.GetAll()).ConfigureAwait(false);

        }

        static public async Task<IEnumerable<EmployeesDTO>> LoadEmployees(EmployeesService service)
        {

            return await Task.Run(() => service.GetAll()).ConfigureAwait(false);

        }

        static public async Task<IEnumerable<SummaryDTO>> LoadSummaries(SummaryService service)
        {

            return await Task.Run(() => service.GetAll()).ConfigureAwait(false);

        }
    }
}
