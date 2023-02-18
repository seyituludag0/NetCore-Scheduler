namespace API
{
    public class MessageMail
    {
        public MessageMail(List<string> to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
        public MessageMail()
        {

        }
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
