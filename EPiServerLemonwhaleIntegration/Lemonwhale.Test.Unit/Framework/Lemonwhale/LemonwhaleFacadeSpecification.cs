using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemonwhale.Core.AdapterPattern;
using Lemonwhale.Core.DataAccess.Lemonwhale;
using Lemonwhale.Core.Domain.Lemonwhale;
using Lemonwhale.Core.Framework.Lemonwhale;
using Lemonwhale.Test.Unit.TestHelpers;
using Moq;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace Lemonwhale.Test.Unit.Framework.Lemonwhale
{
    public class LemonwhaleFacadeSpecification : AutoMockedTestBase<LemonwhaleFacade>
    {
        protected IEnumerable<LemonwhaleMedia> Result;
        protected string JsonString;

        protected override void Given()
        {
            base.Given();
            Using<ILemonwhaleUriBuilder>()
                .Setup(b => b.PublicApiUrl(null, null, null, null, null, null, null, null))
                .Returns(new UriBuilder("http://fakeurl.se/"));

            Using<IWebClientWrapper>()
                .Setup(wc => wc.DownloadString(It.IsAny<string>()))
                .Returns(JsonString);
        }

        protected override void When()
        {
            base.When();
            Result = ClassUnderTest.GetAllPublicMedia();
        }

        public class when_no_media_is_returned_from_api : LemonwhaleFacadeSpecification
        {
            protected override void Given()
            {
                JsonString = @"{""videos"": []}";
                base.Given();
            }
            [Test]
            public void should_return_empty_media_collection()
            {
                Assert.IsEmpty(Result);
            }
        }
        public class when_one_video_is_returned_from_api : LemonwhaleFacadeSpecification
        {
            protected override void Given()
            {
                JsonString =
                    @"
{
""videos"":
[{
""name"":""Video name"",
""id"":""11111111-1111-1111-1111-111111111111"",
""description"":""Description for video."",
""duration"":35,
""views"":4,
""created_at"":""2010-01-01T13:04:00+0100"",
""userid"":""bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"",
""categoryid"":""99999999-9999-9999-9999-999999999999,88888888-8888-8888-8888-888888888888,77777777-7777-7777-7777-777777777777"",
""images"":{
""thumbnail_url"":""http://fakeurl.se/fakethumbnail.jpg"",
""normal_image_url"":""http://fakeurl.se/fakeimage.jpg""
},
""siteid"":""aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"",
""published_at"":""2010-03-11T13:04:00+0100"",
""updated_at"":""2011-04-12T14:08:10+0100""
}]
}";
                base.Given();
                Using<IMediaMapper>()
                    .Setup(m => m.Map(It.IsAny<JObject>()))
                    .Returns(new LemonwhaleMedia { Id = new Guid("11111111-1111-1111-1111-111111111111") });
            }

            [Test]
            public void should_get_one_video_from_api()
            {
                Assert.AreEqual(1, Result.Count());
            }

            [Test]
            public void should_get_correct_video()
            {
                Assert.AreEqual("11111111-1111-1111-1111-111111111111", Result.First().Id.ToString());
            }
        }
        public class when_multiple_videos_are_returned_from_api : LemonwhaleFacadeSpecification
        {
            protected override void Given()
            {
                JsonString =
                    @"
{
""videos"":
[{
""name"":""Video name"",
""id"":""11111111-1111-1111-1111-111111111111"",
""description"":""Description for video."",
""duration"":35,
""views"":4,
""created_at"":""2010-01-01T13:04:00+0100"",
""userid"":""bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"",
""categoryid"":""99999999-9999-9999-9999-999999999999,88888888-8888-8888-8888-888888888888,77777777-7777-7777-7777-777777777777"",
""images"":{
""thumbnail_url"":""http://fakeurl.se/fakethumbnail.jpg"",
""normal_image_url"":""http://fakeurl.se/fakeimage.jpg""
},
""siteid"":""aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"",
""published_at"":""2010-03-11T13:04:00+0100"",
""updated_at"":""2011-04-12T14:08:10+0100""
},
{
""name"":""Testing 123"",
""id"":""22222222-2222-2222-2222-222222222222"",
""description"":""Im a test."",
""duration"":30,
""views"":0,
""created_at"":""2012-02-20T09:20:09+0100"",
""userid"":""44151214-733e-4408-8fb3-02b53d9ad35e"",
""categoryid"":""e22cf116-e825-4b04-8ac7-834b4c80f631"",
""images"":{
""thumbnail_url"":""http://fakeurl.se/fakethumbnail.jpg"",
""normal_image_url"":""http://fakeurl.se/fakeimage.jpg""
},
""siteid"":""b2801424-fef4-4594-8b7d-200466fdc8db"",""published_at"":""2012-02-20T09:20:00+0100"",""updated_at"":""2012-03-07T15:38:47+0100""
},
{
""name"":""Testing 123"",
""id"":""33333333-3333-3333-3333-333333333333"",
""description"":""Im a test."",
""duration"":30,
""views"":0,
""created_at"":""2012-02-20T09:20:09+0100"",
""userid"":""44151214-733e-4408-8fb3-02b53d9ad35e"",
""categoryid"":""e22cf116-e825-4b04-8ac7-834b4c80f631"",
""images"":{
""thumbnail_url"":""http://fakeurl.se/fakethumbnail.jpg"",
""normal_image_url"":""http://fakeurl.se/fakeimage.jpg""
},
""siteid"":""b2801424-fef4-4594-8b7d-200466fdc8db"",""published_at"":""2012-02-20T09:20:00+0100"",""updated_at"":""2012-03-07T15:38:47+0100""
}]
}";
                base.Given();
                Using<IMediaMapper>()
                    .Setup(m => m.Map(It.Is<JObject>(o => (string)o["id"] == "11111111-1111-1111-1111-111111111111")))
                    .Returns(new LemonwhaleMedia { Id = new Guid("11111111-1111-1111-1111-111111111111") });
                Using<IMediaMapper>()
                    .Setup(m => m.Map(It.Is<JObject>(o => (string)o["id"] == "22222222-2222-2222-2222-222222222222")))
                    .Returns(new LemonwhaleMedia { Id = new Guid("22222222-2222-2222-2222-222222222222") });
                Using<IMediaMapper>()
                    .Setup(m => m.Map(It.Is<JObject>(o => (string)o["id"] == "33333333-3333-3333-3333-333333333333")))
                    .Returns(new LemonwhaleMedia { Id = new Guid("33333333-3333-3333-3333-333333333333") });
            }

            [Test]
            public void should_get_one_video_from_api()
            {
                Assert.AreEqual(3, Result.Count());
            }

            [Test]
            public void should_get_correct_video()
            {
                Assert.AreEqual("11111111-1111-1111-1111-111111111111", Result.First().Id.ToString());
                Assert.AreEqual("22222222-2222-2222-2222-222222222222", Result.Skip(1).First().Id.ToString());
                Assert.AreEqual("33333333-3333-3333-3333-333333333333", Result.Last().Id.ToString());
            }
        }
    }
}



//JsonString =
//                    @"
//{
//""videos"":
//[{
//""name"":""Video A"",
//""id"":""02c26624-4c68-4b48-90f7-4d4cb6722d7f"",
//""description"":""Video A"",
//""duration"":35,
//""views"":4,
//""created_at"":""2011-03-11T13:04:00+0100"",
//""userid"":""f0707be2-aa07-48ab-a448-acd0c9729362"",
//""categoryid"":""e22cf116-e825-4b04-8ac7-834b4c80f631"",
//""images"":{
//""thumbnail_url"":""http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0124x70.jpg"",
//""normal_image_url"":""http://pae1d8a01.lwcdn.com/v-i-02c26624-4c68-4b48-90f7-4d4cb6722d7f-0.jpg""
//},
//""siteid"":""b2801424-fef4-4594-8b7d-200466fdc8db"",
//""published_at"":""2011-03-11T13:04:00+0100"",
//""updated_at"":""2011-03-11T14:08:10+0100""
//},
//
//{
//""name"":""Testing 123"",
//""id"":""384a9101-95f6-4c16-9285-20d2763f8f6f"",
//""description"":""Im a test."",
//""duration"":30,
//""views"":0,
//""created_at"":""2012-02-20T09:20:09+0100"",
//""userid"":""44151214-733e-4408-8fb3-02b53d9ad35e"",
//""categoryid"":""e22cf116-e825-4b04-8ac7-834b4c80f631"",
//""images"":{
//""thumbnail_url"":""http://pae1d8a01.lwcdn.com/v-i-384a9101-95f6-4c16-9285-20d2763f8f6f-1124x70.jpg"",
//""normal_image_url"":""http://pae1d8a01.lwcdn.com/v-i-384a9101-95f6-4c16-9285-20d2763f8f6f-1.jpg""
//},
//""siteid"":""b2801424-fef4-4594-8b7d-200466fdc8db"",""published_at"":""2012-02-20T09:20:00+0100"",""updated_at"":""2012-03-07T15:38:47+0100""
//}
//]}";