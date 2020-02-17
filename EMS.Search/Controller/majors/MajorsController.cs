using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;
using EMS.Search.Controller.DTO;
using EMS.Search.Services.MMajors;
using Microsoft.AspNetCore.Authorization;

namespace EMS.Search.Controller.majors
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
        public async Task<MajorsDTO> Get([FromBody] MajorsDTO majorsDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            var majors = await MajorsService.Get(majorsDTO.Id);

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

            List<Majors> listMajors = await MajorsService.List(majorsFilter);

            List<MajorsDTO> majorsDTOs = listMajors.Select(m => new MajorsDTO(m)).ToList();

            return majorsDTOs;
        }
        #endregion
    }
}
