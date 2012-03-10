using Lemonwhale.Core.Extensions;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Lemonwhale.Test.Unit.Extensions
{
    [TestFixture]
    public class JArrayExtensionSpecification
    {
        // Cannot pass static extension classes as generics.
        // Need to set up tests manually.

        [SetUp]
        public void Init()
        {
            Given();
            When();
        }
        protected virtual void Given(){}
        protected virtual void When(){}

        protected JArray JArrayInput;

        public class is_null_or_empty_extension : JArrayExtensionSpecification
        {
            protected bool IsNullOrEmptyResult;

            protected override void When()
            {
                base.When();
                IsNullOrEmptyResult = JArrayInput.IsNullOrEmpty();
            }

            public class when_dictionary_is_null : is_null_or_empty_extension
            {
                protected override void Given()
                {
                    base.Given();
                    JArrayInput = null;
                }

                [Test]
                public void should_be_null_or_empty()
                {
                    Assert.IsTrue(IsNullOrEmptyResult);
                }
            }
            public class when_dictionary_is_empty : is_null_or_empty_extension
            {
                protected override void Given()
                {
                    base.Given();
                    JArrayInput = new JArray();
                }

                [Test]
                public void should_be_null_or_empty()
                {
                    Assert.IsTrue(IsNullOrEmptyResult);
                }
            }
            public class when_dictionary_is_not_empty : is_null_or_empty_extension
            {
                protected override void Given()
                {
                    base.Given();
                    JArrayInput = new JArray {new JObject()};
                }

                [Test]
                public void should_not_be_null_or_empty()
                {
                    Assert.IsFalse(IsNullOrEmptyResult);
                }
            }
        }
    }
}
