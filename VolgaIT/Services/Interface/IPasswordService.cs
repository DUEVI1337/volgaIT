using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IPasswordService
    {
        Task<string> GenerateTokenPasswordResetAsync(ForgotPasswordViewModel model);
        Task SendEmailResetPassword(string email, string callbackUrl);
        Task ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
