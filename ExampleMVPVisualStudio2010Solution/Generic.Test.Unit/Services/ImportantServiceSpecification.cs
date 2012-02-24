using Generic.Core.Services;
using Generic.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Generic.Test.Unit.Services
{
    public class ImportantServiceSpecification : AutoMockedTestBase<ImportantService>
    {
        protected string ResultHeading;
        protected string ResultText;

        protected string InputHeading;
        protected string InputText;

        protected override void When()
        {
            base.When();
            ResultHeading = ClassUnderTest.GetImportantHeading(InputHeading);
            ResultText = ClassUnderTest.GetImportantText(InputText);
        }

        public class when_important_texts_are_empty : ImportantServiceSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputHeading = string.Empty;
                InputText = string.Empty;
            }

            [Test]
            public void should_give_sad_heading_result()
            {
                Assert.AreEqual("Sad text..", ResultHeading);
            }

            [Test]
            public void should_give_sad_text_result()
            {
                Assert.AreEqual("SAD TEXT..", ResultText);
            }
        }

        public class when_there_are_important_texts : ImportantServiceSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputHeading = "important";
                InputText = "IMPORTANT";
            }

            [Test]
            public void should_give_important_heading_result()
            {
                Assert.AreEqual("Hello important", ResultHeading);
            }

            [Test]
            public void should_give_important_text_result()
            {
                Assert.AreEqual("HELLO IMPORTANT", ResultText);
            }
        }
    }
}
