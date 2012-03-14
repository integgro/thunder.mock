using System.Security.Principal;
using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Mock context
    /// </summary>
    public static class MockContext
    {
        /// <summary>
        /// Mock context
        /// </summary>
        /// <param name="request">Request</param>
        public static Mock<HttpContextBase> Make(Mock<HttpRequestBase> request)
        {
            return Make(request, new Mock<HttpResponseBase>(), new Mock<HttpSessionStateBase>(),
                        new Mock<HttpServerUtilityBase>(), new Mock<IPrincipal>());
        }

        /// <summary>
        /// Make mock context
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="session">Session</param>
        /// <returns>Mock context</returns>
        public static Mock<HttpContextBase> Make(Mock<HttpRequestBase> request, Mock<HttpSessionStateBase> session)
        {
            return Make(request, new Mock<HttpResponseBase>(), session, new Mock<HttpServerUtilityBase>(), new Mock<IPrincipal>());
        }

        /// <summary>
        /// Make mock context
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="response">Response</param>
        /// <param name="session">Sesion</param>
        /// <param name="server">Session</param>
        /// <param name="user">User</param>
        /// <returns>Mock context</returns>
        public static Mock<HttpContextBase> Make(Mock<HttpRequestBase> request, Mock<HttpResponseBase> response,
                                                 Mock<HttpSessionStateBase> session, Mock<HttpServerUtilityBase> server,
                                                 Mock<IPrincipal> user)
        {
            var context = new Mock<HttpContextBase>();
            var identity = new Mock<IIdentity>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);

            user.Setup(ctx => ctx.Identity).Returns(identity.Object);

            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("user_test");

            return context;
        }


    }
}