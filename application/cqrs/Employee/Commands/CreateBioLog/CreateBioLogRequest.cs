using domain;
using MediatR;
using System;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateBioLogRequest : IRequest<CreateBioLogResponse>
    {
        public string Email { get; set; }
        public BiologType LogType { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location { get; set; }
    }
}
