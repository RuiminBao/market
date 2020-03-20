using Newtonsoft.Json;
using System;

namespace Qixin.ApiService.Result
{
    [Serializable]
    public class QixinResult
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }

        [JsonProperty("items")]
        public object Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("num")]
        public int Num { get; set; }
    }
}