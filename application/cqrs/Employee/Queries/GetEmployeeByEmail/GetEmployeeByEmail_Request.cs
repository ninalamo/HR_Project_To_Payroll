using MediatR;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByEmail_Request : IRequest<GetEmployeeByEmail_Response>
    {
        public string CompanyEmail { get; set; }
    }
}