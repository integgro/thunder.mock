using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;

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
        public static void Mock(this Controller controller, Uri uri, Dictionary<string, Object> sessions)
        {
            Mock(controller, uri, sessions, null, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void Mock(this Controller controller, Uri uri, Dictionary<string, Object> sessions, bool ajaxRequest)
        {
            Mock(controller, uri, sessions, null, ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        public static void Mock(this Controller controller, Uri uri)
        {
            Mock(controller, uri, new Dictionary<string, object>(), false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void Mock(this Controller controller, Uri uri, bool ajaxRequest)
        {
            Mock(controller, uri, new Dictionary<string, object>(), ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="serverVariables">Server variables</param>
        public static void Mock(this Controller controller, Uri uri, NameValueCollection serverVariables)
        {
            Mock(controller, uri, new Dictionary<string, object>(), serverVariables, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void Mock(this Controller controller, Uri uri, NameValueCollection serverVariables, bool ajaxRequest)
        {
            Mock(controller, uri, new Dictionary<string, object>(), serverVariables, ajaxRequest);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="serverVariables">Server variables</param>
        public static void Mock(this Controller controller, Uri uri, Dictionary<string, Object> sessions, NameValueCollection serverVariables)
        {
            Mock(controller, uri, sessions, serverVariables, false);
        }

        /// <summary>
        /// Set fake controller
        /// </summary>
        /// <param name="controller">Controller</param>
        /// <param name="uri">Uri</param>
        /// <param name="sessions">Sessions</param>
        /// <param name="serverVariables">Server variables</param>
        /// <param name="ajaxRequest">Ajax request</param>
        public static void Mock(this Controller controller, Uri uri, Dictionary<string, Object> sessions, NameValueCollection serverVariables, bool ajaxRequest)
        {
            var context = MockContext.Make(MockRequest.Make(uri, serverVariables, ajaxRequest), MockSesssion.Make(sessions));

            controller.ControllerContext = new ControllerContext(new RequestContext(context.Object,new RouteData()), controller);
        }
    }
}
