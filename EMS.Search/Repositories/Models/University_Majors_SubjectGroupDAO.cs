using System;
using System.Collections.Generic;

namespace EMS.Search.Repositories.Models
{
    public partial class University_Majors_SubjectGroupDAO
    {
        public long Id { get; set; }
        public long University_MajorsId { get; set; }
        public decimal? Benchmark { get; set; }
        public int? Quantity { get; set; }
        public string Note { get; set; }
        public long SubjectGroupId { get; set; }

        public virtual SubjectGroupDAO SubjectGroup { get; set; }
        public virtual University_MajorsDAO University_Majors { get; set; }
    }
}
