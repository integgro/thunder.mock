using System;
using System.Collections.Generic;
using System.Web;
using Moq;

namespace Thunder.Mock
{
    /// <summary>
    /// Fake session
    /// </summary>
    public static class MockSesssion
    {
        ///<summary>
        /// Set fake session
        ///</summary>
        ///<param name="httpSessionStateBase"></param>
        ///<param name="sessions"></param>
        public static void SetFake(this Mock<HttpSessionStateBase> httpSessionStateBase, 
            Dictionary<string, Object> sessions)
        {
            foreach (var item in sessions)
            {
                var item1 = item;
                httpSessionStateBase.SetupGet(s => s[item1.Key]).Returns(item.Value);
            }
        }
    }
}
