using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Biologs
{
    public class DailyTimeRecordResponseViewModel
    {
        public string FullName { get; set; }
        public string EmployeeNumber { get; set; }
        public string Time { get; set; }
        public string Mode { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Location { get; set; }
    }

    public class DailyTimeRecordRequestViewModel
    {
        [Required]
        public DateTimeOffset Date1 { get; set; }
        [Required]
        public DateTimeOffset Date2 { get; set; }
        [Required]
        public Guid EmployeeID { get; set; }

        public string FullName { get; set; }
        public string EmployeeNumber { get; set; }

    }

    
}
