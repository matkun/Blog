using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;
using NUnit.Framework;
using PageTypeTreeFilter.Extensions;

namespace PageTypeTreeFilter.Test.Unit.Extensions
{
    [TestFixture]
    public class PageTypeCollectionExtensionsSpecification
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

        protected PageTypeCollection InputCollection;
        protected IEnumerable<ListItem> ResultCollection;

        public class when_page_type_collection_is_not_set : PageTypeCollectionExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputCollection = null;
            }

            protected override void When()
            {
                base.When();
                ResultCollection = InputCollection.ToListItems();
            }

            [Test]
            public void should_return_empty_list_item_collection()
            {
                Assert.IsEmpty(ResultCollection);
            }
        }
        public class when_page_type_collection_is_empty : PageTypeCollectionExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputCollection = new PageTypeCollection();
            }

            protected override void When()
            {
                base.When();
                ResultCollection = InputCollection.ToListItems();
            }

            [Test]
            public void should_return_empty_list_item_collection()
            {
                Assert.IsEmpty(ResultCollection);
            }
        }
        public class when_page_type_collection_has_page_types : PageTypeCollectionExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputCollection = new PageTypeCollection
                                      {
                                          new PageType(1, Guid.NewGuid(), "pt1", "desc1", "~/1.aspx", true, 1),
                                          new PageType(2, Guid.NewGuid(), "pt2", "desc2", "~/2.aspx", true, 2),
                                          new PageType(3, Guid.NewGuid(), "pt3", "desc3", "~/3.aspx", true, 3),
                                      };
            }

            protected override void When()
            {
                base.When();
                ResultCollection = InputCollection.ToListItems();
            }

            [Test]
            public void should_return_correct_number_of_list_items()
            {
                Assert.AreEqual(3, ResultCollection.Count());
            }

            [Test]
            public void should_return_correct_names_and_values()
            {
                Assert.IsTrue(ResultCollection.Any(i => i.Value == "1" && i.Text == "pt1"));
                Assert.IsTrue(ResultCollection.Any(i => i.Value == "2" && i.Text == "pt2"));
                Assert.IsTrue(ResultCollection.Any(i => i.Value == "3" && i.Text == "pt3"));
            }

            [Test]
            public void should_not_map_a_page_type_that_is_not_there()
            {
                Assert.IsFalse(ResultCollection.Any(i => i.Value == "4" && i.Text == "pt4"));
            }
        }
    }
}
