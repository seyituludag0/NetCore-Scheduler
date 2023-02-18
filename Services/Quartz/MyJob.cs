using MimeKit;
using Quartz;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace API.Services.Quartz
{
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            MessageMail message = new MessageMail();
            message.Subject = "Serverde Günlük Mail Gönderim Testi";
            message.Body = "Teeeeeeeeeeeeeeeeeeeeeeeeeessssssssssssssssssssttttttttttt";

            List<string> savedMails = new List<string>() { "seuludag@multi.eu", "seuludag@property.tech" };
            message.To = savedMails;

            SendEmail(message);
            return Task.CompletedTask;
        }
        public void SendEmail(MessageMail messageMail)
        {
            Console.WriteLine("Hiiiiiiiiii");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("MultiMedia PTS TEST", "multimedia@property.tech"));

            foreach (var address in messageMail.To)
            {
                message.To.Add(new MailboxAddress("Receiver", address));
            }

            message.Subject = messageMail.Subject;

            message.Body = new TextPart("plain")
            {
                Text = messageMail.Body
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.office365.com", 587, false);
                client.Authenticate("multimedia@property.tech", "@Pud369123!");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
