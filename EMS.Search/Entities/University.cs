using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EMS.Search.Entities
{
    public class University : DataEntity
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
    }

    public class UniversityFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public UniversityOrder OrderBy { get; set; }
        public UniversityFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum UniversityOrder
    {
        Id,
        Code,
        Name
    }
}
