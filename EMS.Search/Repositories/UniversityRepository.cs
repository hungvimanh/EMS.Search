using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories.Models;

namespace EMS.Search.Repositories
{
    public interface IUniversityRepository
    {
        Task<List<University>> List(UniversityFilter universityFilter);
        Task<int> Count(UniversityFilter universityFilter);
        Task<University> Get(long Id);
    }
    public class UniversityRepository : IUniversityRepository
    {
        private readonly EMSContext eMSContext;
        public UniversityRepository(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
        }

        private IQueryable<UniversityDAO> DynamicFilter(IQueryable<UniversityDAO> query, UniversityFilter universityFilter)
        {
            if (universityFilter == null)
                return query.Where(q => 1 == 0);

            if (universityFilter.Id != null)
                query = query.Where(q => q.Id, universityFilter.Id);
            if (universityFilter.Name != null)
                query = query.Where(q => q.Name, universityFilter.Name);
            if (universityFilter.Code != null)
                query = query.Where(q => q.Code, universityFilter.Code);
            return query;
        }
        private IQueryable<UniversityDAO> DynamicOrder(IQueryable<UniversityDAO> query, UniversityFilter universityFilter)
        {
            switch (universityFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (universityFilter.OrderBy)
                    {
                        case UniversityOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case UniversityOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (universityFilter.OrderBy)
                    {
                        case UniversityOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case UniversityOrder.Name:
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
            query = query.Skip(universityFilter.Skip).Take(universityFilter.Take);
            return query;
        }
        private async Task<List<University>> DynamicSelect(IQueryable<UniversityDAO> query)
        {
            List<University> Universitys = await query.Select(q => new University()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code,
                Address = q.Address,
                Website = q.Website
            }).ToListAsync();
            return Universitys;
        }

        public async Task<int> Count(UniversityFilter universityFilter)
        {
            IQueryable<UniversityDAO> universityDAOs = eMSContext.University;
            universityDAOs = DynamicFilter(universityDAOs, universityFilter);
            return await universityDAOs.CountAsync();
        }

        public async Task<List<University>> List(UniversityFilter universityFilter)
        {
            if (universityFilter == null) return new List<University>();
            IQueryable<UniversityDAO> universityDAOs = eMSContext.University;
            universityDAOs = DynamicFilter(universityDAOs, universityFilter);
            universityDAOs = DynamicOrder(universityDAOs, universityFilter);
            var universitys = await DynamicSelect(universityDAOs);
            return universitys;
        }

        public async Task<University> Get(long Id)
        {
            University university = await eMSContext.University.Where(u => u.Id.Equals(Id)).Select(u => new University
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name,
                Address = u.Address,
                Website = u.Website
            }).FirstOrDefaultAsync();

            return university;
        }
    }
}
