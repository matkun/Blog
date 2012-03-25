using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;
using NUnit.Framework;
using PageTypeTreeFilter.AdapterPattern;
using PageTypeTreeFilter.Presentation.Settings;
using PageTypeTreeFilter.Test.Unit.Framework.Authorization;
using PageTypeTreeFilter.Test.Unit.TestHelpers;

namespace PageTypeTreeFilter.Test.Unit.Presentation.Settings
{
    public class PageTypePickerPresenterSpecification : AutoMockedTestBase<PageTypePickerPresenter>
    {
        protected IPageTypePickerView View;

        protected override void Given()
        {
            base.Given();

            var pt1 = new PageType(1, Guid.NewGuid(), "pt1", "desc1", "~/1.aspx", true, 1);
            var pt2 = new PageType(2, Guid.NewGuid(), "pt2", "desc2", "~/2.aspx", true, 2);
            var pt3 = new PageType(3, Guid.NewGuid(), "pt3", "desc3", "~/3.aspx", true, 3);
            var pt4 = new PageType(4, Guid.NewGuid(), "pt4", "desc4", "~/4.aspx", true, 4);

            var allPageTypes = new PageTypeCollection { pt1, pt2, pt3, pt4 };

            Using<IPageTypeWrapper>()
                .Setup(pt => pt.List())
                .Returns(allPageTypes);

            Using<IPageTypeWrapper>()
                .Setup(pt => pt.Load(2))
                .Returns(pt2);
            Using<IPageTypeWrapper>()
                .Setup(pt => pt.Load(3))
                .Returns(pt3);
            Using<IPageTypeWrapper>()
                .Setup(pt => pt.Load(4))
                .Returns(pt4);
        }
        public class when_retrieving_selected_page_types : PageTypePickerPresenterSpecification
        {
            protected IEnumerable<ListItem> SelectedPageTypeItems;

            protected override void Given()
            {
                base.Given();

                Using<IPageTypePickerView>()
                    .Setup(v => v.SelectedPageTypeIds)
                    .Returns("2;3;4;not valid will be ignored;");

                Using<HttpContextBase>()
                    .Setup(c => c.User)
                    .Returns(new FakeUser());
            }

            protected override void When()
            {
                base.When();
                SelectedPageTypeItems = ClassUnderTest.GetSelectedPageTypes();
            }

            [Test]
            public void should_retrieve_all_selected_types()
            {
                Assert.AreEqual(3, SelectedPageTypeItems.Count());
            }

            [Test]
            public void should_retrieve_correct_types()
            {
                Assert.IsTrue(SelectedPageTypeItems.Any(i => i.Value == "2"));
                Assert.IsTrue(SelectedPageTypeItems.Any(i => i.Value == "3"));
                Assert.IsTrue(SelectedPageTypeItems.Any(i => i.Value == "4"));
            }

            [Test]
            public void should_not_retrieve_type_that_was_not_selected()
            {
                Assert.IsFalse(SelectedPageTypeItems.Any(i => i.Value == "1"));
            }
        }
        public class when_looking_for_selected_page_types : PageTypePickerPresenterSpecification
        {
            protected bool IsSelected;

            protected override void Given()
            {
                base.Given();

                var selected = new List<ListItem>
                                   {
                                       new ListItem("text 1", "value 1"),
                                       new ListItem("text 2", "value 2")
                                   };

                Using<IPageTypePickerView>()
                    .Setup(v => v.SelectedPageTypes)
                    .Returns(selected);
            }

            [Test]
            public void should_find_selected_page_type()
            {
                var isASelectedPageType = ClassUnderTest.IsASelectedPageType(new ListItem("text 2", "value 2"));
                Assert.IsTrue(isASelectedPageType);
            }

            [Test]
            public void should_not_find_page_types_that_are_not_selected()
            {
                var isASelectedPageType = ClassUnderTest.IsASelectedPageType(new ListItem("not selected", "not selected"));
                Assert.IsFalse(isASelectedPageType);
            }
        }
    }
}
