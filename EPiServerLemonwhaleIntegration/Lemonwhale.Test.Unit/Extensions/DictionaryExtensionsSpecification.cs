using System.Collections.Generic;
using Lemonwhale.Core.Extensions;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.Extensions
{
    [TestFixture]
    public class DictionaryExtensionsSpecification
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

        protected Dictionary<string, string> DictionaryInput;

        public class to_query_string_extension : DictionaryExtensionsSpecification
        {
            protected string QueryStringResult;

            protected override void When()
            {
                base.When();
                QueryStringResult = DictionaryInput.ToQueryString();
            }

            public class when_dictionary_is_null : to_query_string_extension
            {
                protected override void Given()
                {
                    base.Given();
                    DictionaryInput = null;
                }

                [Test]
                public void should_return_empty_query_string()
                {
                    Assert.IsEmpty(QueryStringResult);
                }
            }
            public class when_dictionary_is_empty : to_query_string_extension
            {
                protected override void Given()
                {
                    base.Given();
                    DictionaryInput = new Dictionary<string, string>();
                }

                [Test]
                public void should_return_empty_query_string()
                {
                    Assert.IsEmpty(QueryStringResult);
                }
            }
            public class when_dictionary_contains_one_parameter : to_query_string_extension
            {
                protected override void Given()
                {
                    base.Given();
                    DictionaryInput = new Dictionary<string, string>
                                          {
                                              {"param1", "value1"}
                                          };
                }

                [Test]
                public void should_return_query_string_with_one_parameter()
                {
                    Assert.AreEqual("param1=value1", QueryStringResult);
                }
            }
            public class when_dictionary_contains_multiple_parameters : to_query_string_extension
            {
                protected override void Given()
                {
                    base.Given();
                    DictionaryInput = new Dictionary<string, string>
                                          {
                                              {"param1", "value1"},
                                              {"param2", "value2"},
                                              {"param3", "value3"}
                                          };
                }

                [Test]
                public void should_return_query_string_with_one_parameter()
                {
                    const string expected = "param1=value1&param2=value2&param3=value3";
                    Assert.AreEqual(expected, QueryStringResult);
                }
            }
        }

        public class is_null_or_empty_extension : DictionaryExtensionsSpecification
        {
            protected bool IsNullOrEmptyResult;
            
            protected override void When()
            {
                base.When();
                IsNullOrEmptyResult = DictionaryInput.IsNullOrEmpty();
            }

            public class when_dictionary_is_null : is_null_or_empty_extension
            {
                protected override void Given()
                {
                    base.Given();
                    DictionaryInput = null;
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
                    DictionaryInput = new Dictionary<string, string>();
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
                    DictionaryInput = new Dictionary<string, string>
                                          {
                                              {"key", "value"}
                                          };
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
