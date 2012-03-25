using System;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;
using NUnit.Framework;
using PageTypeTreeFilter.Extensions;

namespace PageTypeTreeFilter.Test.Unit.Extensions
{
    [TestFixture]
    public class PageTypeExtensionsSpecification
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

        protected PageType InputPageType;
        protected ListItem ResultListItem;

        public class when_page_type_collection_is_not_set : PageTypeExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputPageType = null;
            }

            protected override void When()
            {
                base.When();
                ResultListItem = InputPageType.ToListItem();
            }

            [Test]
            public void should_return_null()
            {
                Assert.IsNull(ResultListItem);
            }
        }
        public class when_page_type_collection_has_page_types : PageTypeExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputPageType = new PageType(1, Guid.NewGuid(), "pt1", "desc1", "~/1.aspx", true, 1);
            }

            protected override void When()
            {
                base.When();
                ResultListItem = InputPageType.ToListItem();
            }

            [Test]
            public void should_map_name_correctly()
            {
                Assert.AreEqual("pt1", ResultListItem.Text);
            }

            [Test]
            public void should_map_value_correctly()
            {
                Assert.AreEqual("1", ResultListItem.Value);
            }
        }
    }
}
