using EMS.Search;
using EMS.Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class SubjectGroupDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public SubjectGroupDTO() { }
        public SubjectGroupDTO(SubjectGroup subjectGroup)
        {
            Id = subjectGroup.Id;
            Code = subjectGroup.Code;
            Name = subjectGroup.Name;
        }
    }

    public class SubjectGroupFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public SubjectGroupOrder OrderBy { get; set; }
        public SubjectGroupFilterDTO() : base()
        {

        }
    }
}
