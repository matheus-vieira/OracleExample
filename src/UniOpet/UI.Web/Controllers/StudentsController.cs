namespace UI.Web.Controllers
{
    public class StudentsController : GenericController<Models.Student>
    {
        public StudentsController(Services.IGenericApiService<Models.Student> service)
            : base(service)
        {
        }
    }
}
