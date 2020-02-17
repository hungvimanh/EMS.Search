using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EMS.Search
{
    public class FilterEntity
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }

        public FilterEntity()
        {
            Skip = 0;
            Take = 10;
            OrderType = OrderType.ASC;
        }
    }

    public enum OrderType
    {
        ASC,
        DESC
    }
}
