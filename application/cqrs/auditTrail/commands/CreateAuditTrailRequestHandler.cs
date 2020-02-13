using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace application.cqrs.auditTrail.commands
{
    public class CreateAuditTrailRequestHandler : RequestHandlerBase, IRequestHandler<CreateAuditTrailRequest, CreateAuditTrailResponse>
    {
        public CreateAuditTrailRequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<CreateAuditTrailResponse> Handle(CreateAuditTrailRequest request, CancellationToken cancellationToken)
        {
            var entity = new AuditTrail
            {
                AfterCommit = "Succeeded in testing",
                BeforeCommit = "N/A",
                IsActive = true,
                ObjectID = 9999,
                Table = nameof(AuditTrail),
                Reason = "TEST",
            };

            dbContext.History.Add(entity);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateAuditTrailResponse(nameof(AuditTrail), entity.ID);

        }
    }
}
