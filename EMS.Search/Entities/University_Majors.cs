using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Search.Entities
{
    public class University_Majors : DataEntity
    {
        public long Id { get; set; }
        public long UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public long MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string Year { get; set; }
    }

    public class University_MajorsFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public long? UniversityId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter UniversityName { get; set; }
        public long? MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public string Year { get; set; }
        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilter() : base()
        {

        }
    }

    public enum University_MajorsOrder
    {
        Id,
        UniversityCode,
        UniversityName,
        MajorsCode,
        MajorsName,
        Year
    }
}
