using System;
using System.Collections.Generic;
using System.Linq;
using Lemonwhale.Core.Domain.Lemonwhale;
using Lemonwhale.Core.Framework.Lemonwhale;
using Lemonwhale.Test.Unit.TestHelpers;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Lemonwhale.Test.Unit.Framework.Lemonwhale
{
    public class MediaMapperSpecification : AutoMockedTestBase<MediaMapper>
    {
        protected LemonwhaleMedia ResultMedia;
        protected JObject InputJson;

        protected override void When()
        {
            base.When();
            ResultMedia = ClassUnderTest.Map(InputJson);
        }

        public class when_there_is_no_json_media : MediaMapperSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputJson = null;
                Using<ICategoryParser>()
                    .Setup(p => p.ParseCategoryIds(null))
                    .Returns(Enumerable.Empty<Guid>());
            }

            [Test]
            public void should_return_empty_media_result()
            {
                Assert.AreEqual(Guid.Empty, ResultMedia.Id);
            }
        }
        public class when_values_are_not_set_in_json_media : MediaMapperSpecification
        {
            protected override void Given()
            {
                base.Given();
                InputJson = new JObject();
                Using<ICategoryParser>()
                    .Setup(p => p.ParseCategoryIds(null))
                    .Returns(Enumerable.Empty<Guid>());
            }

            [Test]
            public void should_map_default_name()
            {
                Assert.IsNullOrEmpty(ResultMedia.Name);
            }

            [Test]
            public void should_map_default_id()
            {
                Assert.AreEqual(Guid.Empty.ToString(), ResultMedia.Id.ToString());
            }

            [Test]
            public void should_map_default_description()
            {
                Assert.IsNullOrEmpty(ResultMedia.Description);
            }

            [Test]
            public void should_map_default_duration()
            {
                Assert.AreEqual(0, ResultMedia.Duration);
            }

            [Test]
            public void should_map_default_views()
            {
                Assert.AreEqual(0, ResultMedia.Views);
            }

            [Test]
            public void should_map_default_creation_date()
            {
                Assert.AreEqual(DateTime.MinValue, ResultMedia.Created);
            }

            [Test]
            public void should_map_all_default_categories()
            {
                Assert.AreEqual(0, ResultMedia.CategoryIds.Count());
            }

            [Test]
            public void should_map_default_thumbnail_url()
            {
                Assert.IsNullOrEmpty(ResultMedia.ThumbnailImageUrl);
            }

            [Test]
            public void should_map_default_image_url()
            {
                Assert.IsNullOrEmpty(ResultMedia.ImageUrl);
            }

            [Test]
            public void should_map_default_published_date()
            {
                Assert.AreEqual(DateTime.MinValue, ResultMedia.Published);
            }

            [Test]
            public void should_map_default_updated_date()
            {
                Assert.AreEqual(DateTime.MinValue, ResultMedia.Updated);
            }
        }
        public class when_there_is_json_media : MediaMapperSpecification
        {
            protected override void Given()
            {
                base.Given();

                InputJson = new JObject(
                    new JProperty("name", "Wagner bom EM-final Berlin 2011"),
                    new JProperty("id", "b21d9261-ac60-4f0a-bba9-9fbb7145893f"),
                    new JProperty("description", "A video about Wagner."),
                    new JProperty("duration", "90928"),
                    new JProperty("views", "5"),
                    new JProperty("created_at", "2012-03-07T13:22:23+0100"),
                    new JProperty("userid", "3a743ee9-947a-4db6-bc7e-138ce6ac6cfa"), // Intentionally not mapped
                    new JProperty("categoryid", "29b38774-ea34-4930-ad4e-830706eada92,aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa , bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    new JProperty("images", new JObject(
                        new JProperty("thumbnail_url", "http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1124x70.jpg"),
                        new JProperty("normal_image_url", "http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1.jpg"))),
                    new JProperty("siteid", "45069cd7-e3c6-43f3-8424-6f80ad0ed669"), // Intentionally not mapped
                    new JProperty("published_at", "2012-03-07T13:22:23+0100"),
                    new JProperty("updated_at", "2012-03-07T13:26:55+0100"));

                Using<ICategoryParser>()
                    .Setup(p => p.ParseCategoryIds("29b38774-ea34-4930-ad4e-830706eada92,aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa , bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"))
                    .Returns(new List<Guid>
                                 {
                                     new Guid("29b38774-ea34-4930-ad4e-830706eada92"),
                                     new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                                     new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                                 });
            }

            [Test]
            public void should_map_media_name()
            {
                Assert.AreEqual("Wagner bom EM-final Berlin 2011", ResultMedia.Name);
            }

            [Test]
            public void should_map_media_id()
            {
                Assert.AreEqual("b21d9261-ac60-4f0a-bba9-9fbb7145893f", ResultMedia.Id.ToString());
            }

            [Test]
            public void should_map_media_description()
            {
                Assert.AreEqual("A video about Wagner.", ResultMedia.Description);
            }

            [Test]
            public void should_map_media_duration()
            {
                Assert.AreEqual(90928, ResultMedia.Duration);
            }

            [Test]
            public void should_map_media_views()
            {
                Assert.AreEqual(5, ResultMedia.Views);
            }

            [Test]
            public void should_map_media_creation_date()
            {
                Assert.AreEqual(new DateTime(2012, 03, 07, 13, 22, 23), ResultMedia.Created);
            }

            [Test]
            public void should_map_all_media_categories()
            {
                Assert.AreEqual(3, ResultMedia.CategoryIds.Count());
            }

            [Test]
            public void should_map_media_categories()
            {
                Assert.AreEqual("29b38774-ea34-4930-ad4e-830706eada92", ResultMedia.CategoryIds.First().ToString());
                Assert.AreEqual("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", ResultMedia.CategoryIds.Skip(1).First().ToString());
                Assert.AreEqual("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb", ResultMedia.CategoryIds.Last().ToString());
            }

            [Test]
            public void should_map_media_thumbnail_url()
            {
                Assert.AreEqual("http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1124x70.jpg", ResultMedia.ThumbnailImageUrl);
            }

            [Test]
            public void should_map_media_image_url()
            {
                Assert.AreEqual("http://pastdlwcd.lwcdn.com/v-i-b21d9261-ac60-4f0a-bba9-9fbb7145893f-1.jpg", ResultMedia.ImageUrl);
            }

            [Test]
            public void should_map_media_published_date()
            {
                Assert.AreEqual(new DateTime(2012, 03, 07, 13, 22, 23), ResultMedia.Published);
            }

            [Test]
            public void should_map_media_updated_date()
            {
                Assert.AreEqual(new DateTime(2012, 03, 07, 13, 26, 55), ResultMedia.Updated);
            }
        }
    }
}
