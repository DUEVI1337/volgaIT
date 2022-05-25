using VolgaIT.Models;

namespace VolgaIT.Services.Interface
{
    public interface IPasswordService
    {
        Task<string> GenerateTokenPasswordResetAsync(ForgotPasswordViewModel model);
        Task ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
