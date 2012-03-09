namespace Lemonwhale.Core.Presentation
{
    public abstract class PresenterBase
    {
        public virtual void FirstTimeInit() {}
        public virtual void FirstTimeLoad() {}
        public virtual void Load() {}
        public virtual void PreRender() {}
    }
}
