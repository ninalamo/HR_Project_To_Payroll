using auth.api.Models.Email;
using lib.common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace auth.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly AppSettings _appSettings;

        public EmailController(IOptions<AppSettings> appSettings )
        {
            _appSettings = appSettings.Value;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailModel model)
        {
            try
            {
                var apiKey = _appSettings.SendGridApiKey;
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(model.Sender.Email, model.Sender.Name);
                var to = new EmailAddress(model.Recipient.Email, model.Recipient.Name);
                var msg = MailHelper.CreateSingleTemplateEmail(from, to, model.TemplateID, JsonConvert.DeserializeObject(model.dynamic_template_data.ToString(), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                var response = await client.SendEmailAsync(msg);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
