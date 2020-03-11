using FluentValidation;

namespace HR.Application.cqrs.Approver.Commands
{
    public class CreateApprover_Validator : AbstractValidator<CreateApprover_Request>
    {
        public CreateApprover_Validator()
        {
            RuleFor(i => i.CompanyEmail).EmailAddress();
            RuleFor(i => i.Level).GreaterThan(-1);
            RuleFor(i => i.TypeOfRequest).NotEmpty();
        }
    }
}