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
    public class PositionService
    {
        IRepo<Position> positionRepo;
        IMapper mapper;
        DbContext context = new EmployeesContext();

        public PositionService()
        {
            positionRepo = new PositionRepo(context);

            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Position, PositionDTO>();
                x.CreateMap<PositionDTO, Position>();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<PositionDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Position>, IEnumerable<PositionDTO>>(positionRepo.GetAll());
        }

        public PositionDTO Get(int positionId)
        {
            Position position = positionRepo.Get(positionId);
            return mapper.Map<Position, PositionDTO>(position);
        }

        public void Delete(PositionDTO position)
        {
            Position positionToDelete = positionRepo.Get(position.PositionId);
            positionRepo.Delete(positionToDelete);
            positionRepo.SaveChanges();
        }

        public void CreateOrUpdate(PositionDTO positionDTO)
        {
            Position position = positionRepo.Get(positionDTO.PositionId);
            if (position == null)
            {
                position = new Position() { PostitionName = positionDTO.PostitionName };
            }

            position.PostitionName = positionDTO.PostitionName;

            positionRepo.CreateOrUpdate(position);
            positionRepo.SaveChanges();
        }
    }
}
