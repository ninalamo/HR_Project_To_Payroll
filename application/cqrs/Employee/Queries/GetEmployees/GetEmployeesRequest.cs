using application.cqrs._base;
using MediatR;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeesRequest : PagedQueryRequestBase, IRequest<GetEmployeesResponse>
    {
    }
}
