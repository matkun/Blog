using System;
using System.Globalization;
using EPiServer;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using ScheduledParameterJob.Extensions;

namespace ScheduledParameterJob.ExampleJob
{
    [ScheduledPlugInWithParameters(
        DisplayName = "Sample parameter job",
        Description = "Sample job with parameters",
        DefinitionsClass = "ScheduledParameterJob.ExampleJob.DefinitionSample",
        DefinitionsAssembly = "ScheduledParameterJob"
    )]
    public class SampleParameterJob : ScheduledJob
    {
        public static string Execute()
        {
            var descriptor = PlugInDescriptor.Load("ScheduledParameterJob.ExampleJob.SampleParameterJob", "ScheduledParameterJob");
            var store = typeof (ScheduledJobParameters).GetStore();
            var parameters = store.LoadPersistedValuesFor(descriptor.ID.ToString(CultureInfo.InvariantCulture));

            var cbChecked = parameters.ContainsKey("CheckBoxSample") && (bool) parameters["CheckBoxSample"] ? "Aye!" : "Nay..";
            var tbText = parameters.ContainsKey("TextBoxSample") ? parameters["TextBoxSample"] as string : string.Empty;
            var sampleReference = parameters.ContainsKey("InputPageReferenceSample") ? (PageReference)parameters["InputPageReferenceSample"] : PageReference.EmptyReference;
            var samplePageName = sampleReference != null && sampleReference != PageReference.EmptyReference ? DataFactory.Instance.GetPage(sampleReference).PageName : string.Empty;
            var cDateTime = parameters.ContainsKey("CalendarSample") ? (DateTime?)parameters["CalendarSample"] : null;
            var ddlSelectedValue = parameters.ContainsKey("DropDownListSample") ? parameters["DropDownListSample"] as string : string.Empty;
            
            var result = string.Empty;
            result += string.Format("CheckBoxSample checked: <b>{0}</b><br />", cbChecked);
            result += string.Format("TextBoxSample text: <b>{0}</b><br />", tbText);
            result += string.Format("InputPageReferenceSample page name: <b>{0}</b> (PageId: <b>{1}</b>)<br />", samplePageName, sampleReference);
            result += string.Format("CalendarSample date: <b>{0}</b><br />", cDateTime.ToString());
            result += string.Format("DropDownListSample selected value: <b>{0}</b><br />", ddlSelectedValue);
            return result;
        }
    }
}
