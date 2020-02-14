using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByIDRequestHandler : RequestHandlerBase, IRequestHandler<GetEmployeeByIDRequest, GetEmployeeByIDResponse>
    {
        public GetEmployeeByIDRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<GetEmployeeByIDResponse> Handle(GetEmployeeByIDRequest request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FindAsync(request.EmployeeID);

            if (employee == null) throw new NotFoundException(nameof(domain.Employee), request.EmployeeID);

            return GetEmployeeByIDResponse.Create(employee);
        }
    }
}
