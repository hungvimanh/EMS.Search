using EMS.Search;
using EMS.Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class MajorsDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public MajorsDTO() { }
        public MajorsDTO(Majors majors)
        {
            Id = majors.Id;
            Code = majors.Code;
            Name = majors.Name;
        }
    }

    public class MajorsFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public MajorsOrder OrderBy { get; set; }
        public MajorsFilterDTO() : base()
        {

        }
    }
}
