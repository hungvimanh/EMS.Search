using System;
using System.Collections.Generic;

namespace EMS.Search.Repositories.Models
{
    public partial class University_MajorsDAO
    {
        public University_MajorsDAO()
        {
            University_Majors_SubjectGroups = new HashSet<University_Majors_SubjectGroupDAO>();
        }

        public long Id { get; set; }
        public long UniversityId { get; set; }
        public long MajorsId { get; set; }
        public string Year { get; set; }

        public virtual MajorsDAO Majors { get; set; }
        public virtual UniversityDAO University { get; set; }
        public virtual ICollection<University_Majors_SubjectGroupDAO> University_Majors_SubjectGroups { get; set; }
    }
}
