﻿using EMS.Search.Controller.DTO;
using EMS.Search.Entities;
using EMS.Search.Services.MUniversity_Majors;
using EMS.Search.Services.MUniversity_Majors_Majors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Search.Controller.university_majors
{
    public class University_MajorsController : ApiController
    {
        private IUniversity_MajorsService university_MajorsService;
        private IUniversity_Majors_SubjectGroupService university_Majors_SubjectGroupService;
        public University_MajorsController(IUniversity_MajorsService university_MajorsService, IUniversity_Majors_SubjectGroupService university_Majors_SubjectGroupService)
        {
            this.university_MajorsService = university_MajorsService;
            this.university_Majors_SubjectGroupService = university_Majors_SubjectGroupService;
        }

        #region Read
        [Route(Route.GetUniversity_Majors), HttpPost]
        public async Task<University_MajorsDTO> Get([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState); 
            var university_Majors = await university_MajorsService.Get(university_MajorsDTO.Id);

            if (university_Majors == null) return null;

            var university_Majors_SubjectGroups = await university_Majors_SubjectGroupService.List(new University_Majors_SubjectGroupFilter
            {
                UniversityId = new IdFilter { Equal = university_MajorsDTO.UniversityId },
                MajorsId = new IdFilter { Equal = university_MajorsDTO.MajorsId },
                Year = university_MajorsDTO.Year
            });
            university_MajorsDTO = new University_MajorsDTO(university_Majors);
            university_MajorsDTO.university_Majors_SubjectGroupDTO = university_Majors_SubjectGroups.Select(u => new University_Majors_SubjectGroupDTO(u)).ToList();
            return university_MajorsDTO;
        }

        [Route(Route.ListUniversity_Majors), HttpPost]
        public async Task<List<University_MajorsDTO>> List([FromBody] University_MajorsFilterDTO university_MajorsFilterDTO)
        {
            University_MajorsFilter university_MajorsFilter = new University_MajorsFilter
            {
                UniversityId = university_MajorsFilterDTO.UniversityId,
                UniversityCode = university_MajorsFilterDTO.UniversityCode,
                UniversityName = university_MajorsFilterDTO.UniversityName,
                MajorsId = university_MajorsFilterDTO.MajorsId,
                MajorsCode = university_MajorsFilterDTO.MajorsCode,
                MajorsName = university_MajorsFilterDTO.MajorsName,
                Year = university_MajorsFilterDTO .Year,
                Skip = university_MajorsFilterDTO.Skip,
                Take = university_MajorsFilterDTO.Take,
                OrderType = university_MajorsFilterDTO.OrderType,
                OrderBy = university_MajorsFilterDTO.OrderBy
            };

            List<University_Majors> universities = await university_MajorsService.List(university_MajorsFilter);

            List<University_MajorsDTO> university_MajorsDTOs = universities.Select(u => new University_MajorsDTO(u)).ToList();

            return university_MajorsDTOs;
        }
        #endregion
    }
}
