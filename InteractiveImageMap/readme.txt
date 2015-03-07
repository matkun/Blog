EPiServer custom property: Allowing web editors to create image maps with flexible hot spot areas
https://blog.mathiaskunto.com/2012/08/16/episerver-custom-property-allowing-web-editors-to-create-image-maps-with-flexible-hot-spot-areas/

This release is not a stand alone project resulting in a droppable binary nor a NuGet package, you will have to copy it into your EPiServer website project yourself should you wish to try it.

If you would like to use this with EPiServer 5, please see the following article:
Moving the ImageMap editor custom property to EPiServer 5
https://blog.mathiaskunto.com/2013/08/23/moving-the-imagemap-editor-custom-property-to-episerver-5/

Version 1.0.0.0

******************************************************************************************

Installation notes:

* Use NuGet to add Newtonsoft Json.NET binary or download the binary (Newtonsoft.Json.dll) from the website (http://james.newtonking.com/projects/json-net.aspx).

* Change the CSS link paths in the PluginMaster.Master file to point to your own UI.

* Change the EPi-PageBrowserDialog path in the HotSpotEditor.aspx file to point to your own UI.

* Update namespaces to where you placed the files, and see to it that other paths (like the one pointing out the HotSpotEditor.aspx) are correct.

* You might want to move inline style tags to your CSS files.

* Make sure you do not get any conflicts with existing jQuery includes; the ones included are inserted for the plug-in to work.


******************************************************************************************

Usage:

* Add the custom property to a page type.

* Insert the following line of code into the page type's page template, with your PropertyName of course:
    <EPiServer:Property PropertyName="InteractiveImage" runat="server" />

******************************************************************************************

Dependencies:

* Newtonsoft Json.NET
* jQuery 1.7.2
* jQuery UI 1.8.22
* jQuery json
* json2 (JSON property fix by Douglas Crockford, https://github.com/douglascrockford/JSON-js/blob/master/json2.js)


******************************************************************************************

Release notes:

Version 1.0.0.0

* Initial release.


******************************************************************************************

