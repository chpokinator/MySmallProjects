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
    public class StatusesService
    {
        IRepo<Statuses> statusRepo;
        IMapper mapper;
        DbContext context = new EmployeesContext();

        public StatusesService()
        {
            statusRepo = new StatusRepo(context);

            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Statuses, StatusesDTO>();
                x.CreateMap<StatusesDTO, Statuses>();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<StatusesDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Statuses>, IEnumerable<StatusesDTO>>(statusRepo.GetAll());
        }

        public StatusesDTO Get(int statusId)
        {
            Statuses statuses = statusRepo.Get(statusId);
            return mapper.Map<Statuses, StatusesDTO>(statuses);
        }

        public void Delete(StatusesDTO statusesDTO)
        {
            Statuses statusToDelete = statusRepo.Get(statusesDTO.StatusId);
            statusRepo.Delete(statusToDelete);
            statusRepo.SaveChanges();
        }

        public void CreateOrUpdate(StatusesDTO statusesDTO)
        {
            Statuses statuses = statusRepo.Get(statusesDTO.StatusId);
            if (statuses == null)
            {
                statuses = mapper.Map<StatusesDTO, Statuses>(statusesDTO);
            }

            statuses.StatusName = statusesDTO.StatusName;

            statusRepo.CreateOrUpdate(statuses);
            statusRepo.SaveChanges();
        }
    }
}
