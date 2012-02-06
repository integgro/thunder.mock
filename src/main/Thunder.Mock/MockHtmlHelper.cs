using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Thunder.Mock
{
    ///<summary>
    /// Mock html helper
    ///</summary>
    public static class MockHtmlHelper
    {
        /// <summary>
        /// Create mock html helper
        /// </summary>
        /// <param name="viewDataDictionary"></param>
        /// <returns></returns>
        public static HtmlHelper Create(ViewDataDictionary viewDataDictionary)
        {
            var viewContext = new Mock<ViewContext>(
                new ControllerContext(
                    new Mock<HttpContextBase>().Object,
                    new RouteData(),
                    new Mock<ControllerBase>().Object),
                new Mock<IView>().Object,
                viewDataDictionary,
                new TempDataDictionary(),
                new StringWriter());

            viewContext.Setup(m => m.HttpContext).Returns(new Mock<HttpContextBase>().Object);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewDataDictionary);

            return new HtmlHelper(viewContext.Object, mockViewDataContainer.Object);
        }
    }
}
