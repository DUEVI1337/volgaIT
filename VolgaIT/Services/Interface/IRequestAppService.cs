namespace VolgaIT.Services.Interface
{
    public interface IRequestAppService
    {
        Task CreateRequest(string appId, Guid eventId, string bonusInfo);
    }
}
