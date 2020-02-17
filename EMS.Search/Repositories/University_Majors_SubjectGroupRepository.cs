using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories.Models;

namespace EMS.Search.Repositories
{
    public interface IUniversity_Majors_SubjectGroupRepository
    {
        Task<University_Majors_SubjectGroup> Get(long Id);
        Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter u_M_Y_SFilter);
        Task<int> Count(University_Majors_SubjectGroupFilter u_M_Y_SFilter);
    }
    public class University_Majors_SubjectGroupRepository : IUniversity_Majors_SubjectGroupRepository
    {
        private readonly EMSContext eMSContext;
        public University_Majors_SubjectGroupRepository(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
        }

        private IQueryable<University_Majors_SubjectGroupDAO> DynamicFilter(IQueryable<University_Majors_SubjectGroupDAO> query, University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            if (university_Majors_SubjectGroupFilter == null)
                return query.Where(q => 1 == 0);
            if (university_Majors_SubjectGroupFilter.University_MajorsId.HasValue)
                query = query.Where(q => q.University_MajorsId.Equals(university_Majors_SubjectGroupFilter.University_MajorsId));
            
            if (university_Majors_SubjectGroupFilter.UniversityId != null)
                query = query.Where(q => q.University_Majors.UniversityId.Equals(university_Majors_SubjectGroupFilter.UniversityId));
            if (university_Majors_SubjectGroupFilter.MajorsId != null)
                query = query.Where(q => q.University_Majors.MajorsId.Equals(university_Majors_SubjectGroupFilter.MajorsId));
            if (university_Majors_SubjectGroupFilter.SubjectGroupId != null)
                query = query.Where(q => q.SubjectGroupId.Equals(university_Majors_SubjectGroupFilter.SubjectGroupId));

            if (university_Majors_SubjectGroupFilter.UniversityCode != null)
                query = query.Where(q => q.University_Majors.University.Code, university_Majors_SubjectGroupFilter.UniversityCode);
            if (university_Majors_SubjectGroupFilter.UniversityName != null)
                query = query.Where(q => q.University_Majors.University.Name, university_Majors_SubjectGroupFilter.UniversityName);
            if (university_Majors_SubjectGroupFilter.MajorsCode != null)
                query = query.Where(q => q.University_Majors.Majors.Code, university_Majors_SubjectGroupFilter.MajorsCode);
            if (university_Majors_SubjectGroupFilter.MajorsName != null)
                query = query.Where(q => q.University_Majors.Majors.Name, university_Majors_SubjectGroupFilter.MajorsName);

            if (university_Majors_SubjectGroupFilter.SubjectGroupCode != null)
                query = query.Where(q => q.SubjectGroup.Code, university_Majors_SubjectGroupFilter.SubjectGroupCode);
            if (university_Majors_SubjectGroupFilter.Benchmark != null)
                query = query.Where(q => q.Benchmark, university_Majors_SubjectGroupFilter.Benchmark);
            if (university_Majors_SubjectGroupFilter.Year != null)
                query = query.Where(q => q.University_Majors.Year.Equals(university_Majors_SubjectGroupFilter.Year));
            return query;
        }
        private IQueryable<University_Majors_SubjectGroupDAO> DynamicOrder(IQueryable<University_Majors_SubjectGroupDAO> query, University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            switch (university_Majors_SubjectGroupFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (university_Majors_SubjectGroupFilter.OrderBy)
                    {
                        case University_Majors_SubjectGroupOrder.SubjectGroupCode:
                            query = query.OrderBy(q => q.SubjectGroup.Code);
                            break;
                        case University_Majors_SubjectGroupOrder.Benchmark:
                            query = query.OrderBy(q => q.Benchmark);
                            break;
                        case University_Majors_SubjectGroupOrder.MajorsCode:
                            query = query.OrderBy(q => q.University_Majors.Majors.Code);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (university_Majors_SubjectGroupFilter.OrderBy)
                    {
                        case University_Majors_SubjectGroupOrder.SubjectGroupCode:
                            query = query.OrderByDescending(q => q.SubjectGroup.Code);
                            break;
                        case University_Majors_SubjectGroupOrder.Benchmark:
                            query = query.OrderByDescending(q => q.Benchmark);
                            break;
                        case University_Majors_SubjectGroupOrder.MajorsCode:
                            query = query.OrderByDescending(q => q.University_Majors.Majors.Code);
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
            query = query.Skip(university_Majors_SubjectGroupFilter.Skip).Take(university_Majors_SubjectGroupFilter.Take);
            return query;
        }
        private async Task<List<University_Majors_SubjectGroup>> DynamicSelect(IQueryable<University_Majors_SubjectGroupDAO> query)
        {

            List<University_Majors_SubjectGroup> university_Majorss = await query.Select(q => new University_Majors_SubjectGroup()
            {
                Id = q.Id,
                University_MajorsId = q.University_MajorsId,
                UniversityId = q.University_Majors.UniversityId,
                UniversityCode = q.University_Majors.University.Code,
                UniversityName = q.University_Majors.University.Name,
                MajorsId = q.University_Majors.MajorsId,
                MajorsCode = q.University_Majors.Majors.Code,
                MajorsName = q.University_Majors.Majors.Name,
                Year = q.University_Majors.Year,
                SubjectGroupId = q.SubjectGroupId,
                SubjectGroupCode = q.SubjectGroup.Code,
                SubjectGroupName = q.SubjectGroup.Name,
                Benchmark = q.Benchmark,
                Quantity = q.Quantity,
                Note = q.Note
            }).ToListAsync();
            return university_Majorss;
        }

        public async Task<int> Count(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            IQueryable<University_Majors_SubjectGroupDAO> university_Majors_SubjectGroupDAOs = eMSContext.University_Majors_SubjectGroup;
            university_Majors_SubjectGroupDAOs = DynamicFilter(university_Majors_SubjectGroupDAOs, university_Majors_SubjectGroupFilter);
            return await university_Majors_SubjectGroupDAOs.CountAsync();
        }

        public async Task<List<University_Majors_SubjectGroup>> List(University_Majors_SubjectGroupFilter university_Majors_SubjectGroupFilter)
        {
            if (university_Majors_SubjectGroupFilter == null) return new List<University_Majors_SubjectGroup>();
            IQueryable<University_Majors_SubjectGroupDAO> university_Majors_SubjectGroupDAOs = eMSContext.University_Majors_SubjectGroup;
            university_Majors_SubjectGroupDAOs = DynamicFilter(university_Majors_SubjectGroupDAOs, university_Majors_SubjectGroupFilter);
            university_Majors_SubjectGroupDAOs = DynamicOrder(university_Majors_SubjectGroupDAOs, university_Majors_SubjectGroupFilter);
            var university_Majors_SubjectGroups = await DynamicSelect(university_Majors_SubjectGroupDAOs);
            return university_Majors_SubjectGroups;
        }

        public async Task<University_Majors_SubjectGroup> Get(long Id)
        {
            University_Majors_SubjectGroup university_Majors_SubjectGroup = await eMSContext.University_Majors_SubjectGroup.Where(u => u.Id == Id).Select(u => new University_Majors_SubjectGroup
            {
                Id = u.Id,
                University_MajorsId = u.University_MajorsId,
                Year = u.University_Majors.Year,
                UniversityId = u.University_Majors.UniversityId,
                UniversityCode = u.University_Majors.University.Code,
                UniversityName = u.University_Majors.University.Name,
                MajorsId = u.University_Majors.MajorsId,
                MajorsCode = u.University_Majors.Majors.Code,
                MajorsName = u.University_Majors.Majors.Name,
                SubjectGroupId = u.SubjectGroupId,
                SubjectGroupCode = u.SubjectGroup.Code,
                SubjectGroupName = u.SubjectGroup.Name,
                Benchmark = u.Benchmark,
                Quantity = u.Quantity,
                Note = u.Note
            }).FirstOrDefaultAsync();

            return university_Majors_SubjectGroup;
        }
    }
}
