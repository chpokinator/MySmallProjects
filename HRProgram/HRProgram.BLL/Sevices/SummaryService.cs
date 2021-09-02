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
    public class SummaryService
    {
        IRepo<Summary> summaryRepo;
        IMapper mapper;
        DbContext context = new EmployeesContext();

        public SummaryService()
        {
            summaryRepo = new SummaryRepo(context);

            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Summary, SummaryDTO>();
                x.CreateMap<SummaryDTO, Summary>();
            });

            mapper = new Mapper(configuration);
        }

        public IEnumerable<SummaryDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Summary>, IEnumerable<SummaryDTO>>(summaryRepo.GetAll());
        }

        public SummaryDTO Get(int summaryId)
        {
            Summary summary = summaryRepo.Get(summaryId);
            return mapper.Map<Summary, SummaryDTO>(summary);
        }

        public void Delete(SummaryDTO summary)
        {
            Summary summaryToDelete = summaryRepo.Get(summary.SummaryId);
            summaryRepo.Delete(summaryToDelete);
            summaryRepo.SaveChanges();
        }

        public void CreateOrUpdate(SummaryDTO summaryDTO)
        {
            Summary summary = summaryRepo.Get(summaryDTO.SummaryId);
            if (summary == null)
            {
                summary = mapper.Map<SummaryDTO, Summary>(summaryDTO);
            }

            summary.ContactInfo = summaryDTO.ContactInfo;
            summary.Fullname = summaryDTO.Fullname;
            summary.Education = summaryDTO.Education;
            summary.Experience = summaryDTO.Experience;
            summary.Position = summaryDTO.Position;
            summary.Recommendations = summaryDTO.Recommendations;
            summary.Skills = summaryDTO.Skills;
            

            summaryRepo.CreateOrUpdate(summary);
            summaryRepo.SaveChanges();
        }

    }
}
