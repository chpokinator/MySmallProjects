using AutoMapper;
using HRProgram.BLL.DTO;
using HRProgram.DLL.Context;
using HRProgram.DLL.Repostitory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.BLL.Sevices
{
    public class EmployeesService
    {
        IRepo<Employees> employeeRepo;
        IMapper mapper;
        DbContext context = new EmployeesContext();

        public EmployeesService()
        {
            employeeRepo = new EmployeesRepo(context);

            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Employees, EmployeesDTO>();
                x.CreateMap<EmployeesDTO, Employees>();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<EmployeesDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Employees>, IEnumerable<EmployeesDTO>>(employeeRepo.GetAll());
        }

        public EmployeesDTO Get(int employeeId)
        {
            Employees employees = employeeRepo.Get(employeeId);
            return mapper.Map<Employees, EmployeesDTO>(employees);
        }

        public void Delete(EmployeesDTO employeesDTO)
        {
            Employees employeeToDelete = employeeRepo.Get(employeesDTO.EmployeeId);
            employeeRepo.Delete(employeeToDelete);
            employeeRepo.SaveChanges();
        }

        public void CreateOrUpdate(EmployeesDTO employeesDTO)
        {
            Employees employees = employeeRepo.Get(employeesDTO.EmployeeId);
            if (employees == null)
            {
                employees = mapper.Map<EmployeesDTO, Employees>(employeesDTO);
            }

            employees.Children = employeesDTO.Children;
            employees.Fullname = employeesDTO.Fullname;
            employees.Photo = employeesDTO.Photo;
            employees.PositionId = employeesDTO.PositionId;
            employees.Premium = employeesDTO.Premium;
            employees.Salary = employeesDTO.Salary;
            employees.StatusId = employeesDTO.StatusId;
            employees.SummaryId = employeesDTO.SummaryId;
            employees.Birthday = employeesDTO.Birthday;

            employeeRepo.CreateOrUpdate(employees);
            employeeRepo.SaveChanges();
        }
    }
}
