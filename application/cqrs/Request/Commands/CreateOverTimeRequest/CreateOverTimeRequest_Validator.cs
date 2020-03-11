using FluentValidation;

namespace HR.Application.cqrs.Request.Commands
{
    public class CreateOverTimeRequest_Validator : AbstractValidator<CreateOverTimeRequest_Request>
    {
        public CreateOverTimeRequest_Validator()
        {
            RuleFor(i => i.FinalApprover).EmailAddress();
            RuleFor(i => i.Supervisor).EmailAddress();
            RuleFor(i => i.Requestor).EmailAddress();
        }
    }
}
