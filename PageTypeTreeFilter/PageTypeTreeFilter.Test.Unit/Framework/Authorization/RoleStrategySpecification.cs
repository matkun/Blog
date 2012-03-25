using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NUnit.Framework;
using PageTypeTreeFilter.Framework;
using PageTypeTreeFilter.Framework.Authorization;
using PageTypeTreeFilter.Test.Unit.TestHelpers;

namespace PageTypeTreeFilter.Test.Unit.Framework.Authorization
{
    public class RoleStrategySpecification : AutoMockedTestBase<RoleStrategy>
    {
        protected string AdministratorRoles;

        protected override void Given()
        {
            base.Given();
            Using<ISettings>()
                .Setup(s => s.GlobalSettingsRoles)
                .Returns(AdministratorRoles);
        }
        
        public class when_checking_if_user_is_an_administrator : RoleStrategySpecification
        {
            protected IEnumerable<string> InputUserRoles;
            protected bool IsAdministrator;

            protected override void Given()
            {
                AdministratorRoles = "WebAdmins,Administrators,MadeUpAdmins";
                base.Given();
                var fakeUser = new FakeUser { UserRoles = InputUserRoles };
                Using<HttpContextBase>()
                    .Setup(c => c.User)
                    .Returns(fakeUser);
            }

            protected override void When()
            {
                base.When();
                IsAdministrator = ClassUnderTest.IsAdministrator();
            }

            public class when_user_is_not_in_admin_role : when_checking_if_user_is_an_administrator
            {
                protected override void Given()
                {
                    InputUserRoles = new List<string> {"WebEditors", "OtherRole"};
                    base.Given();
                }

                [Test]
                public void should_not_be_an_administrator()
                {
                    Assert.IsFalse(IsAdministrator);
                }
            }
            public class when_user_in_admin_role : when_checking_if_user_is_an_administrator
            {
                protected override void Given()
                {
                    InputUserRoles = new List<string> { "WebEditors", "WebAdmins", "OtherRole" };
                    base.Given();
                }

                [Test]
                public void should_not_be_an_administrator()
                {
                    Assert.IsTrue(IsAdministrator);
                }
            }
        }
        public class when_retrieving_administrator_roles : RoleStrategySpecification
        {
            protected IEnumerable<string> ResultRoles;

            protected override void When()
            {
                base.When();
                ResultRoles = ClassUnderTest.AdministratorRoles();
            }

            public class when_roles_are_not_set : when_retrieving_administrator_roles
            {
                protected override void Given()
                {
                    AdministratorRoles = null;
                    base.Given();
                }

                [Test]
                public void should_return_empty_list_of_roles()
                {
                    Assert.IsEmpty(ResultRoles);
                }
            }
            public class when_roles_is_empty : when_retrieving_administrator_roles
            {
                protected override void Given()
                {
                    AdministratorRoles = string.Empty;
                    base.Given();
                }

                [Test]
                public void should_return_empty_list_of_roles()
                {
                    Assert.IsEmpty(ResultRoles);
                }
            }
            public class when_there_is_one_role : when_retrieving_administrator_roles
            {
                protected override void Given()
                {
                    AdministratorRoles = "WebAdmins";
                    base.Given();
                }

                [Test]
                public void should_return_one_role()
                {
                    Assert.AreEqual(1, ResultRoles.Count());
                }

                [Test]
                public void should_return_correct_role()
                {
                    Assert.IsTrue(ResultRoles.Any(r => r == "WebAdmins"));
                }
            }
            public class when_there_are_many_roles : when_retrieving_administrator_roles
            {
                protected override void Given()
                {
                    AdministratorRoles = "WebAdmins,Administrators,FakeAdmins";
                    base.Given();
                }

                [Test]
                public void should_return_all_roles()
                {
                    Assert.AreEqual(3, ResultRoles.Count());
                }

                [Test]
                public void should_return_correct_roles()
                {
                    Assert.IsTrue(ResultRoles.Any(r => r == "WebAdmins"));
                    Assert.IsTrue(ResultRoles.Any(r => r == "Administrators"));
                    Assert.IsTrue(ResultRoles.Any(r => r == "FakeAdmins"));
                }
            }
            public class when_role_string_is_malformed : when_retrieving_administrator_roles
            {
                protected override void Given()
                {
                    AdministratorRoles = ", WebAdmins ,  Administrators,,FakeAdmins, ";
                    base.Given();
                }

                [Test]
                public void should_return_all_roles()
                {
                    Assert.AreEqual(3, ResultRoles.Count());
                }

                [Test]
                public void should_return_correct_roles()
                {
                    Assert.IsTrue(ResultRoles.Any(r => r == "WebAdmins"));
                    Assert.IsTrue(ResultRoles.Any(r => r == "Administrators"));
                    Assert.IsTrue(ResultRoles.Any(r => r == "FakeAdmins"));
                }
            }
        }
    }
}
