using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Controller.DTO;
using EMS.Search.Entities;
using EMS.Search.Services.MUniversity;
using EMS.Search.Services.MUniversity_Majors;
using EMS.Search.Services.MUniversity_Majors_Majors;

namespace EMS.Search.Controller.university
{
    public class UniversityController : ApiController
    {
        private IUniversityService UniversityService;
        private IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService;

        public UniversityController(IUniversityService UniversityService,
            IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService)
        {
            this.UniversityService = UniversityService;
            this.University_Majors_SubjectGroupService = University_Majors_SubjectGroupService;
        }

        #region Read
        [Route(Route.GetUniversity), HttpPost]
        public async Task<UniversityDTO> Get([FromBody] University_MajorsFilterDTO university_MajorsFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            var univer = await UniversityService.Get(university_MajorsFilterDTO.UniversityId);

            List<University_Majors_SubjectGroup> listUniversity_Majors_SubjectGroup = await University_Majors_SubjectGroupService.List(new University_Majors_SubjectGroupFilter 
            { 
                UniversityId = univer.Id,
                Year = university_MajorsFilterDTO.Year,
                Skip = university_MajorsFilterDTO.Skip,
                Take = university_MajorsFilterDTO.Take,
                OrderBy = University_Majors_SubjectGroupOrder.MajorsCode
            });

            var listUniversity_Majors_SubjectGroupDTO = listUniversity_Majors_SubjectGroup.Select(u => new University_Majors_SubjectGroupDTO
            {
                Id = u.Id,
                MajorsId = u.MajorsId,
                MajorsCode = u.MajorsCode,
                MajorsName = u.MajorsName,
                UniversityId = u.UniversityId,
                UniversityCode = u.UniversityCode,
                UniversityName = u.UniversityName,
                SubjectGroupId = u.SubjectGroupId,
                SubjectGroupCode = u.SubjectGroupCode,
                SubjectGroupName = u.SubjectGroupName,
                Benchmark = u.Benchmark,
                Note = u.Note,
                Quantity = u.Quantity,
                Year = u.Year
            }).OrderByDescending(u => u.Year).ToList();
            if (univer == null) return null;
            else
            {
                var univerDTO = new UniversityDTO(univer);
                univerDTO.University_Majors_SubjectGroups = listUniversity_Majors_SubjectGroupDTO;
                return univerDTO;
            }
        }

        [Route(Route.ListUniversity), HttpPost]
        public async Task<List<UniversityDTO>> List([FromBody] UniversityFilterDTO universityFilterDTO)
        {
            UniversityFilter universityFilter = new UniversityFilter
            {
                Code = universityFilterDTO.Code,
                Name = universityFilterDTO.Name,
                Skip = universityFilterDTO.Skip,
                Take = universityFilterDTO.Take,
                OrderType = universityFilterDTO.OrderType,
                OrderBy = universityFilterDTO.OrderBy
            };

            List<University> universities = await UniversityService.List(universityFilter);

            List<UniversityDTO> universityDTOs = universities.Select(u => new UniversityDTO
            {
                Id = u.Id,
                Address = u.Address,
                Code = u.Code,
                Name = u.Name,
                Website = u.Website,
                Phone = u.Phone,
                Description = u.Description
            }).ToList();

            return universityDTOs;
        }
        #endregion
    }
}
