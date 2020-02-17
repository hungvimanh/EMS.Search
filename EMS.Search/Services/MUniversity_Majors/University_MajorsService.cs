using EMS.Search.Entities;
using EMS.Search.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Search.Services.MUniversity_Majors_Majors
{
    public interface IUniversity_MajorsService : IServiceScoped
    {
        Task<University_Majors> Get(long Id);
        Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter);
    }
    public class University_MajorsService : IUniversity_MajorsService
    {
        private readonly IUOW UOW;

        public University_MajorsService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<University_Majors> Get(long Id)
        {
            University_Majors University_Majors = await UOW.University_MajorsRepository.Get(Id);
            return University_Majors;
        }

        public async Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter)
        {
            return await UOW.University_MajorsRepository.List(university_MajorsFilter);
        }

    }
}
