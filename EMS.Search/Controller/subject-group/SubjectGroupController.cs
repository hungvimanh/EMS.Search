using EMS.Search;
using EMS.Search.Controller;
using EMS.Search.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MSubjectGroup;

namespace TwelveFinal.Controller.subject_group
{
    public class SubjectGroupController : ApiController
    {
        private ISubjectGroupService SubjectGrupService;
        public SubjectGroupController(ISubjectGroupService SubjectGrupService)
        {
            this.SubjectGrupService = SubjectGrupService;
        }

        #region Read
        [Route(Route.GetSubjectGroup), HttpPost]
        public async Task<SubjectGroupDTO> Get([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (!ModelState.IsValid)
                throw new MessageException(ModelState);

            var subjectGroup = await SubjectGrupService.Get(subjectGroupDTO.Id);
            return subjectGroup == null ? null : new SubjectGroupDTO(subjectGroup);
        }

        [Route(Route.ListSubjectGroup), HttpPost]
        public async Task<List<SubjectGroupDTO>> List([FromBody] SubjectGroupFilterDTO subjectGroupFilterDTO)
        {
            SubjectGroupFilter filter = new SubjectGroupFilter
            {
                Code = subjectGroupFilterDTO.Code,
                Name = subjectGroupFilterDTO.Name,
                Skip = subjectGroupFilterDTO.Skip,
                Take = subjectGroupFilterDTO.Take,
                OrderBy = subjectGroupFilterDTO.OrderBy,
                OrderType = subjectGroupFilterDTO.OrderType
            };

            List<SubjectGroup> subjectGroups = await SubjectGrupService.List(filter);

            List<SubjectGroupDTO> subjectGroupDTOs = subjectGroups.Select(s => new SubjectGroupDTO(s)).ToList();

            return subjectGroupDTOs;
        }
        #endregion
    }
}
