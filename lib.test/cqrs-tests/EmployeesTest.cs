using application.cqrs.auditTrail;
using application.cqrs.auditTrail.commands;
using application.cqrs.auditTrail.queries;
using application.cqrs.Employee.Commands;
using AutoMapper;
using lib.test.infrastructure;
using persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace lib.test.cqrs_tests
{
    [Collection("QueryCollection")]
    public class EmployeesTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeesTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

       

        [Fact]
        public async Task CreateEmployee()
        {
            var sut = new CreateEmployeeRequestHandler(_context, _mapper);

            var result = await sut.Handle(new CreateEmployeeRequest {
                CompanyEmail = "nino.alamo@kmc.solutions",
                EmployeeNumber = "1234",
                FirstName = "Nin",
                LastName = "Alamo",
                PersonalEmail = "janinejams@gmail.com",
            }, CancellationToken.None);

            result.ShouldBeOfType(typeof(CreateEmployeeResponse));
                                                                                        
            result.Entity.ShouldBe("Employee");
        }

        [Fact]
        public async Task DeleteEmployee()
        {
            var sut = new CreateEmployeeRequestHandler(_context, _mapper);

            var result = await sut.Handle(new CreateEmployeeRequest
            {
                CompanyEmail = "nino.alamo@kmc.solutions",
                EmployeeNumber = "1234",
                FirstName = "Nino Francisco",
                LastName = "Alamo",
                PersonalEmail = "janinejams@gmail.com",
            }, CancellationToken.None);

            result.ShouldBeOfType(typeof(CreateEmployeeResponse));

            result.Entity.ShouldBe("Employee");
        }


    }
}
