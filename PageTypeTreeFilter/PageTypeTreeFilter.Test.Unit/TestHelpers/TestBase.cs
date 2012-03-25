using NUnit.Framework;

namespace PageTypeTreeFilter.Test.Unit.TestHelpers
{
    [TestFixture]
    public abstract class TestBase<TClassUnderTest> where TClassUnderTest : class
    {
        protected abstract TClassUnderTest ResolveClassUnderTest();

        private TClassUnderTest _classUnderTest;
        protected TClassUnderTest ClassUnderTest
        {
            get { return _classUnderTest ?? (_classUnderTest = ResolveClassUnderTest()); }
        }

        protected void EnsureClassUnderTest()
        {
            var classUnderTest = ClassUnderTest;
        }

        [SetUp]
        public void Init()
        {
            _classUnderTest = null;

            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }

        [TearDown]
        public void Terminate()
        {
            AfterEachTest();
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}
