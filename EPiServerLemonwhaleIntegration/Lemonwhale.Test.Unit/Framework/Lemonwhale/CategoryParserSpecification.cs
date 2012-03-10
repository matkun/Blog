using System;
using System.Collections.Generic;
using System.Linq;
using Lemonwhale.Core.Framework.Lemonwhale;
using Lemonwhale.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.Framework.Lemonwhale
{
    public class CategoryParserSpecification : AutoMockedTestBase<CategoryParser>
    {
        protected IEnumerable<Guid> ResultGuids;
        protected string InputString;

        protected override void When()
        {
            base.When();
            ResultGuids = ClassUnderTest.ParseCategoryIds(InputString);
        }

        public class when_input_string_is_not_set : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = null;
            }

            [Test]
            public void should_return_empty_list()
            {
                Assert.IsEmpty(ResultGuids);
            }
        }
        public class when_input_string_is_empty : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = string.Empty;
            }

            [Test]
            public void should_return_empty_list()
            {
                Assert.IsEmpty(ResultGuids);
            }
        }
        public class when_input_string_has_one_guid : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";
            }

            [Test]
            public void should_return_correct_number_of_guids()
            {
                Assert.AreEqual(1, ResultGuids.Count());
            }

            [Test]
            public void should_return_correct_guid()
            {
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultGuids.First().ToString());
            }
        }
        public class when_input_string_has_multiple_guids : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa,bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb,cccccccc-cccc-cccc-cccc-cccccccccccc";
            }

            [Test]
            public void should_return_correct_number_of_guids()
            {
                Assert.AreEqual(3, ResultGuids.Count());
            }

            [Test]
            public void should_return_correct_guids()
            {
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultGuids.First().ToString());
                Assert.AreEqual("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", ResultGuids.Skip(1).First().ToString());
                Assert.AreEqual("cccccccc-cccc-cccc-cccc-cccccccccccc", ResultGuids.Last().ToString());
            }
        }
        public class when_input_string_has_multiple_guids_that_are_not_distinct : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa,bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb,aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa";
            }

            [Test]
            public void should_return_distinct_number_of_guids()
            {
                Assert.AreEqual(2, ResultGuids.Count());
            }

            [Test]
            public void should_return_correct_guids()
            {
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultGuids.First().ToString());
                Assert.AreEqual("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", ResultGuids.Last().ToString());
            }
        }
        public class when_input_string_has_non_guid_values : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa,not a guid,not this either";
            }

            [Test]
            public void should_return_only_proper_guids()
            {
                Assert.AreEqual(1, ResultGuids.Count());
            }

            [Test]
            public void should_return_correct_guid()
            {
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultGuids.First().ToString());
            }
        }
        public class when_input_string_has_guids_not_separated_correctly : CategoryParserSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputString = "  aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa, bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb  , cccccccc-cccc-cccc-cccc-cccccccccccc      ";
            }

            [Test]
            public void should_return_all_guids()
            {
                Assert.AreEqual(3, ResultGuids.Count());
            }

            [Test]
            public void should_return_correct_guids()
            {
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultGuids.First().ToString());
                Assert.AreEqual("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", ResultGuids.Skip(1).First().ToString());
                Assert.AreEqual("cccccccc-cccc-cccc-cccc-cccccccccccc", ResultGuids.Last().ToString());
            }
        }
    }
}
