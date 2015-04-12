Mathias Kunto's Blog Repository - Samples and Examples
https://blog.mathiaskunto.com/

Contains examples, code snippets and samples that did not get their own projects and will not be compilable on their own.

*****************************************************************************
Descriptions:

*****************************************************************************

ConfigFileCopyPowerShellScript
https://blog.mathiaskunto.com/2015/04/11/powershell-script-example-for-copying-correct-configs-based-on-server-machine-name-in-multi-server-environment/

PowerShell script example for copying the correct configuration files for the proper server based on server machine name.



EmbeddedEPiServerResources
https://blog.mathiaskunto.com/2012/02/27/how-to-embed-episerver-resources-such-as-guiplugins-and-pages-together-with-images-javascripts-and-stylesheets-in-droppable-assembly-binaries/

Example on how to embed EPiServer resources such as a GuiPlugIn or a page, as well as JavaScripts, Stylesheets and images.



EPiServerEditTreeTooltip_PageUsages
https://blog.mathiaskunto.com/2012/04/22/visual-aid-for-finding-page-usages-directly-in-episerver-edit-mode-page-tree-extending-node-tooltips/

Adding tooltip to the EPiServer edit mode page tree containing information on what pages are referencing the current page or one of its decending pages.



EPiServerFileSystemSurveillance
https://blog.mathiaskunto.com/2012/05/24/how-to-make-episerver-surveil-your-config-files-notifying-you-on-change/

Example code for making EPiServer monitor changes in your configuration files and notify you via Google's Gmail SMTP servers when someone changes them.



EPiServerJob_RunPowerShellScript
https://blog.mathiaskunto.com/2012/02/03/making-the-episerver-scheduler-run-your-windows-powershell-scripts/

An example on how to have the EPiServer scheduler run Windows PowerShell scripts for you.



EPiServerMSBuildServerSpecificConfigTransforms
https://blog.mathiaskunto.com/2015/04/11/extending-episerver-msbuild-config-transforms-to-provide-server-specific-configuration-for-tcp-eventreplication-and-licenses/

Sample MSBuild setup for having server specifict configuration transformation for deploying different ones on each server automatically. This may be useful while for instance needing to use TCP for EPiServer's EventReplication service, working with ImageVault loadbalancing or just deploying the correct License.config file to the correct server in an easy fashion.



EPiServerPopupWindows
https://blog.mathiaskunto.com/2012/07/14/how-to-open-those-episerver-edit-mode-selector-browser-popups-in-your-custom-properties/

Examples on how to open the build-in EPiServer browser controls for the Date Selector (DateTime information), the FileSelector (Images or Documents from EPiServer VPP), the Page Selector (EPiServer Pages), the Url Selector (URLs to external websites, internal EPiServer pages, e-mails, files on network or other websites) as well as the XForm Selector. This is done manually without using server side EPiServer controls.



EPiServerPropertyAppearance
https://blog.mathiaskunto.com/2012/03/05/being-friends-with-the-propertycontrolclassfactory-or-101-ways-to-change-episerver-built-in-property-appearances/

Five examples on how it is possible to change appearance of built-in EPiServer properties using either the PropertyControlClassFactory class through web.config, an initialization module, a plugin attribute or global.asax, or a control adapter mapped through a browser file.



FuBuMVC_RadioButtonList
https://blog.mathiaskunto.com/2012/08/31/how-to-use-htmlconventions-to-create-a-radio-button-list-input-for-fubumvc/

A way of rendering radio button lists in FuBuMVC from enums using FuBuMVC's HtmlConvention.



GeoIPAutoUpdate_PowerShellScript
https://blog.mathiaskunto.com/2012/01/15/geoip-database-update-in-multiple-webserver-environment-using-windows-powershell-scripting/

A script for automatically downloading and updating the MaxMind GeoIP database (shipped with EPiServer) on multiple servers. Requires gzip.exe (may be downloaded from the gzip homepage: https://www.gzip.org/). Requires Get-WebFile v3.6 by Joel Bennett (may be downloaded from the PowerShell Code Repository: https://poshcode.org/417).



NLogBridgeAppenderLog4Net
https://blog.mathiaskunto.com/2014/08/21/using-nlog-with-episerver-and-log4net/

An appender for log4net bridging the log messages to NLog, along with sample configuration for EPiServer CMS.



ShowingHiddenEPiServerProperties
https://blog.mathiaskunto.com/2013/01/16/simple-adapter-for-showing-hidden-episerver-properties-in-edit-mode-for-administrators

A simple control adapter for giving access to hidden EPiServer properties (those with the 'Display in Edit Mode' option turned off in Admin Mode) to users belonging to certain roles; such as web administrators.



StaticEPiServerErrorPages
https://blog.mathiaskunto.com/2011/08/19/allowing-web-editors-to-customize-static-error-pages-in-episerver/

How to allow EPiServer web editors to create static error pages that will work without a database connection.


