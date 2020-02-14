using application.cqrs._base;
using application.exceptions;
using application.interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Commands
{
    public class DeleteEmployeeRequestHandler : RequestHandlerBase, IRequestHandler<DeleteEmployeeRequest>
    {
        public DeleteEmployeeRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<Unit> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees.FindAsync(request.EmployeeID);

            if (employee == null) throw new NotFoundException(nameof(Employee), request.EmployeeID);

            dbContext.Employees.Remove(employee);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
