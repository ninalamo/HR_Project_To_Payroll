namespace auth.server.jwt.Models
{
    public class EmailModel
    {
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
        public string FileName { get; set; }
        public string Source { get; set; }
    }
}
