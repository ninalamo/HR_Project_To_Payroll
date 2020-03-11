using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Requests.Overtime
{
    public class CreateOverTimeRequestViewModel
    {
        [Required]
        public string Classification { get; set; }
        [Required]
        public string Purpose { get; set; }
        [Required]
        public DateTimeOffset ShiftDate { get; set; }
        [Required]
        public DateTimeOffset TimeStart { get; set; }
        [Required]
        public DateTimeOffset TimeEnd { get; set; }
    }
}
