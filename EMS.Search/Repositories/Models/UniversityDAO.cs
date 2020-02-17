using System;
using System.Collections.Generic;

namespace EMS.Search.Repositories.Models
{
    public partial class UniversityDAO
    {
        public UniversityDAO()
        {
            University_Majors = new HashSet<University_MajorsDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Descreption { get; set; }

        public virtual ICollection<University_MajorsDAO> University_Majors { get; set; }
    }
}
