using EMS.Search.Entities;
using EMS.Search.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Search.Repositories
{
    public interface ISubjectGroupRepository
    {
        Task<SubjectGroup> Get(long Id);
        Task<int> Count(SubjectGroupFilter subjectGroupFilter);
        Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter);
    }
    public class SubjectGroupRepository : ISubjectGroupRepository
    {
        private readonly EMSContext eMSContext;
        public SubjectGroupRepository(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
        }

        private IQueryable<SubjectGroupDAO> DynamicFilter(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null)
                return query.Where(q => 1 == 0);

            if (subjectGroupFilter.Id != null)
                query = query.Where(q => q.Id, subjectGroupFilter.Id);
            if (subjectGroupFilter.Name != null)
                query = query.Where(q => q.Name, subjectGroupFilter.Name);
            if (subjectGroupFilter.Code != null)
                query = query.Where(q => q.Code, subjectGroupFilter.Code);
            return query;
        }
        private IQueryable<SubjectGroupDAO> DynamicOrder(IQueryable<SubjectGroupDAO> query, SubjectGroupFilter subjectGroupFilter)
        {
            switch (subjectGroupFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (subjectGroupFilter.OrderBy)
                    {
                        case SubjectGroupOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case SubjectGroupOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.Id);
                    break;
            }
            query = query.Skip(subjectGroupFilter.Skip).Take(subjectGroupFilter.Take);
            return query;
        }
        private async Task<List<SubjectGroup>> DynamicSelect(IQueryable<SubjectGroupDAO> query)
        {

            List<SubjectGroup> subjectGroups = await query.Select(q => new SubjectGroup()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return subjectGroups;
        }

        public async Task<int> Count(SubjectGroupFilter subjectGroupFilter)
        {
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = eMSContext.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            return await subjectGroupDAOs.CountAsync();
        }

        public async Task<List<SubjectGroup>> List(SubjectGroupFilter subjectGroupFilter)
        {
            if (subjectGroupFilter == null) return new List<SubjectGroup>();
            IQueryable<SubjectGroupDAO> subjectGroupDAOs = eMSContext.SubjectGroup;
            subjectGroupDAOs = DynamicFilter(subjectGroupDAOs, subjectGroupFilter);
            subjectGroupDAOs = DynamicOrder(subjectGroupDAOs, subjectGroupFilter);
            var subjectGroups = await DynamicSelect(subjectGroupDAOs);
            return subjectGroups;
        }

        public async Task<SubjectGroup> Get(long Id)
        {
            SubjectGroup subjectGroup = await eMSContext.SubjectGroup.Where(d => d.Id == Id).Select(d => new SubjectGroup
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name
            }).FirstOrDefaultAsync();

            return subjectGroup;
        }
    }
}
