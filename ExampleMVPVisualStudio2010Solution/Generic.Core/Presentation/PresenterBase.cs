namespace Generic.Core.Presentation
{
    public abstract class PresenterBase
    {
        public virtual void Load() {}
        public virtual void PreRender() {}
        public virtual void FirstTimeInit() {}
    }
}
