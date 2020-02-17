using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories;

namespace EMS.Search.Services.MUniversity_Majors
{
    public interface IUniversity_Majors_SubjectGroupService : IServiceScoped
    {
        Task<University_Majors_SubjectGroup> Get(long Id);
        Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter);
    }
    public class University_Majors_SubjectGroupService : IUniversity_Majors_SubjectGroupService
    {
        private readonly IUOW UOW;

        public University_Majors_SubjectGroupService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<University_Majors_SubjectGroup> Get(long Id)
        {
            University_Majors_SubjectGroup u_M_Y_S = await UOW.University_Majors_SubjectGroupRepository.Get(Id);
            return u_M_Y_S;
        }

        public async Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            return await UOW.University_Majors_SubjectGroupRepository.List(university_Majors_SubjectGroupFilter);
        }
    }
}
