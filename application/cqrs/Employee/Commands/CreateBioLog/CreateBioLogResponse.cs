using application.cqrs._base;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateBioLogResponse : CommandResponseBase
    {
        public CreateBioLogResponse(string entity, object id) : base(entity, id)
        {
        }
    }
}
