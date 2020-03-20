using Newtonsoft.Json;
using Qixin.ApiService.Result;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Qixin.ApiService
{
    public class ApiServiceBase
    {
        protected const string BaseApiUrl = "http://api.qixin.com/APIService";
        protected readonly string AppKey;

        protected ApiServiceBase(string appKey)
        {
            AppKey = appKey;
        }

        protected QixinResult SendRequest(HttpWebRequest request, string charset = "utf-8")
        {
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(GetResponseErrorMsg(response));
            var responseStream = response.GetResponseStream();
            if (string.Compare(response.ContentEncoding, "gzip", StringComparison.OrdinalIgnoreCase) >= 0)
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            using (var streamReader = new StreamReader(responseStream, Encoding.GetEncoding(charset)))
            {
                var responseText = streamReader.ReadToEnd();
                dynamic result = JObject.Parse(responseText);
                int status = result.status;

                string message = result.message;
                string sign = result.sign;
                int total = 0;
                int num = 0;
                object items = null;
                if (status == 200)
                {
                    dynamic data = result.data;
                    total = data?.total ?? 0;
                    num = data?.num ?? 0;
                    items = data?.items ?? result.data;
                }
                QixinResult apiResult = new QixinResult()
                {
                    Items = items,
                    Message = message,
                    Num = num,
                    Sign = sign,
                    Status = status,
                    Total = total

                };
                return apiResult;
            }
        }

        protected virtual string GetResponseErrorMsg(WebResponse response)
        {
            if (response == null) return "网络链接异常";
            var builder = new StringBuilder();
            using (response)
            {
                var httpResponse = (HttpWebResponse)response;
                builder.AppendLine($"http error code: {httpResponse.StatusCode}");
                using (var data = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(data))
                    {
                        builder.AppendLine($"error in body: {reader.ReadToEnd()}");
                    }
                }
            }

            return builder.ToString();
        }
    }
}