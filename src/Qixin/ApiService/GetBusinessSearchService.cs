using Qixin.ApiService.Result;
using Qixin.Helper;

namespace Qixin.ApiService
{
    public sealed class GetBusinessSearchService : ApiServiceBase
    {
        public GetBusinessSearchService(string appKey) : base(appKey)
        {
        }

        public QixinResult GetBusinessList(string name)
        {
            var request = HttpWebRequestHelper.Create($"{BaseApiUrl}{string.Format(ApiResource.GetDetailByName, AppKey, name)}", "Get");
            return SendRequest(request);
        }
    }
}