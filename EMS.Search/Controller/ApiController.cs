using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Search.Controller
{
    public class Root
    {
        public const string Base = "api/Search";
    }

    public class Route : Root
    {
        public const string GetUniversity = Base + "/university/get";
        public const string ListUniversity = Base + "/university/list";

        public const string GetMajors = Base + "/majors/get";
        public const string ListMajors = Base + "/majors/list";

        public const string GetUniversity_Majors = Base + "/um/get";
        public const string ListUniversity_Majors = Base + "/um/list";

        public const string GetUniversity_Majors_SubjectGroup = Base + "/ums/get";
        public const string ListUniversity_Majors_SubjectGroup = Base + "/ums/list";
    }

    [AllowAnonymous]
    //[Authorize(Policy = "Permission")]
    public class ApiController : ControllerBase
    {

    }
}
