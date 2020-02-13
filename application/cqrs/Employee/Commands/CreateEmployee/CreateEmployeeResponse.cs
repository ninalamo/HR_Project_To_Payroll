using application.cqrs._base;

namespace application.cqrs.Employee.Commands
{
    public class CreateEmployeeResponse : CommandResponseBase
    {
        public CreateEmployeeResponse(string entity, object id) : base(entity, id)
        {
        }
    }
}