namespace DataAPI.Controllers
{
    public class StudentsController : GenericController<Models.Student>
    {
        protected override object GetIdentifier(Models.Student resource)
        {
            return resource.Id;
        }
    }
}