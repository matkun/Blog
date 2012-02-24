namespace Generic.Core.Presentation.StartPage
{
    public interface IStartPageView : IView
    {
        string ImportantHeading { get; set; }
        string ImportantText { get; set; }
    }
}
