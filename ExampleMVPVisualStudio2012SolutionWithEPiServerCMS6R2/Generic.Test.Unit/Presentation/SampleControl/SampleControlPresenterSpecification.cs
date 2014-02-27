using Generic.Core.Presentation.SampleControl;
using Generic.Core.Services;
using Generic.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Generic.Test.Unit.Presentation.SampleControl
{
    public class SampleControlPresenterSpecification : AutoMockedTestBase<SampleControlPresenter>
    {
        protected override void Given()
        {
            base.Given();
            Using<IImportantService>()
                .Setup(s => s.GetImportantHeading("ControlText"))
                .Returns("FakeControlText");
        }

        [Test]
        public void should_return_service_control_text_result_when_using_service()
        {
            Assert.AreEqual("FakeControlText", ClassUnderTest.GetControlText(true));
        }

        [Test]
        public void should_use_default_control_text_when_not_using_service()
        {
            Assert.AreEqual("Default text", ClassUnderTest.GetControlText(false));
        }
    }
}
