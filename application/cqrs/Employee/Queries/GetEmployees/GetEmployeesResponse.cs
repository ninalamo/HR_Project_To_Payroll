﻿using application.cqrs._base;
using application.interfaces.mapping;
using AutoMapper;
using System;
using System.Collections.Generic;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeesResponse : PagedQueryResponseBase<GetEmployeesDto>
    {
        public GetEmployeesResponse(IEnumerable<GetEmployeesDto> items, int pageNo, int pageSize, long totalRecordCount) : base(items, pageNo, pageSize, totalRecordCount)
        {
        }
    }

    public class GetEmployeesDto : IHaveCustomMapping
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string ReportsTo { get; set; }
        public bool CanApprove { get; set; }
        public bool IsActive { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<domain.Employee, GetEmployeesDto>()
                .ForMember(i => i.EmployeeID, opt => opt.MapFrom(o => o.ID))
                .ForMember(i => i.FullName, opt => opt.MapFrom(o => $"{o.FirstName} {o.LastName}"));
        }
    }
}