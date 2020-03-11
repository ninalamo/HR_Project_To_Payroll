using application.cqrs._base;
using MediatR;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployees_Request : PagedQueryRequestBase, IRequest<GetEmployeesResponse>
    {
    }
}
