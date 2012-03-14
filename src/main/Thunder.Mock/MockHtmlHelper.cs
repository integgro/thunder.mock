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
        /// Make mock html helper
        /// </summary>
        /// <param name="viewDataDictionary"></param>
        /// <returns></returns>
        public static HtmlHelper Make(ViewDataDictionary viewDataDictionary)
        {
            var context = new Mock<HttpContextBase>();
            var controllerContext = new ControllerContext(context.Object,
                new RouteData(),new Mock<ControllerBase>().Object);

            var viewContext = new Mock<ViewContext>(
                controllerContext,
                new Mock<IView>().Object,
                viewDataDictionary,
                new TempDataDictionary(),
                new StringWriter());

            viewContext.Setup(m => m.HttpContext).Returns(context.Object);

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewDataDictionary);

            return new HtmlHelper(viewContext.Object, mockViewDataContainer.Object);
        }
    }
}
