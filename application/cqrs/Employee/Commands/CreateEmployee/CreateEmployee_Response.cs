using application.cqrs._base;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateEmployee_Response : CommandResponseBase
    {
        public CreateEmployee_Response(string entity, object id) : base(entity, id)
        {
        }
    }
}