EPiServer CustomProperty: jQuery image slide show with HTML captions and smooth transitions using Dev7studios Nivo Slider
http://blog.mathiaskunto.com/2012/06/24/episerver-customproperty-jquery-image-slide-show-with-html-captions-and-smooth-transitions-using-dev7studios-responsive-nivo-slider/


Version 1.0.0.0

******************************************************************************************

Usage (or just get the NuGet package from EPiServer's NuGet feed):

* Move the ImageSlideShow.Core\lang\ImageSlideShow.xml to the EPiServer language directory.

* Move the ImageSlideShow.Core\App_Code\AppStart.cs file to your web projects App_Code directory.
  - Uncomment the code and correct the namespace (This will register the image slide show embedded resource provider).

* Add the ImageSlideShowInitializer to your web.config file as below.

<system.webServer>
 <modules runAllManagedModulesForAllRequests="true">
  <add name="ImageSlideShowInitializer" type="ImageSlideShow.Core.Bootstrap.Initializer" />
 ..

* Reference the ImageSlideShow project from your own, or drop the assembly file.

* Make sure that you are adding jQuery to the view mode page where you want to use the slider; for instance version 1.7.2.


******************************************************************************************

Dependencies:

* ImageSlideShow.Core
   - EPiServer.CMS.Core 6.1.379.0
   - EPiServer.Framework 6.2.267.1
   - Newtonsoft.Json 4.5.7
   - log4net 2.0.0
   - StructureMap 2.6.3


******************************************************************************************

Release notes:

Version 1.0.0.0

* Initial release.


******************************************************************************************

Current limitations:

* Only default theme available for the Nivo Slider; not yet possible to add your own.
* No TinyMCE support in caption textarea for the image slide editor, even though HTML is allowed.
* Linked slides always open in same window when clicked.


******************************************************************************************

Known Issues:

* The EPiServer.UI.dll is not included in any NuGet package.


******************************************************************************************
