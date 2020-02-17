using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS.Search.Entities;

namespace EMS.Search.Controller.DTO
{
    public class UniversityDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public List<University_Majors_SubjectGroupDTO> University_Majors_SubjectGroups { get; set; } 

        public UniversityDTO() { }
        public UniversityDTO(University university)
        {
            Id = university.Id;
            Code = university.Code;
            Name = university.Name;
            Address = university.Address;
            Website = university.Website;
            Phone = university.Phone;
            Description = university.Description;
        }
    }

    public class UniversityFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Year { get; set; }
        public UniversityOrder OrderBy { get; set; }
        public UniversityFilterDTO() : base()
        {

        }
    }
}
