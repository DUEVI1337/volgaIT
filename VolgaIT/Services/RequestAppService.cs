using VolgaIT.Data.Repository.Interface;
using VolgaIT.Models;
using VolgaIT.Services.Interface;

namespace VolgaIT.Services
{
    public class RequestAppService : IRequestAppService
    {
        private IRequestRepository _repoRequest;

        public RequestAppService(IRequestRepository repoRequest)
        {
            _repoRequest = repoRequest;
        }

        public async Task CreateRequest(string appId, Guid eventId, string bonusInfo)
        {
                RequestUser requestUser = new RequestUser { AppId = appId, EventId = eventId, BonusInfo = bonusInfo };
                await _repoRequest.AddRequsetAsync(requestUser);
                await _repoRequest.SaveAsync();
        }
    }
}
