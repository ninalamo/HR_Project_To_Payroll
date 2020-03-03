using application.cqrs.auditTrail;
using application.cqrs.auditTrail.commands;
using application.cqrs.auditTrail.queries;
using application.cqrs.Employee.Commands;
using AutoMapper;
using HR.Application.cqrs.Employee.Commands;
using HR.Application.cqrs.Employee.Queries;
using lib.test.infrastructure;
using persistence;
using Shouldly;
using System;
using System.Linq;
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


            _context.Employees.Add(new domain.Employee
            {
                FirstName = "Janinejams",
                LastName = "Shaunerin",
                CompanyEmail = "janinejams@gmail.com",
                PersonalEmail = "someemail@yahoo.com",
                EmployeeNumber = "13124",
                ID = Guid.Parse("96863F02-CE07-40FC-A2ED-A7FAFB36FEAD"),
            });

            _context.SaveChanges();

            
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
        public async Task GetEmployeeByID()
        {
            var sut = new CreateEmployeeRequestHandler(_context, _mapper);

            var result = await sut.Handle(new CreateEmployeeRequest
            {
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
                      
            var getHandler = new GetEmployeeByIDRequestHandler(_context, _mapper);

            var temp = await getHandler.Handle(new GetEmployeeByIDRequest { EmployeeID = Guid.Parse("96863F02-CE07-40FC-A2ED-A7FAFB36FEAD") }, CancellationToken.None);

            temp.FirstName.ShouldBe("Janinejams");

            var delHandler = new DeleteEmployeeRequestHandler(_context, _mapper);

            await delHandler.Handle(new DeleteEmployeeRequest { EmployeeID = Guid.Parse("96863F02-CE07-40FC-A2ED-A7FAFB36FEAD") }, CancellationToken.None);

        }

        [Fact]
        public async Task CreateEmployeeBiolog()
        {

            var createHandler = new CreateBioLogRequestHandler(_context, _mapper);

            var result = await createHandler.Handle(new CreateBioLogRequest
            {
                EmployeeNumber = "sabin.alessa@outlook.com",
                Lat = 0,
                Location = "N/A",
                LogType = domain.BiologType.IN,
                Long = 0
            }, CancellationToken.None);

            result.ID.ShouldNotBeNull();

        }


    }
}
