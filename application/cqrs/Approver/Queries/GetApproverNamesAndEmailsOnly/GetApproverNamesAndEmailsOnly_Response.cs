using application.interfaces.mapping;
using AutoMapper;
using domain;
using System;
using System.Collections.Generic;

namespace HR.Application.cqrs.Approver.Queries
{
    public class GetApproverNamesAndEmailsOnly_Response
    {
        public IEnumerable<GetApproverNamesAndEmailsOnly_Dto> Result { get; internal set; }
    }

    public class GetApproverNamesAndEmailsOnly_Dto : IHaveCustomMapping
    {
        public string FullName { get; internal set; }
        public string Email { get; internal set; }
        public long ApproverID { get; internal set; }
        public string DisplayName { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<domain.Approver, GetApproverNamesAndEmailsOnly_Dto>()
                .ForMember(i => i.FullName, opt => opt.MapFrom(o => $"{o.Employee.LastName}, {o.Employee.FirstName}"))
                .ForMember(i => i.DisplayName, opt => opt.MapFrom(o => $"{o.Employee.LastName}, {o.Employee.FirstName} ({o.Employee.CompanyEmail})"))
                .ForMember(i => i.Email, opt => opt.MapFrom(o => o.Employee.CompanyEmail))
                .ForMember(i => i.ApproverID, opt => opt.MapFrom(o => o.ID));
        }
    }
}