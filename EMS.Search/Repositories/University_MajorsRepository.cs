using EMS.Search.Entities;
using EMS.Search.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Search.Repositories
{
    public interface IUniversity_MajorsRepository
    {
        Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter);
        Task<int> Count(University_MajorsFilter university_MajorsFilter);
        Task<University_Majors> Get(long Id);
    }
    public class University_MajorsRepository : IUniversity_MajorsRepository
    {
        private readonly EMSContext eMSContext;
        public University_MajorsRepository(EMSContext _eMSContext)
        {
            eMSContext = _eMSContext;
        }

        private IQueryable<University_MajorsDAO> DynamicFilter(IQueryable<University_MajorsDAO> query, University_MajorsFilter university_MajorsFilter)
        {
            if (university_MajorsFilter == null)
                return query.Where(q => 1 == 0);
            if (university_MajorsFilter.Year != null)
                query = query.Where(q => q.Year.Equals(university_MajorsFilter.Year));

            if (university_MajorsFilter.UniversityId != null)
                query = query.Where(q => q.UniversityId, university_MajorsFilter.UniversityId);
            if (university_MajorsFilter.MajorsId != null)
                query = query.Where(q => q.MajorsId, university_MajorsFilter.MajorsId);

            if (university_MajorsFilter.MajorsCode != null)
                query = query.Where(q => q.Majors.Code, university_MajorsFilter.MajorsCode);
            if (university_MajorsFilter.MajorsName != null)
                query = query.Where(q => q.Majors.Name, university_MajorsFilter.MajorsName);
           
            if (university_MajorsFilter.UniversityCode != null)
                query = query.Where(q => q.University.Code, university_MajorsFilter.UniversityCode);
            if (university_MajorsFilter.UniversityName != null)
                query = query.Where(q => q.University.Name, university_MajorsFilter.UniversityName);
            
            return query;
        }
        private IQueryable<University_MajorsDAO> DynamicOrder(IQueryable<University_MajorsDAO> query, University_MajorsFilter university_MajorsFilter)
        {
            switch (university_MajorsFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (university_MajorsFilter.OrderBy)
                    {
                        case University_MajorsOrder.MajorsCode:
                            query = query.OrderBy(q => q.Majors.Code);
                            break;
                        case University_MajorsOrder.MajorsName:
                            query = query.OrderBy(q => q.Majors.Name);
                            break;
                        case University_MajorsOrder.UniversityCode:
                            query = query.OrderBy(q => q.University.Code);
                            break;
                        case University_MajorsOrder.UniversityName:
                            query = query.OrderBy(q => q.University.Name);
                            break;
                        case University_MajorsOrder.Year:
                            query = query.OrderBy(q => q.Year);
                            break;
                        default:
                            query = query.OrderBy(q => q.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (university_MajorsFilter.OrderBy)
                    {
                        case University_MajorsOrder.MajorsCode:
                            query = query.OrderByDescending(q => q.Majors.Code);
                            break;
                        case University_MajorsOrder.MajorsName:
                            query = query.OrderByDescending(q => q.Majors.Name);
                            break;
                        case University_MajorsOrder.UniversityCode:
                            query = query.OrderByDescending(q => q.University.Code);
                            break;
                        case University_MajorsOrder.UniversityName:
                            query = query.OrderByDescending(q => q.University.Name);
                            break;
                        case University_MajorsOrder.Year:
                            query = query.OrderByDescending(q => q.Year);
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
            query = query.Skip(university_MajorsFilter.Skip).Take(university_MajorsFilter.Take);
            return query;
        }
        private async Task<List<University_Majors>> DynamicSelect(IQueryable<University_MajorsDAO> query)
        {

            List<University_Majors> university_Majorss = await query.Select(q => new University_Majors()
            {
                Id = q.Id,
                MajorsId = q.MajorsId,
                MajorsCode = q.Majors.Code,
                MajorsName = q.Majors.Name,
                UniversityId = q.UniversityId,
                UniversityCode = q.University.Code,
                UniversityName = q.University.Name,
                Year = q.Year
            }).ToListAsync();
            return university_Majorss;
        }

        public async Task<int> Count(University_MajorsFilter university_MajorsFilter)
        {
            IQueryable<University_MajorsDAO> university_MajorsDAOs = eMSContext.University_Majors;
            university_MajorsDAOs = DynamicFilter(university_MajorsDAOs, university_MajorsFilter);
            return await university_MajorsDAOs.CountAsync();
        }

        public async Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter)
        {
            if (university_MajorsFilter == null) return new List<University_Majors>();
            IQueryable<University_MajorsDAO> university_MajorsDAOs = eMSContext.University_Majors;
            university_MajorsDAOs = DynamicFilter(university_MajorsDAOs, university_MajorsFilter);
            university_MajorsDAOs = DynamicOrder(university_MajorsDAOs, university_MajorsFilter);
            var university_Majorss = await DynamicSelect(university_MajorsDAOs);
            return university_Majorss;
        }


        public async Task<University_Majors> Get(long Id)
        {
            University_Majors University_Majors = await eMSContext.University_Majors.Where(m => m.Id == Id).Select(u => new University_Majors
            {
                Id = u.Id,
                UniversityId = u.UniversityId,
                UniversityCode = u.University.Code,
                UniversityName = u.University.Name,
                MajorsId = u.MajorsId,
                MajorsCode = u.Majors.Code,
                MajorsName = u.Majors.Name,
                Year = u.Year
            }).FirstOrDefaultAsync();

            return University_Majors;
        }
    }
}
