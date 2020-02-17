using System.Collections.Generic;

namespace EMS.Search
{
    public class DataDTO
    {
        public Dictionary<string, string> Errors;
    }

    public class FilterDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public OrderType OrderType { get; set; }

        public FilterDTO()
        {
            Skip = 0;
            Take = 10;
            OrderType = OrderType.ASC;
        }
    }
}
