using Microsoft.VisualStudio.TestTools.UnitTesting;
using dots.services;

namespace TestProject
{
    [TestClass]
    class CommentsServiceTest
    {

        private Service getService()
        {
            return new EntityCommentsService();
        }

        [TestMethod]
        public void Refresh()
        {
            Service service = getService();
            service.Refresh();


        }
    }
}
