Allowing web editors to apply PageType based filtering on the EPiServer edit mode PageTree
http://blog.mathiaskunto.com/2012/02/06/allowing-web-editors-to-apply-pagetype-based-filtering-on-the-episerver-edit-mode-pagetree/

Version 1.1.1.0

******************************************************************************************

Usage

* Include the PageTypeTreeFilter.csproj in your Visual Studio solution.
* Reference it from your web project.
* Make sure all EPiServer references are OK in the PageTypeTreeFilter project.
* Add the adapter configuration below to your browser file, or drop the sample SampleAdapterMappings.browser file in your wwwroot App_Browsers directory.

<adapter controlType="EPiServer.UI.Edit.PageExplorer" adapterType="PageTypeTreeFilter.PageExplorerAdapter" />
<adapter controlType="EPiServer.UI.WebControls.PageTreeView" adapterType="PageTypeTreeFilter.PageTreeViewAdapter" />


******************************************************************************************

Release notes:

Version 1.1.1.0

* Moved code into separate Visual Studio 2010 project and changed namespaces.
* Boldify.css is now embedded resource.


Version 1.1.0.0

* Fixed issue with null pointer exception on Admin Mode 'Set Access Rights' page.


Version 1.0.0.0

* Initial release.

******************************************************************************************