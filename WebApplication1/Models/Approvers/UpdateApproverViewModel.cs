using domain;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Approvers
{
    public class UpdateApproverViewModel
    {
        [Required]
        public int Level { get; set; }
        [Required]
        [Display(Name = "Request Type")]
        public RequestType TypeOfRequest { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public long ApproverID { get; set; }
    }
}
