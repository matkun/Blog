using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.ClientScript;
using EPiServer.Data.Dynamic;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.Shell.WebForms;
using EPiServer.UI;
using EPiServer.UI.Admin;
using Newtonsoft.Json;
using ScheduledParameterJobEPiServer75.Extensions;

[assembly: WebResource("ScheduledParameterJobEPiServer75.Style.JobParameters.css", "text/css")]
namespace ScheduledParameterJobEPiServer75
{
    public class DatabaseJobAdapter : PageAdapter
    {
        private const string ShowResetMessage = "ShowResetMessage";
        private const string ShowSaveMessage = "ShowSaveMessage";

        private ScheduledPlugInWithParametersAttribute _attribute;
        private IParameterDefinitions _parameterDefinitions;
        private Dictionary<string, object> _persistedValues;
        private string _pluginId;

        public DatabaseJobAdapter()
        {
            ParameterControls = new List<Control>();
        }

        private List<Control> ParameterControls { get; set; }

        private string PluginId
        {
            get { return _pluginId ?? (_pluginId = ((Page)Control).Request.QueryString["pluginId"]); }
        }

        private Dictionary<string, object> PersistedValues
        {
            get
            {
                return _persistedValues ??
                       (_persistedValues = typeof(ScheduledJobParameters).GetStore().LoadPersistedValuesFor(PluginId));
            }
        }

        private ScheduledPlugInWithParametersAttribute Attribute
        {
            get
            {
                if (_attribute == null)
                    _attribute =
                        PlugInDescriptor.Load(int.Parse(PluginId))
                            .GetAttribute(typeof(ScheduledPlugInWithParametersAttribute)) as
                            ScheduledPlugInWithParametersAttribute;

                return _attribute;
            }
        }

        private IParameterDefinitions Definitions
        {
            get
            {
                if (_parameterDefinitions == null)
                {
                    _parameterDefinitions =
                        Assembly.Load(Attribute.DefinitionsAssembly).CreateInstance(Attribute.DefinitionsClass) as
                            IParameterDefinitions;
                    if (_parameterDefinitions == null)
                        throw new Exception("Your DefinitionsClass must implement the IParameterDefinitions interface.");
                }
                return _parameterDefinitions;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (string.IsNullOrWhiteSpace(Page.Request.QueryString["GetRunningState"]))
            {
                if (Attribute == null)
                    return;
                Attribute.Validate();
                Initialization();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Page.Request.QueryString["GetRunningState"]))
            {
                string g = Page.Request.QueryString["GetRunningState"];
                Page.Response.Clear();
                Page.Response.ContentType = "application/json";
                Page.Response.Write(GetJSONJobRunningState(new Guid(g)));
                Page.Response.End();
            }
            else
            {
                base.OnLoad(e);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (string.IsNullOrWhiteSpace(Page.Request.QueryString["GetRunningState"]))
            {
                if (Attribute == null)
                    return;
                foreach (var control in ParameterControls)
                    control.DataBind();
            }
        }

        private void Initialization()
        {
            AddStylesheet();
            AddParameterControls();
            DisplaySystemMessage();
        }

        private void DisplaySystemMessage()
        {
            var databaseJob = (DatabaseJob)Control;
            var systemPageBase = databaseJob.Page as SystemPageBase;
            if (systemPageBase == null)
                return;
            if (databaseJob.Request.Cookies["ShowResetMessage"] != null)
            {
                systemPageBase.SystemMessageContainer.Message =
                    "Parameter values were reset to default for this scheduled job.";
                var showResetMessageCookie = databaseJob.Response.Cookies["ShowResetMessage"];
                if (showResetMessageCookie != null)
                    showResetMessageCookie.Expires = DateTime.Now.AddDays(-1.0);
            }
            else
            {
                if (databaseJob.Request.Cookies["ShowSaveMessage"] == null)
                    return;
                systemPageBase.SystemMessageContainer.Message =
                    "Parameter values were successfully saved for this scheduled job.";
                var showSaveMessageCookie = databaseJob.Response.Cookies["ShowSaveMessage"];
                if (showSaveMessageCookie != null)
                    showSaveMessageCookie.Expires = DateTime.Now.AddDays(-1.0);
            }
        }

        private void AddParameterControls()
        {
            var controls = Definitions.GetParameterControls();
            var fieldset = CreateFieldsetFor(controls);

            var generalSettings = Control.FindControlRecursively("GeneralSettings"); // Div with Settings-tab content
            generalSettings.Controls.AddAt(0, fieldset);
        }

        private Control CreateFieldsetFor(IEnumerable<ParameterControlDTO> controls)
        {
            var fieldset = new HtmlGenericControl("fieldset");
            fieldset.Attributes.Add("class", "job-parameters-container");
            fieldset.Controls.Add(new HtmlGenericControl("legend") { InnerText = "Job parameters" });

            foreach (var parameterControl in controls)
            {
                SetPersistedValueFor(parameterControl); // Persisted value from DDS or definition file default if not present.
                ParameterControls.Add(parameterControl.Control);
                fieldset.Controls.Add(CreateRowFor(parameterControl));
            }
            fieldset.Controls.Add(SaveAndResetValuesButtons());
            return fieldset;
        }

        private Control SaveAndResetValuesButtons()
        {
            var container = new HtmlGenericControl("div");
            container.Attributes.Add("class", "save-and-reset-button-container");

            var resetButton = new Button
            {
                Text = "Reset values",
                ToolTip = "Resets all parameters for this scheduled job to their default values.",
                CssClass = "reset-button"
            };
            resetButton.Click += ResetValues_Click;
            var resetButtonOutline = new HtmlGenericControl("span");
            resetButtonOutline.Attributes.Add("class", "epi-cmsButton");
            resetButtonOutline.Controls.Add(resetButton);
            container.Controls.Add(resetButtonOutline);

            var saveButton = new Button
            {
                Text = "Save values",
                ToolTip = "Saves all parameters for this scheduled job.",
                CssClass = "epi-cmsButton-tools save-button"
            };
            saveButton.Click += SaveValues_Click;
            var saveButtonOutline = new HtmlGenericControl("span");
            saveButtonOutline.Attributes.Add("class", "epi-cmsButton");
            saveButtonOutline.Controls.Add(saveButton);
            container.Controls.Add(saveButtonOutline);

            return container;
        }

        private void ResetValues_Click(object sender, EventArgs e)
        {
            var store = typeof(ScheduledJobParameters).GetStore();
            store.RemovePersistedValuesFor(PluginId);
            RefreshWithMessage(ShowResetMessage);
        }

        private void SaveValues_Click(object sender, EventArgs e)
        {
            var store = typeof(ScheduledJobParameters).GetStore();
            store.RemovePersistedValuesFor(PluginId);
            store.PersistValuesFor(PluginId, ParameterControls, c => Definitions.GetValue(c));
            RefreshWithMessage(ShowSaveMessage);
        }

        private void RefreshWithMessage(string message)
        {
            var databaseJob = ((DatabaseJob)Control);
            databaseJob.Response.SetCookie(new HttpCookie(message, "true"));
            databaseJob.Response.Redirect(databaseJob.Request.Url.ToString());
        }

        private static Control CreateRowFor(ParameterControlDTO parameterControlDto)
        {
            var rowContainer = new HtmlGenericControl("div");
            rowContainer.Attributes.Add("class", "parameter-control-container");
            var control = parameterControlDto.Control;
            if (parameterControlDto.ShowLabel)
            {
                var label = new Label
                {
                    AssociatedControlID = parameterControlDto.Id,
                    Text = parameterControlDto.LabelText,
                    ToolTip = parameterControlDto.Description
                };
                rowContainer.Controls.Add(label);
            }
            else
            {
                var noLabelContainer = new HtmlGenericControl("div");
                noLabelContainer.Attributes.Add("title", parameterControlDto.Description);
                noLabelContainer.Attributes.Add("class", "control-without-label");
                noLabelContainer.Controls.Add(parameterControlDto.Control);
                control = noLabelContainer;
            }
            var controlContainer = new HtmlGenericControl("div");
            controlContainer.Attributes.Add("class", "control-container");
            controlContainer.Controls.Add(control);
            rowContainer.Controls.Add(controlContainer);
            return rowContainer;
        }

        private void SetPersistedValueFor(ParameterControlDTO controlDto)
        {
            if (!PersistedValues.ContainsKey(controlDto.Id))
                return;
            Definitions.SetValue(controlDto.Control, PersistedValues[controlDto.Id]);
        }

        private void AddStylesheet()
        {
            var webResourceUrl = Page.ClientScript.GetWebResourceUrl(typeof(ScheduledParameterJobEPiServer75.DatabaseJobAdapter),
                "ScheduledParameterJob.Style.JobParameters.css");
            var htmlLink = new HtmlLink
            {
                Href = webResourceUrl
            };
            htmlLink.Attributes.Add("rel", "stylesheet");
            htmlLink.Attributes.Add("type", "text/css");
            htmlLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(htmlLink);
        }

        private string GetJSONJobRunningState(Guid scheduledJobId)
        {
            var isRunning = false;
            var statusMessage = string.Empty;
            var scheduledJob = ScheduledJob.Load(scheduledJobId);
            if (scheduledJob != null)
            {
                isRunning = scheduledJob.IsRunning;
                statusMessage = scheduledJob.CurrentStatusMessage;
            }

            return JsonConvert.SerializeObject(new
            {
                IsRunning = isRunning,
                CurrentStatusMessage = ClientScriptUtility.ToScriptSafeString(statusMessage)
            });
        }
    }
}
