using Generic.Core.Presentation.StartPage;
using Generic.Core.Services;
using Generic.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Generic.Test.Unit.Presentation.StartPage
{
    public class StartPagePresenterSpecification : AutoMockedTestBase<StartPagePresenter>
    {
        protected override void Given()
        {
            base.Given();
            Using<IImportantService>()
                .Setup(s => s.GetImportantHeading("world"))
                .Returns("FakeHeading");

            Using<IImportantService>()
                .Setup(s => s.GetImportantText("WORLD"))
                .Returns("FakeText");
        }

        [Test]
        public void should_return_service_heading_result_when_using_service()
        {
            Assert.AreEqual("FakeHeading", ClassUnderTest.GetHeading(true));
        }

        [Test]
        public void should_return_service_text_result_when_using_service()
        {
            Assert.AreEqual("FakeText", ClassUnderTest.GetText(true));
        }

        [Test]
        public void should_use_default_heading_when_not_using_service()
        {
            Assert.AreEqual("No heading", ClassUnderTest.GetHeading(false));
        }

        [Test]
        public void should_use_default_text_when_not_using_service()
        {
            Assert.AreEqual("No text", ClassUnderTest.GetText(false));
        }
    }
}
