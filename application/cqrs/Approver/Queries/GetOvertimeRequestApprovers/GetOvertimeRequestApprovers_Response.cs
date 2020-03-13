using application.interfaces.mapping;
using AutoMapper;
using domain;
using System;
using System.Collections.Generic;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetOvertimeRequestApprovers_Response
    {
        public IEnumerable<GetOvertimeApprovers_Dto> Approvers { get; internal set; }
    }

 

    public class GetOvertimeApprovers_Dto : IHaveCustomMapping
    {
        public string EmployeeNumber { get; internal set; }
        public string CompanyEmail { get; internal set; }
        public long ApproverID { get; internal set; }
        public Guid EmployeeID { get; internal set; }
        public RequestType TypeOfRequest { get; internal set; }
        public int Level { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<domain.Approver, GetOvertimeApprovers_Dto>()
               .ForMember(i => i.FullName, opt => opt.MapFrom(o => $"{o.Employee.FirstName} {o.Employee.LastName}"))
               .ForMember(i => i.ApproverID, opt => opt.MapFrom(o => o.ID))
               .ForMember(i => i.CompanyEmail, opt => opt.MapFrom(o => o.Employee.CompanyEmail))
               .ForMember(i => i.EmployeeID, opt => opt.MapFrom(o => o.Employee.ID))
               .ForMember(i => i.EmployeeNumber, opt => opt.MapFrom(o => o.Employee.EmployeeNumber))
               .ForMember(i => i.TypeOfRequest, opt => opt.MapFrom(o => o.TypeOfRequest));
        }
    }
}