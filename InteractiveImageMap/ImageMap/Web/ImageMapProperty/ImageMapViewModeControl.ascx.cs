using System;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using EPiServer.ClientScript;
using EPiServer.Core;
using EPiServer.ImageMap.Core;
using EPiServer.Web.Hosting;
using HotSpot = EPiServer.ImageMap.Core.Domain.HotSpot;
using Image = System.Drawing.Image;

namespace EPiServer.ImageMap.Web.ImageMapProperty
{
    public partial class ImageMapViewModeControl : UserControlBase, IPropertyImageMapViewModeControlView
    {
        public int MaxImageWidth { get { return 607; } }
        public Core.Domain.ImageMap ImageMap { get; set; }
        public Image Image { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Image = LoadImage();

            AddHotSpots();
            DataBindChildren();
        }

        private Image LoadImage()
        {
            var url = HttpUtility.UrlDecode(ImageMap.ImageUrl, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            var unifiedFile = HostingEnvironment.VirtualPathProvider.GetFile(url) as UnifiedFile;
            using (var stream = unifiedFile.Open())
            {
                return Image.FromStream(stream);
            }
        }

        private void AddHotSpots()
        {
            var totalWidth = Image.Size.Width;
            var totalHeight = Image.Size.Height;
            if(totalWidth > MaxImageWidth)
            {
                totalHeight = (MaxImageWidth * Image.Size.Height) / Image.Size.Width;
                totalWidth = MaxImageWidth;
            }

            // Customize culture to make float parsing easier calculating hot spot position.
            var currentCultureBackup = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            var customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            foreach (var hotSpot in ImageMap.HotSpots)
            {
                var link = new HyperLink
                {
                    ID = hotSpot.HotSpotId.ToString(),
                    ClientIDMode = ClientIDMode.Static,
                    ToolTip = HttpUtility.UrlDecode(hotSpot.Tooltip, System.Text.Encoding.GetEncoding("ISO-8859-1")),
                    CssClass = "hot-spot",
                    NavigateUrl = GetUrlFor(hotSpot.ClickTargetValue)
                };
                link.Attributes.Add("style", PositionStyleFor(hotSpot, totalWidth, totalHeight));
                phHotSpots.Controls.Add(link);
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = currentCultureBackup;
        }

        private static string PositionStyleFor(HotSpot hotSpot, int totalWidth, int totalHeight)
        {
            var left = float.Parse(hotSpot.XPosition, System.Threading.Thread.CurrentThread.CurrentCulture) * totalWidth;
            var top = float.Parse(hotSpot.YPosition, System.Threading.Thread.CurrentThread.CurrentCulture) * totalHeight;
            var width = float.Parse(hotSpot.Width, System.Threading.Thread.CurrentThread.CurrentCulture) * totalWidth;
            var height = float.Parse(hotSpot.Height, System.Threading.Thread.CurrentThread.CurrentCulture) * totalHeight;

            const string style = "left:{0}px;top:{1}px;width:{2}px;height:{3}px;";
            return string.Format(style, Math.Floor(left), Math.Floor(top), Math.Floor(width), Math.Floor(height));
        }

        private static string GetUrlFor(string pageId)
        {
            var page = DataFactory.Instance.GetPage(new PageReference(pageId));
            return page.LinkURL;
        }

        protected string UniqueIdentifier
        {
            get { return ClientScriptUtility.ToScriptSafeIdentifier(ClientID); }
        }
    }
}
