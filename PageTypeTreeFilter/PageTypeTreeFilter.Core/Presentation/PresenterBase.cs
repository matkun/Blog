namespace PageTypeTreeFilter.Presentation
{
    public abstract class PresenterBase
    {
        public virtual void PreInit() {}
        public virtual void FirstTimeInit() {}
        public virtual void Init() {}
        public virtual void FirstTimeLoad() {}
        public virtual void Load() {}
        public virtual void PreRender() {}
    }
}
