using DataAPI.Models;

namespace DataAPI.Controllers
{

    public class GradesController : GenericController<Models.Grade>
    {
        protected override object GetIdentifier(Grade resource)
        {
            return resource.Id;
        }
    }
}