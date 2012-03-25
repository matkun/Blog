using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.Core;
using NUnit.Framework;
using PageTypeTreeFilter.Framework;
using PageTypeTreeFilter.Test.Unit.TestHelpers;

namespace PageTypeTreeFilter.Test.Unit.Framework
{
    [TestFixture]
    public class FilterStrategySpecification
    {
        // Cannot pass static extension classes as generics.
        // Need to set up tests manually.

        [SetUp]
        public void Init()
        {
            Given();
            When();
        }
        protected virtual void Given() { }
        protected virtual void When() { }
        
        protected string InputPageTypeId;
        protected bool Result;

        public class when_filtering_on_page_type : FilterStrategySpecification
        {
            protected override void When()
            {
                base.When();
                Result = FilterStrategy.ShouldFilterOnPageType(InputPageTypeId);
            }

            public class when_selected_page_type_id_is_not_set : when_filtering_on_page_type
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeId = null;
                }

                [Test]
                public void should_not_filter_on_page_type()
                {
                    Assert.IsFalse(Result);
                }
            }
            public class when_selected_page_type_id_is_empty : when_filtering_on_page_type
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeId = string.Empty;
                }

                [Test]
                public void should_not_filter_on_page_type()
                {
                    Assert.IsFalse(Result);
                }
            }
            public class when_showing_all_page_types : when_filtering_on_page_type
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeId = PageFilterConstants.ShowAllPageTypes;
                }

                [Test]
                public void should_not_filter_on_page_type()
                {
                    Assert.IsFalse(Result);
                }
            }
            public class when_selected_page_type_id_is_set : when_filtering_on_page_type
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeId = "1";
                }

                [Test]
                public void should_filter_on_page_type()
                {
                    Assert.IsTrue(Result);
                }
            }
        }

        //public class when_checking_if_page_matches_a_filter : FilterStrategySpecification
        //{
        //    protected PageData InputPageData;

        //    protected override void Given()
        //    {
        //        base.Given();
        //        InputPageTypeId = "1";
        //    }

        //    protected override void When()
        //    {
        //        base.When();
        //        Result = FilterStrategy.MatchesFilter(InputPageData, InputPageTypeId);
        //    }

        //    public class when_page_is_of_matching_page_type : when_checking_if_page_matches_a_filter
        //    {
        //        protected override void Given()
        //        {
        //            base.Given();
        //            InputPageData = new PageData {PageTypeID = 1};
        //        }

        //        [Test]
        //        public void should_match_filter()
        //        {
        //            Assert.IsTrue(Result);
        //        }
        //    }
        //}
    }
}
