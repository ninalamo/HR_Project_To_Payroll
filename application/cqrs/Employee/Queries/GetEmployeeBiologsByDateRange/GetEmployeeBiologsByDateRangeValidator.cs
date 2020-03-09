using FluentValidation;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeBiologsByDateRangeValidator : AbstractValidator<GetEmployeeBiologsByDateRange_Request>
    {
        public GetEmployeeBiologsByDateRangeValidator()
        {
            RuleFor(i => i.Date1).NotNull();
            RuleFor(i => i.Date2).NotEmpty();
            RuleFor(i => i.EmployeeID).NotEmpty();
            RuleFor(i => i.Date2).GreaterThanOrEqualTo(i => i.Date1);
        }

    }
}
