using EMS.Search.Entities;
using System.Collections.Generic;

namespace EMS.Search.Controller.DTO
{
    public class University_MajorsDTO : DataDTO
    {
        public long Id { get; set; }
        public long? UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public long? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }
        public List<University_Majors_SubjectGroupDTO> university_Majors_SubjectGroupDTO { get; set; }

        public University_MajorsDTO() { }

        public University_MajorsDTO(University_Majors university_Majors)
        {
            Id = university_Majors.Id;
            UniversityId = university_Majors.UniversityId;
            UniversityCode = university_Majors.UniversityCode;
            UniversityName = university_Majors.UniversityName;
            MajorsId = university_Majors.MajorsId;
            MajorsCode = university_Majors.MajorsCode;
            MajorsName = university_Majors.MajorsName;
            Year = university_Majors.Year;
        }
    }

    public class University_MajorsFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter UniversityId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter UniversityName { get; set; }
        public IdFilter MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public string Year { get; set; }

        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilterDTO() : base()
        {

        }
    }
}
