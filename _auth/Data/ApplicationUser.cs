using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace auth.api.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? AzureId { get; set; }
        //public DateTimeOffset CreatedOn { get; set; }
        //public DateTimeOffset LastModifedOn { get; set; }
        //public string ApiKey { get; set; }
    }

   
}