using System.Web.UI.HtmlControls;
using NUnit.Framework;
using PageTypeTreeFilter.Framework.EmbeddedResources;
using PageTypeTreeFilter.Test.Unit.TestHelpers;

namespace PageTypeTreeFilter.Test.Unit.Framework.EmbeddedResources
{
    public class ResourceHandlerSpecification : AutoMockedTestBase<ResourceHandler>
    {
        protected HtmlLink ResultLink;
        protected string Path;

        protected override void Given()
        {
            base.Given();
            Path = "/embedded/resource.css";
        }

        protected override void When()
        {
            base.When();
            ResultLink = ClassUnderTest.CreateHtmlLink(Path);
        }

        [Test]
        public void should_have_correct_path()
        {
            Assert.AreEqual("/embedded/resource.css", ResultLink.Href);
        }

        [Test]
        public void should_have_correct_rel()
        {
            Assert.AreEqual("stylesheet", ResultLink.Attributes["rel"]);
        }

        [Test]
        public void should_have_correct_type()
        {
            Assert.AreEqual("text/css", ResultLink.Attributes["type"]);
        }

        [Test]
        public void should_have_correct_media()
        {
            Assert.AreEqual("screen", ResultLink.Attributes["media"]);
        }
    }
}
