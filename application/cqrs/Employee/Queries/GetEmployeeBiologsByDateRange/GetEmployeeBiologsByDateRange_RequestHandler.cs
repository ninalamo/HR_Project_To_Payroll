using application.cqrs._base;
using application.interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeBiologsByDateRange_RequestHandler : RequestHandlerBase, IRequestHandler<GetEmployeeBiologsByDateRange_Request, GetEmployeeBiologsByDateRange_Response>
    {
        public GetEmployeeBiologsByDateRange_RequestHandler(IApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }


        public async Task<GetEmployeeBiologsByDateRange_Response> Handle(GetEmployeeBiologsByDateRange_Request request, CancellationToken cancellationToken)
        {
                      
           
                var timeInRecords = await dbContext.RawLogs
                    .Include(i => i.Employee)
                    .Where(i => (
                        (i.Time.Date >= request.Date1.Date && i.Time.Date <= request.Date2.Date) 
                            ||(i.Time.Date == request.Date2.AddDays(1).Date && i.LogType == domain.BiologType.OUT)
                            || (i.Time.Date == request.Date1.AddDays(-1).Date && i.LogType == domain.BiologType.IN))
                       && i.EmployeeID == request.EmployeeID 
                       )
                    .OrderBy(i => i.CreatedOn).ThenBy(i => i.Employee.LastName).ThenBy(i => i.Employee.LastName)
                    .Select(i => new TimeRecord
                    {
                        FullName = $"{i.Employee.FirstName}, {i.Employee.LastName}",
                        EmployeeNumber = i.Employee.EmployeeNumber,
                        Time = i.Time.ToString("MMM dd yyyy hh:mm tt"),
                        Mode = i.LogType.ToString().ToUpper(),
                        Lat = i.Lat,
                        Long = i.Long,
                        Location = i.Location
                    })
                    .AsNoTracking()
                    .ToListAsync();

            
            
            return new GetEmployeeBiologsByDateRange_Response { EmployeeTimeRecords = timeInRecords };

        }
    }

    public class TimeRecord
    {
        public string FullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Time { get; set; }
        public string Mode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location { get; set; }
    }
}
