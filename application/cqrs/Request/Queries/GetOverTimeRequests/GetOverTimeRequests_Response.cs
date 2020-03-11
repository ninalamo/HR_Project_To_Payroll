using application.interfaces.mapping;
using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HR.Application.cqrs.Request.Queries
{
    public class GetOverTimeRequests_Response
    {
        public GetOverTimeRequests_Response()
        {
            Result = new List<GetOverTimeRequestsResponseDto>();
        }
        public ICollection<GetOverTimeRequestsResponseDto> Result { get; set; }
    }

    public class GetOverTimeRequestsResponseDto : IHaveCustomMapping
    {
        public string Requestor { get; internal set; }
        public string RequestorEmail { get; set; }
        public Guid RequestorID { get; internal set; }
        public long RequestTrackerID { get; set; }
        public Guid EmployeeID { get; set; }
        public DateTimeOffset TimeStart { get; set; }
        public DateTimeOffset TimeEnd { get; set; }
        public string Classification { get; set; }
        public DateTimeOffset ShiftDate { get; set; }
        public string Purpose { get; set; }
        public string ApproverFirstName1 { get; set; }
        public string ApproverLastName1 { get; set; }
        public string ApproverEmail1 { get; set; }
        public bool? ApproverStatus1 { get; set; }
        public string ApproverFirstName2 { get; set; }
        public string ApproverLastName2 { get; set; }
        public string ApproverEmail2 { get; set; }
        public bool? ApproverStatus2 { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<domain.OverTimeRequest, GetOverTimeRequestsResponseDto>()
                .ForMember(i => i.Requestor,
                    opt => opt.MapFrom(o => o.Tracker.Requestor == null ? "" : $"{o.Tracker.Requestor.FirstName} {o.Tracker.Requestor.LastName}"))
                .ForMember(i => i.RequestorEmail,
                    opt => opt.MapFrom(o => o.Tracker.Requestor == null ? "" : o.Tracker.Requestor.CompanyEmail.ToLower()))
                .ForMember(i => i.ApproverEmail1,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 1).Approver.Employee.CompanyEmail))
                .ForMember(i => i.ApproverFirstName1,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 1).Approver.Employee.FirstName))
                .ForMember(i => i.ApproverLastName1,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 1).Approver.Employee.LastName))
                .ForMember(i => i.ApproverStatus1,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 1).Status))
                .ForMember(i => i.ApproverEmail2,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 2).Approver.Employee.CompanyEmail))
                .ForMember(i => i.ApproverFirstName2,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 2).Approver.Employee.FirstName))
                .ForMember(i => i.ApproverLastName2,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 2).Approver.Employee.LastName))
                .ForMember(i => i.ApproverStatus2,
                    opt => opt.MapFrom(o => o.Tracker.ApproverList.FirstOrDefault(i => i.Approver.Level == 2).Status));

        }
    }

   
}