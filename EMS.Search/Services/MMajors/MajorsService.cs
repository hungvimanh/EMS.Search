using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories;

namespace EMS.Search.Services.MMajors
{
    public interface IMajorsService : IServiceScoped
    {
        Task<Majors> Get(long Id);
        Task<List<Majors>> List(MajorsFilter majorsFilter);
    }
    public class MajorsService : IMajorsService
    {
        private readonly IUOW UOW;

        public MajorsService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<Majors> Get(long Id)
        {
            Majors majors = await UOW.MajorsRepository.Get(Id);
            return majors;
        }

        public async Task<List<Majors>> List(MajorsFilter majorsFilter)
        {
            return await UOW.MajorsRepository.List(majorsFilter);
        }
    }
}
