using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Repositories.Models;

namespace EMS.Search.Repositories
{
    public interface IMajorsRepository
    {
        Task<List<Majors>> List(MajorsFilter majorsFilter);
        Task<int> Count(MajorsFilter majorsFilter);
        Task<Majors> Get(long Id);
    }
    public class MajorsRepository : IMajorsRepository
    {
        private readonly EMSContext eMSContext;
        public MajorsRepository(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
        }

        private IQueryable<MajorsDAO> DynamicFilter(IQueryable<MajorsDAO> query, MajorsFilter majorsFilter)
        {
            if (majorsFilter == null)
                return query.Where(q => 1 == 0);

            if (majorsFilter.Id != null)
                query = query.Where(q => q.Id, majorsFilter.Id);
            if (majorsFilter.Name != null)
                query = query.Where(q => q.Name, majorsFilter.Name);
            if (majorsFilter.Code != null)
                query = query.Where(q => q.Code, majorsFilter.Code);
            return query;
        }
        private IQueryable<MajorsDAO> DynamicOrder(IQueryable<MajorsDAO> query, MajorsFilter majorsFilter)
        {
            switch (majorsFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (majorsFilter.OrderBy)
                    {
                        case MajorsOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case MajorsOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (majorsFilter.OrderBy)
                    {
                        case MajorsOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case MajorsOrder.Name:
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
            query = query.Skip(majorsFilter.Skip).Take(majorsFilter.Take);
            return query;
        }
        private async Task<List<Majors>> DynamicSelect(IQueryable<MajorsDAO> query)
        {

            List<Majors> Majorss = await query.Select(q => new Majors()
            {
                Id = q.Id,
                Name = q.Name,
                Code = q.Code
            }).ToListAsync();
            return Majorss;
        }

        public async Task<int> Count(MajorsFilter majorsFilter)
        {
            IQueryable<MajorsDAO> majorsDAOs = eMSContext.Majors;
            majorsDAOs = DynamicFilter(majorsDAOs, majorsFilter);
            return await majorsDAOs.CountAsync();
        }

        public async Task<List<Majors>> List(MajorsFilter majorsFilter)
        {
            if (majorsFilter == null) return new List<Majors>();
            IQueryable<MajorsDAO> majorsDAOs = eMSContext.Majors;
            majorsDAOs = DynamicFilter(majorsDAOs, majorsFilter);
            majorsDAOs = DynamicOrder(majorsDAOs, majorsFilter);
            var majorss = await DynamicSelect(majorsDAOs);
            return majorss;
        }

        public async Task<Majors> Get(long Id)
        {
            Majors Majors = await eMSContext.Majors.Where(m => m.Id.Equals(Id)).Select(m => new Majors
            {
                Id = m.Id,
                Code = m.Code,
                Name = m.Name
            }).FirstOrDefaultAsync();

            return Majors;
        }
    }
}
