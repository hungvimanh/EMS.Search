using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories;

namespace EMS.Search.Services.MUniversity
{
    public interface IUniversityService : IServiceScoped
    {
        Task<University> Get(long Id);
        Task<List<University>> List(UniversityFilter universityFilter);
    }
    public class UniversityService : IUniversityService
    {
        private readonly IUOW UOW;

        public UniversityService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<University> Get(long Id)
        {
            University university = await UOW.UniversityRepository.Get(Id);
            return university;
        }

        public async Task<List<University>> List(UniversityFilter universityFilter)
        {
            return await UOW.UniversityRepository.List(universityFilter);
        }

    }
}
