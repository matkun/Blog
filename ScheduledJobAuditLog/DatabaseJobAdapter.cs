using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EPiServer.Core;
using EPiServer.Data.Dynamic;
using EPiServer.DataAbstraction;
using EPiServer.UI.Admin;
using EPiServer.UI.WebControls;
using EPiServer.Web.WebControls;

//<adapter controlType="EPiServer.UI.Admin.DatabaseJob" adapterType="EPiServer.CodeSample.ScheduledJobAuditLog.DatabaseJobAdapter" />

[assembly: System.Web.UI.WebResource("EPiServer.CodeSample.ScheduledJobAuditLog.JobAudit.css", "text/css")]
namespace EPiServer.CodeSample.ScheduledJobAuditLog
{
    public class DatabaseJobAdapter : PageAdapter
    {
        // ID of the scheduled job
        private string _pluginId;
        private string PluginId { get { return _pluginId ?? (_pluginId = ((DatabaseJob)Control).Request.QueryString["pluginId"]); } }
        
        // Div with Settings-tab content
        private Panel _gsc;
        public Panel GeneralSettingsControl { get { return _gsc ?? (_gsc = Control.FindControlRecursively("GeneralSettings") as Panel); } }

        private string Reason
        {
            get
            {
                var box = GeneralSettingsControl.FindControlRecursively("ReasonTextBox") as TextBox;
                return box != null ?
                    HttpUtility.HtmlEncode(box.Text) :
                    "Unable to locate reason text box.";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            AddStylesheet();
            AddAuditLogComponents();
            AttachClickEvents();
        }

        private void AttachClickEvents()
        {
            var saveButton = GeneralSettingsControl.FindControlRecursively("saveChanges") as ToolButton;
            saveButton.Click += Save_Click;
            var runButton = GeneralSettingsControl.FindControlRecursively("startNowButton") as ToolButton;
            runButton.Click += Run_Click;
            var stopButton = GeneralSettingsControl.FindControlRecursively("stopRunningJobButton") as ToolButton;
            stopButton.Click += Stop_Click;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            AddLogEntry(JobAction.SavedSettings, SettingsLog());
        }

        private void Run_Click(object sender, EventArgs e)
        {
            AddLogEntry(JobAction.StartedManually);
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            AddLogEntry(JobAction.StoppedManually);
        }

        private void AddLogEntry(JobAction action, string messageAmendment = null)
        {
            var entry = new AuditLogEntry
            {
                PluginId = PluginId,
                Action = action,
                Message = messageAmendment == null ?
                                Reason :
                                string.Format("{0}<br />{1}", Reason, messageAmendment)
            };
            var store = typeof(AuditLogEntry).GetStore();
            store.Save(entry);
        }

        private string SettingsLog()
        {
            var isActive = GeneralSettingsControl.FindControlRecursively("isActiveInput") as CheckBox;
            var frequency = GeneralSettingsControl.FindControlRecursively("frequencyInput") as TextBox;
            var recurrence = GeneralSettingsControl.FindControlRecursively("recurrenceInput") as DropDownList;
            var nextExecution = GeneralSettingsControl.FindControlRecursively("nextExecutionInput") as InputDate;
            
            const string format = "'{0}': '{1}'";
            var isActiveText = string.Format(format, LanguageManager.Instance.Translate("/admin/databasejob/activecaption"), isActive.Checked);
            var intervalValue = string.Format("{0} {1}", frequency.Text, (ScheduledIntervalType)int.Parse(recurrence.SelectedValue));
            var intervalText = string.Format(format, LanguageManager.Instance.Translate("/admin/databasejob/schedulecaption"), intervalValue);
            var nextExecutionText = string.Format(format, LanguageManager.Instance.Translate("/admin/databasejob/nextexecution"), FormatDateTime(nextExecution.Value));
            
            var settings = new[]
                {
                    "New settings in database:",
                    isActiveText,
                    intervalText,
                    nextExecutionText
                };
            return string.Join("<br />", settings);
        }

        private void AddAuditLogComponents()
        {
            var store = typeof(AuditLogEntry).GetStore();
            var entries = store.Items<AuditLogEntry>()
                               .Where(log => log.PluginId == PluginId)
                               .OrderByDescending(log => log.Timestamp)
                               .Take(10);

            var container = new HtmlGenericControl("div");
            container.Attributes.Add("class", "job-audit-log-container");
            container.Controls.Add(InteractiveControls());
            container.Controls.Add(LogTableFor(entries));

            GeneralSettingsControl.Controls.Add(container);
        }
        
        private HtmlGenericControl InteractiveControls()
        {
            var container = new HtmlGenericControl("div");

            var reasonTextBox = new TextBox
                {
                    ID = "ReasonTextBox",
                    ClientIDMode = ClientIDMode.Static
                };
            var reasonLabel = new Label
                {
                    Text = "Reason (Explain yourself):",
                    AssociatedControlID = reasonTextBox.ID
                };
            container.Controls.Add(reasonLabel);
            container.Controls.Add(reasonTextBox);

            const string msg =
                "If you don't have a reason for doing that, then you probably shouldn't. Reason is required when changing settings.";
            var reasonValidator = new RequiredFieldValidator
                {
                    ControlToValidate = reasonTextBox.ID,
                    Text = "* ",
                    ErrorMessage = msg,
                    EnableClientScript = true
                };
            container.Controls.Add(reasonValidator);

            var downloadButton = new ToolButton
                {
                    ID = "downloadButton",
                    Text = "Download audit log",
                    ToolTip = "Export the audit log and open it in for instance Excel.",
                    SkinID = "Save",
                    CausesValidation = false
                };
            downloadButton.Click += Download_Click;
            container.Controls.Add(downloadButton);

            return container;
        }

        private void Download_Click(object sender, EventArgs e)
        {
            var store = typeof(AuditLogEntry).GetStore();
            var entries = store.Items<AuditLogEntry>()
                               .Where(log => log.PluginId == PluginId)
                               .OrderByDescending(log => log.Timestamp);
            var content = new ExcelFeedGenerator().ExcelFeedFor(entries);

            var context = HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", string.Format("attachment;filename=AuditLogExport_PluginId_{0}.xls", PluginId));
            context.Response.ContentType = "application/ms-excel";
            context.Response.Charset = "";
            context.Response.Write(content);
            context.Response.End();
        }

        private static Control LogTableFor(IEnumerable<AuditLogEntry> entries)
        {
            var logTable = new HtmlGenericControl("table");
            logTable.Attributes.Add("class", "epi-default");
            logTable.Controls.Add(Headers());
            var tbody = new HtmlGenericControl("tbody");
            foreach (var entry in entries)
            {
                tbody.Controls.Add(RowFor(entry));
            }
            logTable.Controls.Add(tbody);
            return logTable;
        }

        private static Control Headers()
        {
            var thead = new HtmlGenericControl("thead");
            thead.Controls.Add(Header("Timestamp"));
            thead.Controls.Add(Header("Action"));
            thead.Controls.Add(Header("Username"));
            thead.Controls.Add(Header("Email"));
            thead.Controls.Add(Header("Reason / Message"));
            return thead;
        }

        private static Control Header(string header)
        {
            var th = new HtmlGenericControl("th");
            th.Attributes.Add("class", "epitableheading");
            th.Controls.Add(new Literal{Text = header});
            return th;
        }

        private static Control RowFor(AuditLogEntry entry)
        {
            var row = new HtmlGenericControl("tr");
            row.Controls.Add(CellContaining(FormatDateTime(entry.Timestamp)));
            row.Controls.Add(CellContaining(entry.Action.ToDescriptionString()));
            row.Controls.Add(CellContaining(entry.Username));
            row.Controls.Add(CellContaining(entry.Email));
            row.Controls.Add(CellContaining(entry.Message));
            return row;
        }

        private static Control CellContaining(string content)
        {
            var cell = new HtmlGenericControl("td");
            cell.Controls.Add(new Literal {Text = content});
            return cell;
        }

        private static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void AddStylesheet()
        {
            var cssPath = Page
                .ClientScript
                .GetWebResourceUrl(typeof(DatabaseJobAdapter), "EPiServer.CodeSample.ScheduledJobAuditLog.JobAudit.css");
            var cssLink = new HtmlLink { Href = cssPath };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("media", "screen");
            Page.Header.Controls.Add(cssLink);
        }
    }
}
