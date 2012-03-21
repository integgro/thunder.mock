using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Mock response
    /// </summary>
    public static class MockResponse
    {
        /// <summary>
        /// Mock response
        /// </summary>
        /// <returns>Mock</returns>
        public static Mock<HttpResponseBase> Make()
        {
            var response = new Mock<HttpResponseBase>();

            response.Setup(res => res.ApplyAppPathModifier(It.IsAny<string>())).
                Returns((string virtualPath) => virtualPath);

            return response;
        }
    }
}