using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using EPiServer.DataAbstraction;
using Moq;
using NUnit.Framework;
using PageTypeTreeFilter.AdapterPattern;
using PageTypeTreeFilter.Framework;
using PageTypeTreeFilter.Framework.DataAccess.GlobalSettings;
using PageTypeTreeFilter.Framework.DataAccess.UserSettings;
using PageTypeTreeFilter.Framework.Language;
using PageTypeTreeFilter.Test.Unit.TestHelpers;

namespace PageTypeTreeFilter.Test.Unit.Framework
{
    public class PageTypeStrategySpecification : AutoMockedTestBase<PageTypeStrategy>
    {
        protected IEnumerable<ListItem> ResultListItems;

        protected override void Given()
        {
            base.Given();
            var collection = new PageTypeCollection
                                      {
                                          new PageType(1, Guid.NewGuid(), "pt1", "desc1", "~/1.aspx", true, 1),
                                          new PageType(2, Guid.NewGuid(), "pt2", "desc2", "~/2.aspx", true, 2),
                                          new PageType(3, Guid.NewGuid(), "pt3", "desc3", "~/3.aspx", true, 3),
                                          new PageType(4, Guid.NewGuid(), "pt4", "desc4", "~/4.aspx", true, 4),
                                      };
            Using<IPageTypeWrapper>()
                .Setup(pt => pt.List())
                .Returns(collection);
        }

        public class when_creating_list_items_from_semi_colon_separated_string_of_ids : PageTypeStrategySpecification
        {
            protected string InputPageTypeIds;
            
            protected override void When()
            {
                base.When();
                ResultListItems = ClassUnderTest.CreateListItemsFor(InputPageTypeIds);
            }

            public class when_page_type_ids_string_is_not_set : when_creating_list_items_from_semi_colon_separated_string_of_ids
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeIds = null;
                }

                [Test]
                public void should_return_empty_list_of_list_items()
                {
                    Assert.IsEmpty(ResultListItems);
                }
            }
            public class when_page_type_ids_string_is_empty : when_creating_list_items_from_semi_colon_separated_string_of_ids
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeIds = string.Empty;
                }

                [Test]
                public void should_return_empty_list_of_list_items()
                {
                    Assert.IsEmpty(ResultListItems);
                }
            }
            public class when_there_is_one_id_in_the_string : when_creating_list_items_from_semi_colon_separated_string_of_ids
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeIds = "2";
                }

                [Test]
                public void should_return_collection_with_one_list_item()
                {
                    Assert.AreEqual(1, ResultListItems.Count());
                }

                [Test]
                public void should_map_name_and_value_correctly()
                {
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt2" && i.Value == "2"));
                }
            }
            public class when_there_are_multiple_ids_in_the_string : when_creating_list_items_from_semi_colon_separated_string_of_ids
            {
                protected override void Given()
                {
                    base.Given();
                    InputPageTypeIds = "2;3;4";
                }

                [Test]
                public void should_return_collection_with_all_list_items()
                {
                    Assert.AreEqual(3, ResultListItems.Count());
                }

                [Test]
                public void should_map_name_and_value_correctly()
                {
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt2" && i.Value == "2"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt3" && i.Value == "3"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt4" && i.Value == "4"));
                }

                [Test]
                public void should_not_map_list_item_that_was_not_included_in_string()
                {
                    Assert.IsFalse(ResultListItems.Any(i => i.Text == "pt1" && i.Value == "1"));
                }
            }
        }
        public class when_choosing_between_global_or_user_settings : PageTypeStrategySpecification
        {
            protected bool AllowUserIds;
            protected bool UseUserSpecificIds;

            protected override void Given()
            {
                base.Given();

                var globalSettings = new GlobalSettingsDto
                                         {
                                             AllowUserSettings = AllowUserIds,
                                             SelectedPageTypeIds = "1;2;3"
                                         };
                Using<IGlobalSettingsRepository>()
                    .Setup(r => r.LoadGlobalSettings())
                    .Returns(globalSettings);

                Using<IUserSettingsRepository>()
                    .Setup(r => r.UserSelectedPageTypeIds)
                    .Returns("3;4");
                Using<IUserSettingsRepository>()
                    .Setup(r => r.EnableUserSelectedPageTypes)
                    .Returns(UseUserSpecificIds);
            }

            protected override void When()
            {
                base.When();
                ResultListItems = ClassUnderTest.SelectablePageTypes();
            }

            public class when_user_specific_settings_are_not_allowed : when_choosing_between_global_or_user_settings
            {
                protected override void Given()
                {
                    AllowUserIds = false;
                    UseUserSpecificIds = true;
                    base.Given();
                }

                [Test]
                public void should_use_global_settings()
                {
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt1" && i.Value == "1"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt2" && i.Value == "2"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt3" && i.Value == "3"));
                }

                [Test]
                public void should_not_include_user_defined_page_types()
                {
                    Assert.IsFalse(ResultListItems.Any(i => i.Text == "pt4" && i.Value == "4"));
                }
            }
            public class when_user_specific_settings_allowed_but_not_used : when_choosing_between_global_or_user_settings
            {
                protected override void Given()
                {
                    AllowUserIds = true;
                    UseUserSpecificIds = false;
                    base.Given();
                }

                [Test]
                public void should_use_global_settings()
                {
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt1" && i.Value == "1"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt2" && i.Value == "2"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt3" && i.Value == "3"));
                }

                [Test]
                public void should_not_include_user_defined_page_types()
                {
                    Assert.IsFalse(ResultListItems.Any(i => i.Text == "pt4" && i.Value == "4"));
                }
            }
            public class when_user_specific_settings_allowed_and_used : when_choosing_between_global_or_user_settings
            {
                protected override void Given()
                {
                    AllowUserIds = true;
                    UseUserSpecificIds = true;
                    base.Given();
                }

                [Test]
                public void should_use_user_settings()
                {
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt3" && i.Value == "3"));
                    Assert.IsTrue(ResultListItems.Any(i => i.Text == "pt4" && i.Value == "4"));
                }

                [Test]
                public void should_not_include_exclusively_globally_defined_page_types()
                {
                    Assert.IsFalse(ResultListItems.Any(i => i.Text == "pt1" && i.Value == "1"));
                    Assert.IsFalse(ResultListItems.Any(i => i.Text == "pt2" && i.Value == "2"));
                }
            }
        }
        public class when_adding_all_page_types_alternative_to_the_collection : PageTypeStrategySpecification
        {
            protected override void Given()
            {
                base.Given();

                Using<ITranslator>()
                    .Setup(t => t.Translate(It.IsAny<string>()))
                    .Returns("--- Show all ---");

                var globalSettings = new GlobalSettingsDto
                {
                    AllowUserSettings = false,
                    SelectedPageTypeIds = "1"
                };
                Using<IGlobalSettingsRepository>()
                    .Setup(r => r.LoadGlobalSettings())
                    .Returns(globalSettings);
                Using<IUserSettingsRepository>()
                    .Setup(r => r.EnableUserSelectedPageTypes)
                    .Returns(false);
            }

            protected override void When()
            {
                base.When();
                ResultListItems = ClassUnderTest.AvailablePageTypes();
            }

            [Test]
            public void should_have_correct_number_of_page_types()
            {
                Assert.AreEqual(2, ResultListItems.Count());
            }

            [Test]
            public void should_add_all_page_types_item()
            {
                Assert.IsTrue(
                    ResultListItems
                        .Any(i => i.Text == "--- Show all ---" && i.Value == PageFilterConstants.ShowAllPageTypes));
            }

            [Test]
            public void should_add_all_page_types_item_on_correct_position()
            {
                Assert.AreEqual("--- Show all ---", ResultListItems.First().Text);
            }
        }
    }
}
