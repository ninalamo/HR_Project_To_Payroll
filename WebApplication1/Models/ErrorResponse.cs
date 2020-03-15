using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Models
{
    public class ErrorResponse
    {
        private readonly List<string> _messages;
        public ErrorResponse()
        {
            _messages = new List<string>();
        }

        public void AddError(string[] errors)
        {
            _messages.AddRange(errors);
        }

        public void AddError(string error)
        {
            AddError(new[] { error });
        }
        public string Message => string.Join("|", _messages.Select(i => i).ToArray());
    }
}
