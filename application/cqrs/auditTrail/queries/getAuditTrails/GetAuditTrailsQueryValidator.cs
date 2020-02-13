using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.cqrs.auditTrail.queries
{
    public class GetAuditTrailsQueryValidator : AbstractValidator<GetAuditTrailsQuery>
    {
        public GetAuditTrailsQueryValidator()
        {
            RuleFor(x => x.Requestor).NotEmpty();
        }
    }
}
