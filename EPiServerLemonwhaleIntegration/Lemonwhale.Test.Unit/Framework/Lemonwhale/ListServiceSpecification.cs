using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemonwhale.Core.Framework.Lemonwhale;
using Lemonwhale.Test.Unit.TestHelpers;
using NUnit.Framework;

namespace Lemonwhale.Test.Unit.Framework.Lemonwhale
{
    public class ListServiceSpecification : AutoMockedTestBase<ListService>
    {
        protected IEnumerable<LemonwhaleMedia> Result; 

        protected override void When()
        {
            base.When();
            Result = ClassUnderTest.GetMedia();
        }

        public class WhenNoMediaIsReturnedFrom : ListServiceSpecification
        {
            protected override void Given()
            {
                base.Given();

            }

            [Test]
            public void should_return_empty_collection()
            {
                Assert.IsEmpty(Result);
            }
        }
    }
}
