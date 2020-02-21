using EMS.Search;
using EMS.Search.Entities;
using EMS.Search.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TwelveFinal.Services.MSubjectGroup
{
    public interface ISubjectGroupService : IServiceScoped
    {
        Task<SubjectGroup> Get(long Id);
        Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter);
    }
    public class SubjectGroupService : ISubjectGroupService
    {
        private readonly IUOW UOW;

        public SubjectGroupService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<SubjectGroup> Get(long Id)
        {
            SubjectGroup subjectGroup = await UOW.SubjectGroupRepository.Get(Id);
            return subjectGroup;
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter)
        {
            return await UOW.SubjectGroupRepository.List(subjectGroupFilter);
        }
    }
}
