This code is described in: "Keeping your hair from turning gray, or: How to style the asp:FileUpload control using control adapters"
http://blog.mathiaskunto.com/2012/01/28/keeping-your-hair-from-turning-gray-or-how-to-style-the-aspfileupload-control-using-control-adapters

Version 1.1.0.0

******************************************************************************************

Usage

* Add a JQuery reference if you are not already using it in your solution.
  - Hot linked example, <script type="text/javascript" language="javascript" src="http://code.jquery.com/jquery-1.7.1.min.js"></script>
* Include the StylableFileUploadControl.csproj in your Visual Studio solution.
* Reference it from your web project.
* Add the adapter configuration below to your browser file, or drop the sample SampleAdapterMappings.browser file in your wwwroot App_Browser directory.

<adapter controlType="System.Web.UI.WebControls.FileUpload" adapterType="StylableFileUploadControl.FileUploadAdapter" />

******************************************************************************************

Release notes:

Version 1.1.0.0

* Moved code into separate Visual Studio 2010 project and changed namespaces.
* FileUploadAdapter.js is now embedded resource.
* JQuery file jquery-1.7.1.min.js is no longer included from control adapter, but hot linked only from the example FileUploadExample.aspx page.


Version 1.0.0.0

* Initial release.

******************************************************************************************