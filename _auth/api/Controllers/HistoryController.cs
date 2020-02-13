using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application.cqrs.auditTrail.queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : BaseController
    {

        [HttpGet("get")]
        public async Task<ActionResult<GetAuditTrailsResult>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAuditTrailsQuery()));
        }
    }
}