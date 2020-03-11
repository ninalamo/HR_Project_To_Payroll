using domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Approvers
{
    public class CreateApproverViewModel
    {
        [Required]
        [Display(Name ="Employee")]
        public string CompanyEmail { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        [Display(Name ="Request Type")]
        public RequestType TypeOfRequest { get; set; }
    }
}
