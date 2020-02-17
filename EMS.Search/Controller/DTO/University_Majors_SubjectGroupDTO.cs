using EMS.Search.Entities;

namespace EMS.Search.Controller.DTO
{
    public class University_Majors_SubjectGroupDTO : DataDTO
    {
        public long Id { get; set; }
        public long? University_MajorsId { get; set; }
        public long? UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public long? MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public long? SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public string Year { get; set; }
        public decimal? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }

        public University_Majors_SubjectGroupDTO() { }
        public University_Majors_SubjectGroupDTO(University_Majors_SubjectGroup university_Majors_SubjectGroup)
        {
            Id = university_Majors_SubjectGroup.Id;
            University_MajorsId = university_Majors_SubjectGroup.University_MajorsId;
            UniversityId = university_Majors_SubjectGroup.UniversityId;
            UniversityCode = university_Majors_SubjectGroup.UniversityCode;
            UniversityName = university_Majors_SubjectGroup.UniversityName;
            MajorsId = university_Majors_SubjectGroup.MajorsId;
            MajorsCode = university_Majors_SubjectGroup.MajorsCode;
            MajorsName = university_Majors_SubjectGroup.MajorsName;
            SubjectGroupId = university_Majors_SubjectGroup.SubjectGroupId;
            SubjectGroupCode = university_Majors_SubjectGroup.SubjectGroupCode;
            SubjectGroupName = university_Majors_SubjectGroup.SubjectGroupName;
            Year = university_Majors_SubjectGroup.Year;
            Benchmark = university_Majors_SubjectGroup.Benchmark;
            Quantity = university_Majors_SubjectGroup.Quantity;
            Note = university_Majors_SubjectGroup.Note;
        }
    }

    public class University_Majors_SubjectGroupFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter University_MajorsId { get; set; }
        public string Year { get; set; }
        public IdFilter UniversityId { get; set; }
        public StringFilter UniversityName { get; set; }
        public StringFilter UniversityCode { get; set; }
        public IdFilter MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public DecimalFilter Benchmark { get; set; }
        public IdFilter SubjectGroupId { get; set; }
        public StringFilter SubjectGroupCode { get; set; }
        public University_Majors_SubjectGroupOrder OrderBy { get; set; }
        public University_Majors_SubjectGroupFilterDTO(): base()
        {

        }
    }
}
