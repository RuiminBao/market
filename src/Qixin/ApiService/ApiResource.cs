namespace Qixin.ApiService
{
    public sealed class ApiResource
    {
        public const string GetAbnormalListByName = "/v2/abnormal/getAbnormalListByName?appkey={0}&name={1}&skip={2}";
        public const string GetExecutionListByName = "/execution/getExecutionListByName?appkey={0}&keyword={1}&skip={2}";
        public const string GetDetailByName = "/v2/enterprise/getDetailByName?appkey={0}&keyword={1}";
    }
}