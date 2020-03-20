using System;
using System.Net;
using System.Text;

namespace Qixin.Helper
{
    public sealed class HttpWebRequestHelper
    {
        public static HttpWebRequest Create(string url, string type = "GET", string data = null, string charset = "utf-8")
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = type;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.MaximumAutomaticRedirections = 4;
            httpWebRequest.Timeout = 98301;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            if (!type.Equals("get", StringComparison.OrdinalIgnoreCase) && data != null)
            {
                var bytes = Encoding.GetEncoding(charset).GetBytes(data);
                httpWebRequest.ContentLength = Encoding.GetEncoding(charset).GetBytes(data).Length;
                var requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }

            return httpWebRequest;
        }
    }
}