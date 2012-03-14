using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Mock request
    /// </summary>
    public static class MockRequest
    {
        /// <summary>
        /// Mock request
        /// </summary>
        /// <param name="url">Url</param>
        public static Mock<HttpRequestBase> Make(string url)
        {
            return Make(url, new NameValueCollection());
        }
        
        /// <summary>
        /// Mock request
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="serverVariables">Server variables</param>
        public static Mock<HttpRequestBase> Make(string url, NameValueCollection serverVariables)
        {
            return Make(url, serverVariables, false);
        }
        
        /// <summary>
        /// Mock request
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="ajaxRequest">Is ajax request</param>
        public static Mock<HttpRequestBase> Make(string url, bool ajaxRequest)
        {
            return Make(url, new NameValueCollection(), ajaxRequest);
        }
        
        /// <summary>
        /// Mock request
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Is ajax request</param>
        public static Mock<HttpRequestBase> Make(string url, NameValueCollection serverVariables,
                                bool ajaxRequest)
        {
            return Make(new Uri(url, UriKind.RelativeOrAbsolute), serverVariables, ajaxRequest);
        }
        
        /// <summary>
        /// Mock request
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Is ajax request</param>
        public static Mock<HttpRequestBase> Make(Uri uri, NameValueCollection serverVariables, bool ajaxRequest)
        {
            var request = new Mock<HttpRequestBase>();

            request.Setup(x => x.ApplicationPath).Returns("/");
            request.Setup(x => x.HttpMethod).Returns("GET");
            request.Setup(x => x.Url).Returns(uri);
            request.Setup(x => x.QueryString).Returns(GetQueryStringParameters(uri.ToString()));
            request.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(GetUrlFileName(uri.ToString()));
            request.Setup(x => x.ServerVariables).Returns(serverVariables);

            if (ajaxRequest)
            {
                request.Setup(x => x.Headers).Returns(
                    new WebHeaderCollection {{"X-Requested-With", "XMLHttpRequest"}});
            }

            return request;
        }

        private static string GetUrlFileName(string url)
        {
            return url.Contains("?") ? url.Substring(0, url.IndexOf("?")) : url;
        }

        private static NameValueCollection GetQueryStringParameters(string url)
        {
            if (!url.Contains("?"))
            {
                return null;
            }

            var parameters = new NameValueCollection();
            var parts = url.Split("?".ToCharArray());
            var keys = parts[1].Split("&".ToCharArray());

            foreach (var part in keys.Select(key => key.Split("=".ToCharArray())))
            {
                parameters.Add(part[0], part[1]);
            }

            return parameters;
        }
    }
}