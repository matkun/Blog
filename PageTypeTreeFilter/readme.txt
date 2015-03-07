Allowing web editors to apply PageType based filtering on the EPiServer edit mode PageTree
https://blog.mathiaskunto.com/2012/02/06/allowing-web-editors-to-apply-pagetype-based-filtering-on-the-episerver-edit-mode-pagetree/

Revisited and improved: PageType based filtering of EPiServer’s EditMode PageTree
https://blog.mathiaskunto.com/2012/03/25/revisited-and-improved-pagetype-based-filtering-of-episervers-editmode-pagetree/


Version 1.1.3.0

Note: PageTypeTreeFilter now uses the EPiServer Dynamic Data Store to persist global settings, so at least EPiServer CMS 6 R2 is required.

******************************************************************************************

Usage (or just get the NuGet package):

* Add the adapter configuration below to your browser file, or drop the sample SampleAdapterMappings.browser file in your wwwroot App_Browsers directory.

<adapter controlType="EPiServer.UI.Edit.PageExplorer" adapterType="PageTypeTreeFilter.PageExplorerAdapter" />
<adapter controlType="EPiServer.UI.WebControls.PageTreeView" adapterType="PageTypeTreeFilter.PageTreeViewAdapter" />

* Move the PageTypeTreeFilter.Core\App_Code\AppStart.cs file to your web projects App_Code directory.
  - Uncomment the code and correct the namespace.

* Move the PageTypeTreeFilter.Core\lang\PageTypeTreeFilter.xml file to your web project lang directory.

* Add the PageTypeTreeFilterInitializer to your web.config file as below.

<system.webServer>
 <modules runAllManagedModulesForAllRequests="true">
  <add name="PageTypeTreeFilterInitializer" type="PageTypeTreeFilter.Bootstrap.Initializer" />
 ..

* Add the profile fields to your web.config file as below.

<profile .. >
 <properties>
  <add name="PTTF_UserPageTypes" type="System.String" />
  <add name="PTTF_EnableUsersPageTypes" type="System.Boolean" />
 ..

* Add the administrator role configuration to your web.config file as below, and make sure that WebAdmins and Administrators are the roles that you wish to allow changing the global settings.

<configuration>
  <configSections>
    <sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" >
      <section name="PageTypeTreeFilter.Properties.Settings" type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
    </sectionGroup>
  </configSections>
  <applicationSettings>
    <PageTypeTreeFilter.Properties.Settings>
      <setting name="GlobalSettingsRoles" serializeAs="String">
        <value>WebAdmins, Administrators</value>
      </setting>
    </PageTypeTreeFilter.Properties.Settings>
  </applicationSettings>
 ..


******************************************************************************************

Dependencies:

* PageTypeTreeFilter.Core
   - EPiServer.CMS.Core 6.1.379.0
   - EPiServer.Framework 6.2.267.1
   - log4net 2.0.0
   - StructureMap 2.6.3

* PageTypeTreeFilter.Test.Unit
   - AutoMoq 1.6.1
   - EPiServer.CMS.Core 6.1.379.0
   - Moq 4.0.10827
   - NUnit 2.6.0.12054
   - Unity 2.0


******************************************************************************************

Release notes:

Version 1.1.3.0

* Now applying Model-View-Presenter approach using StructureMap.
* Added unit testing with NUnit and AutoMoq.
* Using NuGet to automatically retrieve dependencies.
* Possibility to filter the PageType DropDownList globally for all user if you are an administrator.
* Possibility to filter your own PageType DropDownList if you require a special set of page types.
* Added translation possibility using EPiServer language files.
* Added logging using log4net.


Version 1.1.2.0

* Refactoring of code after review with Stefan Forsberg (http://www.popkram.com/). Thanks Stefan!
* Fixed stack overflow risk while retrieving paths to selected pages.


Version 1.1.1.0

* Moved code into separate Visual Studio 2010 project and changed namespaces.
* Boldify.css is now embedded resource.


Version 1.1.0.0

* Fixed issue with null pointer exception on Admin Mode 'Set Access Rights' page.


Version 1.0.0.0

* Initial release.


******************************************************************************************

Known Issues

* The 'PreviewFrame' disappears when opening the global settings in the 'EditPanel'; this causes the EPiServer PageTree links to stop working (as they no longer have a valid target) until the page is refreshed.
* The EPiServer.UI.dll is not included in any NuGet package.

******************************************************************************************
