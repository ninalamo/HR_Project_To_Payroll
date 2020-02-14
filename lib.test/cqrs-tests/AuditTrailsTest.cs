using application.cqrs.auditTrail;
using application.cqrs.auditTrail.commands;
using application.cqrs.auditTrail.queries;
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
    public class AuditTrailsTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuditTrailsTest(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAuditTrails()
        {
            var sut = new GetAuditTrailsHandler(_context, _mapper);

            var result = await sut.Handle(new GetAuditTrailsQuery(), CancellationToken.None);

            result.ShouldBeOfType<GetAuditTrailsResult>();

           

        }

        [Fact]
        public async Task CreateAuditTrail()
        {
            var sut = new CreateAuditTrailRequestHandler(_context, _mapper);

            var result = await sut.Handle(new CreateAuditTrailRequest(), CancellationToken.None);

            result.ShouldBeOfType(typeof(CreateAuditTrailResponse));
                                                                                                                                                                 
         
        }
    }
}
