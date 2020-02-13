using application.interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace application.cqrs.auditTrail.queries
{
    public class GetAuditTrailsHandler : IRequestHandler<GetAuditTrailsQuery, GetAuditTrailsResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAuditTrailsHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAuditTrailsResult> Handle(GetAuditTrailsQuery request, CancellationToken cancellationToken)
        {
            return new GetAuditTrailsResult {
                History = await _context.History.ToListAsync(cancellationToken)
            };
        }
    }
}
