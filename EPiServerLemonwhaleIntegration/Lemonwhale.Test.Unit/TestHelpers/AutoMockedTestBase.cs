using AutoMoq;
using Moq;

namespace Lemonwhale.Test.Unit.TestHelpers
{
    public abstract class AutoMockedTestBase<TClassUnderTest> : TestBase<TClassUnderTest>
        where TClassUnderTest : class
    {
        protected AutoMoqer Container;

        protected override TClassUnderTest ResolveClassUnderTest()
        {
            return Container.Resolve<TClassUnderTest>();
        }

        public Mock<T> Using<T>() where T : class
        {
            return Container.GetMock<T>();
        }

        protected override void Given()
        {
            Container = new AutoMoqer();
            base.Given();
        }
    }
}
