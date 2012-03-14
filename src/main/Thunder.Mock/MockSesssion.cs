using System.Collections.Generic;
using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Mock session
    /// </summary>
    public static class MockSesssion
    {
        ///<summary>
        /// Make mock session
        ///</summary>
        ///<param name="sessions">Sessions</param>
        public static Mock<HttpSessionStateBase> Make(Dictionary<string, object> sessions)
        {
            var session = new Mock<HttpSessionStateBase>();
            foreach (var item in sessions)
            {
                var item1 = item;
                session.SetupGet(s => s[item1.Key]).Returns(item.Value);
            }

            return session;
        }
    }
}