using Qixin.ApiService.Result;
using Qixin.Helper;

namespace Qixin.ApiService
{
    public sealed class GetAbnormalListService : ApiServiceBase
    {
        private readonly int _skip;
        public GetAbnormalListService(string appKey, int skip) : base(appKey)
        {
            _skip = skip;
        }

        public QixinResult GetAbnormalList(string name)
        {
            var request = HttpWebRequestHelper.Create($"{BaseApiUrl}{string.Format(ApiResource.GetAbnormalListByName, AppKey, name, _skip)}", "Get");
            return SendRequest(request);
        }
    }
}