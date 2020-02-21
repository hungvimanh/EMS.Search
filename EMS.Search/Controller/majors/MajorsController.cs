using EMS.Search;
using EMS.Search.Controller;
using EMS.Search.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MMajors;

namespace TwelveFinal.Controller.majors
{
    public class MajorsController : ApiController
    {
        private IMajorsService MajorsService;

        public MajorsController(IMajorsService majorsService)
        {
            this.MajorsService = majorsService;
        }

        #region Read
        [Route(Route.GetMajors), HttpPost]
        public async Task<MajorsDTO> Get([FromBody] MajorsDTO MajorsDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);
            var majors = await MajorsService.Get(MajorsDTO.Id);

            return majors == null ? null : new MajorsDTO(majors);
        }

        [Route(Route.ListMajors), HttpPost]
        public async Task<List<MajorsDTO>> List([FromBody] MajorsFilterDTO majorsFilterDTO)
        {
            MajorsFilter majorsFilter = new MajorsFilter
            {
                Code = majorsFilterDTO.Code,
                Name = majorsFilterDTO.Name,
                Skip = majorsFilterDTO.Skip,
                Take = majorsFilterDTO.Take,
                OrderBy = majorsFilterDTO.OrderBy,
                OrderType = majorsFilterDTO.OrderType
            };

            List<Majors> universities = await MajorsService.List(majorsFilter);

            List<MajorsDTO> majorsDTOs = universities.Select(m => new MajorsDTO(m)).ToList();

            return majorsDTOs;
        }
        #endregion
    }
}
