Supplying EPiServer scheduled jobs with parameters through Admin Mode
http://blog.mathiaskunto.com/2012/02/13/supplying-episerver-scheduled-jobs-with-parameters-through-admin-mode

Version 1.1.0.0

******************************************************************************************

Installation:

* Include the ScheduledParameterJob.csproj in your Visual Studio solution.
* Reference it from your web project.
* Make sure all EPiServer references are OK in the ScheduledParameterJob project.
* Add the adapter configuration below to your browser file, or drop the sample SampleAdapterMappings.browser file in your wwwroot App_Browsers directory.

<adapter controlType="EPiServer.UI.Admin.DatabaseJob" adapterType="ScheduledParameterJob.DatabaseJobAdapter" />

Usage:

* See blog post for usage.
* See ExampleJob directory for example job; not included in droppable binary version, nor in the NuGet package.
  - Get it from the blog post or GitHub (https://github.com/matkun/Blog/tree/master/ScheduledParameterJob/ExampleJob)

******************************************************************************************

Release notes:

Version 1.1.0.0

* Moved core files to separate project.
* Reset button image reset.png is now base64 encoded inside the CSS file JobParameters.css.
* JobParameters.css is now an embedded resource.
* Example scheduled job with parameters is moved to the ExampleJob directory, not included in the droppable binary version nor the NuGet package.


Version 1.0.0.0

* Initial release.

******************************************************************************************