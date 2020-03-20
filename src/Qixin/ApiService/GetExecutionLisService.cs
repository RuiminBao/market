using Qixin.ApiService.Result;
using Qixin.Helper;

namespace Qixin.ApiService
{
    public sealed class GetExecutionLisService : ApiServiceBase
    {
        private readonly int _skip;

        public GetExecutionLisService(string appKey, int skip = 0) : base(appKey)
        {
            _skip = skip;
        }

        public QixinResult GetExecutionList(string name)
        {
            var request = HttpWebRequestHelper.Create($"{BaseApiUrl}{string.Format(ApiResource.GetExecutionListByName, AppKey, name, _skip)}", "Get");
            return SendRequest(request);
        }
    }
}