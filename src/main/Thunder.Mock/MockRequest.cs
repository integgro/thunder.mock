using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Fake request
    /// </summary>
    public static class MockRequest
    {
        ///<summary>
        /// Set fake query string
        ///</summary>
        ///<param name="request">Http Request</param>
        ///<param name="uri">Uri</param>
        ///<param name="serverVariables">Server variables</param>
        ///<param name="ajaxRequest">Ajax request</param>
        public static void SetFake(this Mock<HttpRequestBase> request, Uri uri, NameValueCollection serverVariables, bool ajaxRequest)
        {
            request.Setup(x => x.ApplicationPath).Returns("/");
            request.Setup(x => x.HttpMethod).Returns("GET");
            request.Setup(x => x.Url).Returns(uri);
            request.Setup(x => x.QueryString).Returns(GetQueryStringParameters(uri.ToString()));
            request.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(GetUrlFileName(uri.ToString()));
            request.Setup(x => x.ServerVariables).Returns(serverVariables);

            if (ajaxRequest)
            {
                request.Setup(x => x.Headers).Returns(new WebHeaderCollection
                                                      {
                                                          {"X-Requested-With", "XMLHttpRequest"}
                                                      });
            }
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
