using application.interfaces.mapping;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeBiologsByDateRange_Response
    {
       
        public IList<TimeRecord> EmployeeTimeRecords { get; set; }
    }

    public class GetEmployeeBiologsByDateRangeDto : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<TimeRecord, GetEmployeeBiologsByDateRangeDto>();
        }
    }
}