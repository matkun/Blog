using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemonwhale.Core.DataAccess.Lemonwhale;
using Lemonwhale.Core.Extensions;
using Lemonwhale.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.DataAccess.Lemonwhale
{
    public class QueryCollectionGeneratorSpecification : AutoMockedTestBase<QueryCollectionGenerator>
    {
        protected Dictionary<string, string> QueryCollectionResult;

        protected SortBy? SortByInput;
        protected SortOrder? SortOrderInput;
        protected int? PageInput;
        protected int? PageSizeInput;
        protected DateTime? StartDateInput;
        protected DateTime? StopDateInput;
        protected IEnumerable<Guid> CategoriesInput;
        protected string SearchQueryInput;

        protected string ValueFor(string key)
        {
            return QueryCollectionResult.First(p => p.Key == key).Value;
        }

        protected override void When()
        {
            base.When();
            QueryCollectionResult = ClassUnderTest.GenerateQueryCollection(sortBy: SortByInput,
                                                                           sortOrder: SortOrderInput,
                                                                           page: PageInput,
                                                                           pageSize: PageSizeInput,
                                                                           startDate: StartDateInput,
                                                                           stopDate: StopDateInput,
                                                                           categories: CategoriesInput,
                                                                           searchQuery: SearchQueryInput);
        }

        public class when_there_are_no_input_values : QueryCollectionGeneratorSpecification
        {
            protected override void Given()
            {
                base.Given();

                SortByInput = null;
                SortOrderInput = null;
                PageInput = null;
                PageSizeInput = null;
                StartDateInput = null;
                StopDateInput = null;
                CategoriesInput = null;
                SearchQueryInput = null;
            }

            [Test]
            public void should_return_empty_query_collection()
            {
                Assert.IsEmpty(QueryCollectionResult);
            }
        }
        public class when_all_input_values_are_set : QueryCollectionGeneratorSpecification
        {
            protected override void Given()
            {
                base.Given();

                SortByInput = SortBy.Published;
                SortOrderInput = SortOrder.Descending;
                PageInput = 2;
                PageSizeInput = 14;
                StartDateInput = new DateTime(2010, 12, 31, 11, 22, 33);
                StopDateInput = new DateTime(2020, 1, 1, 1, 1, 1);
                CategoriesInput = new []
                                    {
                                        new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                                        new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                                        new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc")
                                    };
                SearchQueryInput = "encoded search string";
            }

            [Test]
            public void should_contain_all_parameters()
            {
                Assert.AreEqual(7, QueryCollectionResult.Count);
            }

            [Test]
            public void should_sort_by_published_date()
            {
                Assert.AreEqual(SortBy.Published.StringValue(), ValueFor(QueryParameterKeys.SortBy));
            }

            [Test]
            public void should_sort_in_decending_order()
            {
                Assert.AreEqual(SortOrder.Descending.StringValue(), ValueFor(QueryParameterKeys.SortOrder));
            }

            [Test]
            public void should_retrieve_second_page()
            {
                Assert.AreEqual("2", ValueFor(QueryParameterKeys.Page));
            }

            [Test]
            public void should_have_page_size_fourteen()
            {
                Assert.AreEqual("14", ValueFor(QueryParameterKeys.PageSize));
            }

            [Test]
            public void should_have_corret_created_between_time_span()
            {
                Assert.AreEqual("2010-12-31,2020-01-01", ValueFor(QueryParameterKeys.CreatedBetween));
            }

            [Test]
            public void should_use_all_categories()
            {
                const string expected = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa,bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb,cccccccc-cccc-cccc-cccc-cccccccccccc";
                Assert.AreEqual(expected, ValueFor(QueryParameterKeys.Categories));
            }

            [Test]
            public void should_search_for_query()
            {
                Assert.AreEqual("encoded+search+string", ValueFor(QueryParameterKeys.SearchQuery));
            }
        }
        public class when_start_date_is_not_set : QueryCollectionGeneratorSpecification
        {
            protected override void Given()
            {
                base.Given();
                StopDateInput = new DateTime(2020, 1, 1, 1, 1, 1);
            }

            [Test]
            public void should_not_have_time_span_parameter()
            {
                Assert.IsEmpty(QueryCollectionResult);
            }
        }
        public class when_stop_date_is_not_set : QueryCollectionGeneratorSpecification
        {
            protected override void Given()
            {
                base.Given();
                StartDateInput = new DateTime(2010, 12, 31, 11, 22, 33);
            }

            [Test]
            public void should_not_have_time_span_parameter()
            {
                Assert.IsEmpty(QueryCollectionResult);
            }
        }
    }
}
