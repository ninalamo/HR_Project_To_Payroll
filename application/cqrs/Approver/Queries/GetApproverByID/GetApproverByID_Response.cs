using application.interfaces.mapping;
using AutoMapper;
using domain;
using System;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverByID_Response
    {
        public GetApproverByID_Dto Result { get; set; }
    }

    public class GetApproverByID_Dto : IHaveCustomMapping
    {
        public string EmployeeNumber { get; internal set; }
        public string FullName { get; set; }
        public string CompanyEmail { get; internal set; }
        public long ApproverID { get; internal set; }
        public Guid EmployeeID { get; internal set; }
        public RequestType TypeOfRequest { get; internal set; }
        public int Level { get; internal set; }
        public bool IsActive { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<domain.Approver, GetApproverByID_Dto>()
               .ForMember(i => i.FullName, opt => opt.MapFrom(o => $"{o.Employee.FirstName} {o.Employee.LastName}"))
               .ForMember(i => i.ApproverID, opt => opt.MapFrom(o => o.ID))
               .ForMember(i => i.CompanyEmail, opt => opt.MapFrom(o => o.Employee.CompanyEmail))
               .ForMember(i => i.EmployeeID, opt => opt.MapFrom(o => o.Employee.ID))
               .ForMember(i => i.EmployeeNumber, opt => opt.MapFrom(o => o.Employee.EmployeeNumber))
               .ForMember(i => i.TypeOfRequest, opt => opt.MapFrom(o => o.TypeOfRequest));
        }
    }
}