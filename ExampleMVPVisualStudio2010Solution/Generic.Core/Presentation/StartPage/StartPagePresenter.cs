using System;
using Generic.Core.Services;

namespace Generic.Core.Presentation.StartPage
{
    public class StartPagePresenter :  PresenterBase
    {
        private readonly IStartPageView _view;
        private readonly IImportantService _importantService;

        public StartPagePresenter(IStartPageView view, IImportantService importantService)
        {
            if (view == null) throw new ArgumentNullException("view");
            if (importantService == null) throw new ArgumentNullException("importantService");
            _view = view;
            _importantService = importantService;
        }

        public override void Load()
        {
            _view.ImportantHeading = GetHeading(true);
            _view.ImportantText = GetText(true);
        }

        public string GetHeading(bool useService)
        {
            return useService ?
                _importantService.GetImportantHeading("world") :
                "No heading";
        }

        public string GetText(bool useService)
        {
            return useService ?
                _importantService.GetImportantText("WORLD") :
                "No text";
        }
    }
}
