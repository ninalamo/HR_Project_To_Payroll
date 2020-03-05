using application.cqrs._base;

namespace HR.Application.cqrs.Employee.Commands
{
    public class UpdateEmployee_Response : CommandResponseBase
    {
        public UpdateEmployee_Response(string entity, object id) : base(entity, id)
        {
        }
    }
}
