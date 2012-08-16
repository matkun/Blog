namespace EPiServer.ImageMap.Core
{
    public interface IPropertyImageMapEditModeView
    {
        string InitialImageMap { get; set; }
        string UpdatedImageMap { get; }
    }
}
