using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Fake controller
    /// </summary>
    public static class MockController
    {
        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        public static void SetFake(this Controller controller, Uri uri, Dictionary<string, Object> sessions)
        {
            SetFake(controller, uri, sessions, null, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void SetFake(this Controller controller, Uri uri, Dictionary<string, Object> sessions, bool ajaxRequest)
        {
            SetFake(controller, uri, sessions, null, ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        public static void SetFake(this Controller controller, Uri uri)
        {
            SetFake(controller, uri, new Dictionary<string, object>(), false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void SetFake(this Controller controller, Uri uri, bool ajaxRequest)
        {
            SetFake(controller, uri, new Dictionary<string, object>(), ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="serverVariables">Server variables</param>
        public static void SetFake(this Controller controller, Uri uri, NameValueCollection serverVariables)
        {
            SetFake(controller, uri, new Dictionary<string, object>(), serverVariables, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void SetFake(this Controller controller, Uri uri, NameValueCollection serverVariables, bool ajaxRequest)
        {
            SetFake(controller, uri, new Dictionary<string, object>(), serverVariables, ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="serverVariables">Server variables</param>
        public static void SetFake(this Controller controller, Uri uri, Dictionary<string, Object> sessions, NameValueCollection serverVariables)
        {
            SetFake(controller, uri, sessions, serverVariables, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void SetFake(this Controller controller, Uri uri, Dictionary<string, Object> sessions, NameValueCollection serverVariables, bool ajaxRequest)
        {
            controller.ControllerContext = new ControllerContext(
                new RequestContext(
                    CreateContext(uri, sessions, serverVariables, ajaxRequest),
                    new RouteData()
                ), controller);
        }

        /// <summary>
        /// Create fake context
        /// </summary>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Session</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Ajax request</param>
        /// <returns>Fake context</returns>
        internal static HttpContextBase CreateContext(Uri uri, Dictionary<string, Object> sessions, NameValueCollection serverVariables, bool ajaxRequest)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var user = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session.Object);
            context.Setup(ctx => ctx.Server).Returns(server.Object);
            context.Setup(ctx => ctx.User).Returns(user.Object);

            user.Setup(ctx => ctx.Identity).Returns(identity.Object);

            identity.Setup(id => id.IsAuthenticated).Returns(true);
            identity.Setup(id => id.Name).Returns("test");

            session.SetFake(sessions);
            request.SetFake(uri, serverVariables, ajaxRequest);

            return context.Object;
        }
    }
}
