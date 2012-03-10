using System;
using System.Collections.Generic;
using System.Web;
using Lemonwhale.Core.DataAccess.Lemonwhale;
using Lemonwhale.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.DataAccess.Lemonwhale
{
    public class LemonwhaleUriBuilderSpecification : AutoMockedTestBase<LemonwhaleUriBuilder>
    {
        protected UriBuilder UriBuilderResult;
        
        protected override void Given()
        {
            base.Given();

            Using<HttpContextBase>()
                .Setup(c => c.User)
                .Returns(FakeUser.ThatIsAuthenticated());

            Using<IQueryCollectionGenerator>()
                .Setup(g => g.GenerateQueryCollection(null, null, null, null, null, null, null, null))
                .Returns(new Dictionary<string, string>
                             {
                                 {"param1", "value1"},
                                 {"param2", "value2"}
                             });
        }

        public class when_using_public_api : LemonwhaleUriBuilderSpecification
        {
            protected override void When()
            {
                base.When();
                UriBuilderResult = ClassUnderTest.PublicApiUrl();
            }

            [Test]
            public void should_build_uri_to_public_api_with_query_string()
            {
                // Defined in settings values; see Lemonwhale.Test.Unit.app.config.
                const string expectedUrl = "http://lemonwhale.list.base.url:80/aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa/videos.json?param1=value1&param2=value2";
                Assert.AreEqual(expectedUrl, UriBuilderResult.ToString());
            }
        }
    }
}
