using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NUnit.Framework;
using PageTypeTreeFilter.Extensions;

namespace PageTypeTreeFilter.Test.Unit.Extensions
{
    [TestFixture]
    public class ControlExtensionsSpecification
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

        protected Control InputControl;
        protected Control ResultControl;

        public class when_finding_closest_parent_of_specific_type : ControlExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();

                var rootControl = new Control { ID = "rootControl" };
                var firstChild = new HtmlGenericControl("div") { ID = "firstChild" };
                var secondChild = new Control { ID = "secondChild" };
                var thirdChild = new Control { ID = "thirdChild" };

                rootControl.Controls.Add(firstChild);
                firstChild.Controls.Add(secondChild);
                secondChild.Controls.Add(thirdChild);

                InputControl = thirdChild;
            }

            public class when_control_exists : when_finding_closest_parent_of_specific_type
            {
                protected override void When()
                {
                    base.When();
                    ResultControl = InputControl.FindParentControlOfType<HtmlGenericControl>();
                }

                [Test]
                public void should_find_first_parent_of_type_html_generic_control()
                {
                    Assert.AreEqual("firstChild", ResultControl.ID);
                }
            }
            public class when_control_does_not_exist : when_finding_closest_parent_of_specific_type
            {
                protected override void When()
                {
                    base.When();
                    ResultControl = InputControl.FindParentControlOfType<TextBox>();
                }

                [Test]
                public void should_return_null_when_control_is_not_found()
                {
                    Assert.IsNull(ResultControl);
                }
            }
        }
        public class when_finding_child_control_by_id_recursive : ControlExtensionsSpecification
        {
            protected override void Given()
            {
                base.Given();

                var rootControl = new Control { ID = "rootControl" };
                var firstChild = new Control { ID = "firstChild" };
                var secondChild = new Control { ID = "secondChild" };
                var thirdChild = new Control { ID = "thirdChild" };

                rootControl.Controls.Add(firstChild);
                firstChild.Controls.Add(secondChild);
                secondChild.Controls.Add(thirdChild);

                InputControl = rootControl;
            }

            public class when_control_exists : when_finding_child_control_by_id_recursive
            {
                protected override void When()
                {
                    base.When();
                    ResultControl = InputControl.FindControlRecursive("secondChild");
                }

                [Test]
                public void should_find_the_control_matching_the_id()
                {
                    Assert.AreEqual("secondChild", ResultControl.ID);
                }
            }
            public class when_control_does_not_exist : when_finding_child_control_by_id_recursive
            {
                protected override void When()
                {
                    base.When();
                    ResultControl = InputControl.FindControlRecursive("nonExistingChild");
                }

                [Test]
                public void should_return_null_when_control_is_not_found()
                {
                    Assert.IsNull(ResultControl);
                }
            }
        }
    }
}
