using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Search.Entities
{
    public class University_Majors_SubjectGroup : DataEntity
    {
        public long Id { get; set; }
        public long University_MajorsId { get; set; }
        public long UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string UniversityCode { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public long SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public string Year { get; set; }
        public decimal? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }
    }

    public class University_Majors_SubjectGroupFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public long? University_MajorsId { get; set; }
        public long? UniversityId { get; set; }
        public StringFilter UniversityName { get; set; }
        public StringFilter UniversityCode { get; set; }
        public long? MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public string Year { get; set; }
        public long? SubjectGroupId { get; set; }
        public StringFilter SubjectGroupCode { get; set; }
        public DecimalFilter Benchmark { get; set; }
        public University_Majors_SubjectGroupOrder OrderBy { get; set; }
        public University_Majors_SubjectGroupFilter() : base()
        {

        }
    }

    public enum University_Majors_SubjectGroupOrder
    {
        Id,
        MajorsCode,
        SubjectGroupCode,
        Benchmark
    }
}
