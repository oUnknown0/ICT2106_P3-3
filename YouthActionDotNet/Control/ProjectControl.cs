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
    public class ProjectControl : IUserInterfaceCRUD<Project>
    {
        private GenericRepositoryIn<Project> ProjectRepositoryIn;
        private GenericRepositoryOut<Project> ProjectRepositoryOut;
        private GenericRepositoryIn<ServiceCenter> ServiceCenterRepositoryIn;
        private GenericRepositoryOut<ServiceCenter> ServiceCenterRepositoryOut;

        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public ProjectControl(DBContext context)
        {
            ProjectRepositoryIn = new GenericRepositoryIn<Project>(context);
            ProjectRepositoryOut = new GenericRepositoryOut<Project>(context);
            ServiceCenterRepositoryIn = new GenericRepositoryIn<ServiceCenter>(context);
            ServiceCenterRepositoryOut = new GenericRepositoryOut<ServiceCenter>(context);
        }

        public bool Exists(string id)
        {
            return ProjectRepositoryOut.GetByID(id) != null;
        }

        public async Task<ActionResult<string>> Create(Project template)
        {

            var project = await ProjectRepositoryIn.InsertAsync(template);
            return JsonConvert.SerializeObject(new { success = true, message = "Project Created", data = project }, settings);
        }

        public async Task<ActionResult<string>> Get(string id)
        {
            var project = await ProjectRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Project Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, data = project, message = "Project Successfully Retrieved" });
        }

        public async Task<ActionResult<string>> Update(string id, Project template)
        {
            if (id != template.ProjectId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Id Mismatch" });
            }
            await ProjectRepositoryIn.UpdateAsync(template);
            try
            {
                return JsonConvert.SerializeObject(new { success = true, data = template, message = "Project Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Project template)
        {
            if (id != template.ProjectId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Id Mismatch" });
            }
            await ProjectRepositoryIn.UpdateAsync(template);
            try
            {
                var projects = await ProjectRepositoryOut.GetAllAsync();
                return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Project Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> Delete(string id)
        {
            var project = await ProjectRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Not Found" });
            }
            await ProjectRepositoryIn.DeleteAsync(id);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Project Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Delete(Project template)
        {
            var project = await ProjectRepositoryOut.GetByIDAsync(template.ProjectId);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Project Not Found" });
            }
            await ProjectRepositoryIn.DeleteAsync(template);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Project Successfully Deleted" });
        }

        public async Task<ActionResult<string>> All()
        {
            var projects = await ProjectRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Projects Successfully Retrieved" });
        }

        public string Settings()
        {
            Settings settings = new Settings();
            settings.ColumnSettings = new Dictionary<string, ColumnHeader>();
            settings.FieldSettings = new Dictionary<string, InputType>();

            settings.ColumnSettings.Add("ProjectId", new ColumnHeader { displayHeader = "Project Id" });
            settings.ColumnSettings.Add("ProjectName", new ColumnHeader { displayHeader = "Project Name" });
            settings.ColumnSettings.Add("ProjectDescription", new ColumnHeader { displayHeader = "Project Description" });
            settings.ColumnSettings.Add("ProjectStartDate", new ColumnHeader { displayHeader = "Project Start Date" });
            settings.ColumnSettings.Add("ProjectEndDate", new ColumnHeader { displayHeader = "Project End Date" });
            settings.ColumnSettings.Add("ProjectCompletionDate", new ColumnHeader { displayHeader = "Project Completion Date" });
            settings.ColumnSettings.Add("ProjectStatus", new ColumnHeader { displayHeader = "Project Status" });
            settings.ColumnSettings.Add("ProjectBudget", new ColumnHeader { displayHeader = "Project Budget" });
            settings.ColumnSettings.Add("ServiceCenterId", new ColumnHeader { displayHeader = "Service Center Id" });

            settings.FieldSettings.Add("ProjectId", new InputType { type = "text", displayLabel = "Project Id", editable = false, primaryKey = true });
            settings.FieldSettings.Add("ProjectName", new InputType { type = "text", displayLabel = "Project Name", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectDescription", new InputType { type = "text", displayLabel = "EmaiProject Descriptionl", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectStartDate", new InputType { type = "datetime", displayLabel = "Project Start Date", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectEndDate", new InputType { type = "datetime", displayLabel = "Project End Date", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectCompletionDate", new InputType { type = "datetime", displayLabel = "Project Completion Date", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectStatus", new InputType { type = "text", displayLabel = "Project Status", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ProjectBudget", new InputType { type = "number", displayLabel = "Project Budget", editable = true, primaryKey = false });

            var serviceCenters = ServiceCenterRepositoryOut.GetAll();
            settings.FieldSettings.Add("ServiceCenterId", new DropdownInputType
            {
                type = "dropdown",
                displayLabel = "Service Center",
                editable = true,
                primaryKey = false,
                options = serviceCenters.Select(x => new DropdownOption { value = x.ServiceCenterId, label = x.ServiceCenterName }).ToList()
            });

            return JsonConvert.SerializeObject(new { success = true, data = settings, message = "Settings Successfully Retrieved" });
        }


    }
}
