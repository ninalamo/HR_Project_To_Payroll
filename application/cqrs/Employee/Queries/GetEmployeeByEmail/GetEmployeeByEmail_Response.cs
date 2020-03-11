using System;
using System.Linq.Expressions;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByEmail_Response
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public bool IsActive { get; set; }
        public Guid EmployeeID { get; set; }
        public string ReportsTo { get; set; }

        private static readonly Expression<Func<domain.Employee, GetEmployeeByEmail_Response>> Projection = (e) =>
        new GetEmployeeByEmail_Response
        {
            CompanyEmail = e.CompanyEmail,
            EmployeeNumber = e.EmployeeNumber,
            FirstName = e.FirstName,
            LastName = e.LastName,
            PersonalEmail = e.PersonalEmail,
            IsActive = e.IsActive,
            FullName = $"{e.LastName}, {e.FirstName}",
            EmployeeID = e.ID,
            ReportsTo = e.ReportsTo
        };

        public static GetEmployeeByEmail_Response Create(domain.Employee e) => Projection.Compile().Invoke(e);
    }

}