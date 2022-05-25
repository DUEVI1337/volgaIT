using MailKit.Net.Smtp;
using MimeKit;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string emailUser, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("VolgaIt", "volgaitegor@gmail.com"));
            emailMessage.To.Add(new MailboxAddress ("", emailUser));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message,
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("volgaitegor@gmail.com", "eihqnosbwachqfpv");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}