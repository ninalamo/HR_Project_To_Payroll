using application.interfaces.mapping;
using AutoMapper;
using System;
using System.Linq.Expressions;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByIDResponse
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public bool IsActive { get; set; }

        private static readonly Expression<Func<domain.Employee, GetEmployeeByIDResponse>> Projection = (e) =>
        new GetEmployeeByIDResponse
            {
                CompanyEmail = e.CompanyEmail,
                EmployeeNumber = e.EmployeeNumber,
                FirstName = e.FirstName,
                LastName = e.LastName,
                PersonalEmail = e.PersonalEmail,
                EmployeeID = e.ID,
                IsActive = e.IsActive,
                FullName = $"{e.LastName}, {e.FirstName}"
        };
    
        public static GetEmployeeByIDResponse Create(domain.Employee e) => Projection.Compile().Invoke(e);
    }

   
}