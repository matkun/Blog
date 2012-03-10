using Lemonwhale.Core.Extensions;
using Lemonwhale.Core.Framework.CustomAttributes;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.Extensions
{
    [TestFixture]
    public class EnumExtensionsSpecification
    {
        // Cannot pass static extension classes as generics.
        // Need to set up tests manually.

        [SetUp]
        public void Init()
        {
            Given();
            When();
        }
        protected virtual void Given() {}
        protected virtual void When() {}
        
        public class string_value_extension : EnumExtensionsSpecification
        {
            protected string StringValueResult;
            protected TestEnum InputEnum;

            protected enum TestEnum
            {
                [StringValue("Value string")]
                HasStringValueAttribute,
                NoStringValueAttribute,
            }

            protected override void When()
            {
                base.When();
                StringValueResult = InputEnum.StringValue();
            }

            public class when_enum_has_attribute : string_value_extension
            {
                protected override void Given()
                {
                    base.Given();
                    InputEnum = TestEnum.HasStringValueAttribute;
                }

                [Test]
                public void should_return_enum_string_value()
                {
                    Assert.AreEqual("Value string", StringValueResult);
                }
            }
            public class when_enum_has_no_attribute : string_value_extension
            {
                protected override void Given()
                {
                    base.Given();
                    InputEnum = TestEnum.NoStringValueAttribute;
                }

                [Test]
                public void should_return_enum_string_value()
                {
                    Assert.IsNullOrEmpty(StringValueResult);
                }
            }
        }
    }
}
