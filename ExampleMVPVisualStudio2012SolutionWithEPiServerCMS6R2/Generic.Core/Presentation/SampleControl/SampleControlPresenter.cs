using System;
using Generic.Core.Services;

namespace Generic.Core.Presentation.SampleControl
{
    public class SampleControlPresenter : PresenterBase
    {
        private readonly ISampleControlView _view;
        private readonly IImportantService _importantService;

        public SampleControlPresenter(ISampleControlView view, IImportantService importantService)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (importantService == null) throw new ArgumentNullException("importantService");
            _view = view;
            _importantService = importantService;
        }

        public override void Load()
        {
            base.Load();
            _view.ControlText = GetControlText(true);
        }

        public string GetControlText(bool useService)
        {
            return useService ?
                _importantService.GetImportantHeading("ControlText") :
                "Default text";
        }
    }
}
