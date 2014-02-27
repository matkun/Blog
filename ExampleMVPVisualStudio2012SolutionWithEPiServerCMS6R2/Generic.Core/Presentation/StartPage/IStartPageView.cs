namespace Generic.Core.Presentation.StartPage
{
    public interface IStartPageView : IView<StartPage>
    {
        string ImportantHeading { get; set; }
        string ImportantText { get; set; }
    }
}
