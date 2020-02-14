using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace auth.api.Models.Email
{
    public class EmailModel
    {
        public EmailAddressFormat Sender { get; set; }
        public EmailAddressFormat Recipient { get; set; }
        public string TemplateID { get; set; }
        public object dynamic_template_data { get; set; }
    }

    public class EmailAddressFormat
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
