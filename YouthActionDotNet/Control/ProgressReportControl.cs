using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Controllers;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Control
{
    public class ProgressReportControl : IUserInterfaceCRUD<ProgressReport>
    {
        //-------------------------------------------------TO BE UPDATED------------------------------------------------//
        private ProgressReportRepositoryIn ProgressReportRepositoryIn;
        private ProgressReportRepositoryOut ProgressReportRepositoryOut;
        //-------------------------------------------------TO BE UPDATED------------------------------------------------//
        private GenericRepositoryIn<ProgressReport> ProgressRepositoryIn;
        private GenericRepositoryOut<ProgressReport> ProgressRepositoryOut;
        private GenericRepositoryIn<ServiceCenter> ServiceCenterRepositoryIn;
        private GenericRepositoryOut<ServiceCenter> ServiceCenterRepositoryOut;

        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public ProgressReportControl(DBContext context)
        {
            //-------------------------------------------------TO BE UPDATED------------------------------------------------//
            ProgressReportRepositoryIn = new ProgressReportRepositoryIn(context);
            ProgressReportRepositoryOut = new ProgressReportRepositoryOut(context);
            //-------------------------------------------------TO BE UPDATED------------------------------------------------//
            ProgressRepositoryIn = new GenericRepositoryIn<ProgressReport>(context);
            ProgressRepositoryOut = new GenericRepositoryOut<ProgressReport>(context);
        }

        public bool Exists(string id)
        {
            return ProgressRepositoryOut.GetByID(id) != null;
        }

        public async Task<ActionResult<string>> RetrieveReport(string id)
        {
            var report = await ProgressReportRepositoryOut.retriveReport(id);
            if (report == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Tag Not Found" }, settings);
            }
            return JsonConvert.SerializeObject(new { success = true, data = report, message = "Tag Successfully Retrieved" }, settings);
        }
        public async Task<ActionResult<string>> Update(string id, ProgressReport template)
        {
            if (id != template.reportId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Id Mismatch" });
            }
            await ProgressRepositoryIn.UpdateAsync(template);
            try
            {
                return JsonConvert.SerializeObject(new { success = true, data = template, message = "Report Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> CreateProgressReport(ProgressReport template)
        {
            var report = await ProgressReportRepositoryIn.createReport(template);
            return JsonConvert.SerializeObject(new { sucess = true, message = "Report Created", data = report});
        }

        public async Task<ActionResult<string>> UpdateProgressReport(string id, ProgressReport template)
        {
            if (id != template.reportId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Id Mismatch" });
            }
            await ProgressReportRepositoryIn.UpdateReport(template);
            try
            {
                return JsonConvert.SerializeObject(new { success = true, data = template, message = "Report Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<ActionResult<string>> DeleteProgressReport(ProgressReport id)
        {
            var project = await ProgressRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
            }
            await ProgressReportRepositoryIn.deleteReport(id);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Report Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Create(ProgressReport template)
        {

            var project = await ProgressRepositoryIn.InsertAsync(template);
            return JsonConvert.SerializeObject(new { success = true, message = "Report Created", data = project }, settings);
        }

        public async Task<ActionResult<string>> Get(string id)
        {
            var project = await ProgressReportRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Report Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, data = project, message = "Report Successfully Retrieved" });
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, ProgressReport template)
        {
            if (id != template.reportId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Id Mismatch" });
            }
            await ProgressRepositoryIn.UpdateAsync(template);
            try
            {
                var projects = await ProgressRepositoryOut.GetAllAsync();
                return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Report Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> Delete(string id)
        {
            var project = await ProgressRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
            }
            await ProgressRepositoryIn.DeleteAsync(id);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Report Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Delete(ProgressReport template)
        {
            var project = await ProgressRepositoryOut.GetByIDAsync(template.reportId);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Report Not Found" });
            }
            await ProgressRepositoryIn.DeleteAsync(template);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Report Successfully Deleted" });
        }

        public async Task<ActionResult<string>> All()
        {
            var projects = await ProgressRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Report Successfully Retrieved" });
        }

        public string Settings()
        {
            Settings settings = new Settings();
            settings.ColumnSettings = new Dictionary<string, ColumnHeader>();
            settings.FieldSettings = new Dictionary<string, InputType>();

            settings.ColumnSettings.Add("reportId", new ColumnHeader { displayHeader = "Report Id" });
            settings.ColumnSettings.Add("reportName", new ColumnHeader { displayHeader = "Report Name" });
            settings.ColumnSettings.Add("projectId", new ColumnHeader { displayHeader = "Project Id" });
            settings.ColumnSettings.Add("reportDate", new ColumnHeader { displayHeader = "Report Date" });

            settings.FieldSettings.Add("reportId", new InputType { type = "text", displayLabel = "Report Id", editable = false, primaryKey = true });
            settings.FieldSettings.Add("reportName", new InputType { type = "text", displayLabel = "Report Name", editable = true, primaryKey = false });
            settings.FieldSettings.Add("projectId", new InputType { type = "text", displayLabel = "Project Id", editable = true, primaryKey = false });
            settings.FieldSettings.Add("reportDate", new InputType { type = "datetime", displayLabel = "Report Date", editable = true, primaryKey = false });

            return JsonConvert.SerializeObject(new { success = true, data = settings, message = "Settings Successfully Retrieved" });
        }


    }
}