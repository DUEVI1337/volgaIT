namespace VolgaIT.Services.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string emailUser, string subject, string message);
    }
}
