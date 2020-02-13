using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace application.cqrs.Employee.Commands
{
    public class CreateEmployeeRequestHandler : RequestHandlerBase, IRequestHandler<CreateEmployeeRequest, CreateEmployeeResponse>
    {
        private object requst;

        public CreateEmployeeRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            var employee = new domain.Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmployeeNumber = request.EmployeeNumber,
                IsActive =true
            };

            return new CreateEmployeeResponse(nameof(Employee), employee.ID);
        }
    }
}
