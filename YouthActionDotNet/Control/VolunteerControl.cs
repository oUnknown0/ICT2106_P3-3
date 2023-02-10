using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;
using Newtonsoft.Json;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Controllers;

namespace YouthActionDotNet.Control
{
    public class VolunteerControl : IUserInterfaceCRUD<Volunteer>
    {
        private GenericRepositoryIn<Volunteer> VolunteerRepositoryIn;
        private GenericRepositoryOut<Volunteer> VolunteerRepositoryOut;
        private GenericRepositoryIn<Employee> EmployeeRepositoryIn;
        private GenericRepositoryOut<Employee> EmployeeRepositoryOut;
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public VolunteerControl(DBContext context)
        {
            VolunteerRepositoryIn = new GenericRepositoryIn<Volunteer>(context);
            VolunteerRepositoryOut = new GenericRepositoryOut<Volunteer>(context);
            EmployeeRepositoryIn = new GenericRepositoryIn<Employee>(context);
            EmployeeRepositoryOut = new GenericRepositoryOut<Employee>(context);
        }

        public bool Exists(string id)
        {
            return VolunteerRepositoryOut.GetByID(id) != null;
        }

        public async Task<ActionResult<string>> Create(Volunteer template)
        {
            var volunteers = await VolunteerRepositoryOut.GetAllAsync();
            var existingVolunteer = volunteers.FirstOrDefault(d => d.UserId == template.UserId);
            if (existingVolunteer != null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Volunteer Already Exists" });
            }
            template.Password = Utils.hashpassword(template.Password);
            await VolunteerRepositoryIn.InsertAsync(template);
            var createdVolunteer = await VolunteerRepositoryOut.GetByIDAsync(template.UserId);
            return JsonConvert.SerializeObject(new { success = true, data = template, message = "Volunteer Successfully Created" });
        }

        public async Task<ActionResult<string>> Register(Volunteer template){
            var volunteers = await VolunteerRepositoryOut.GetAllAsync();
            var existingVolunteer = volunteers.FirstOrDefault(d => 
                d.username == template.username
            );
            if(existingVolunteer != null){
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "User name already exists"});
            }

            existingVolunteer = volunteers.FirstOrDefault(d =>
                d.Email == template.Email
            );
            if(existingVolunteer != null){
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Email already exists"});
            }

            template.Password = Utils.hashpassword(template.Password);
            await VolunteerRepositoryIn.InsertAsync(template);
            var createdVolunteer = await VolunteerRepositoryOut.GetByIDAsync(template.UserId);
            return JsonConvert.SerializeObject(new { success = true, data = createdVolunteer, message = "Volunteer successfully created"});
        }

        public async Task<ActionResult<string>> Get(string id)
        {
            var volunteer = await VolunteerRepositoryOut.GetByIDAsync(id);
            if (volunteer == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, data = volunteer, message = "Volunteer Successfully Retrieved" });
        }

        public async Task<ActionResult<string>> Update(string id, Volunteer template)
        {
            if (id != template.UserId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Id Mismatch" });
            }
            await VolunteerRepositoryIn.UpdateAsync(template);
            try
            {
                return await Get(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Volunteer template)
        {
            if (id != template.UserId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Id Mismatch" });
            }
            await VolunteerRepositoryIn.UpdateAsync(template);
            try
            {
                return await All();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }
        
        public async Task<ActionResult<string>> Approve(string id, Employee template){
            var volunteer = await VolunteerRepositoryOut.GetByIDAsync(id);
            if(volunteer == null){
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
            }
            volunteer.ApprovalStatus = "Approved";
            volunteer.ApprovedBy = template.UserId;
            await VolunteerRepositoryIn.UpdateAsync(volunteer);
            return JsonConvert.SerializeObject(new { success = true, data = volunteer, message = "Volunteer Successfully Approved" });
        }

        public async Task<ActionResult<string>> RevokeApproval(string id){
            var volunteer = await VolunteerRepositoryOut.GetByIDAsync(id);
            if(volunteer == null){
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
            }
            volunteer.ApprovalStatus = "Pending";
            volunteer.ApprovedBy = null;
            await VolunteerRepositoryIn.UpdateAsync(volunteer);
            return JsonConvert.SerializeObject(new { success = true, data = volunteer, message = "Volunteer Approval Successfully Revoked" });
        }

        public async Task<ActionResult<string>> Delete(string id)
        {
            var volunteer = await VolunteerRepositoryOut.GetByIDAsync(id);
            if (volunteer == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
            }
            await VolunteerRepositoryIn.DeleteAsync(volunteer);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Volunteer Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Delete(Volunteer template)
        {
            var volunteer = await VolunteerRepositoryOut.GetByIDAsync(template.UserId);
            if (volunteer == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
            }
            await VolunteerRepositoryIn.DeleteAsync(volunteer);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Volunteer Successfully Deleted" });
        }

        public async Task<ActionResult<string>> All()
        {
            var volunteers = await VolunteerRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = volunteers, message = "Volunteers Successfully Retrieved" }, settings);
        }

        public string Settings()
        {
            Settings settings = new UserSettings();
            settings.ColumnSettings.Add("UserId", new ColumnHeader { displayHeader = "User Id" });
            settings.ColumnSettings.Add("username", new ColumnHeader { displayHeader = "Username" });
            settings.ColumnSettings.Add("Email", new ColumnHeader { displayHeader = "Email" });
            settings.ColumnSettings.Add("Password", new ColumnHeader { displayHeader = "Password" });
            settings.ColumnSettings.Add("Role", new ColumnHeader { displayHeader = "Role" });

            settings.FieldSettings.Add("VolunteerNationalId", new InputType { type = "text", displayLabel = "Volunteer National Id", editable = true, primaryKey = false });
            settings.FieldSettings.Add("VolunteerDateJoined", new InputType { type = "datetime", displayLabel = "Volunteer Date Joined", editable = true, primaryKey = false });
            settings.FieldSettings.Add("VolunteerDateBirth", new InputType { type = "datetime", displayLabel = "Volunteer Date Birth", editable = true, primaryKey = false });
            settings.FieldSettings.Add("Qualifications", new InputType { type = "text", displayLabel = "Qualifications", editable = true, primaryKey = false });
            settings.FieldSettings.Add("CriminalHistory", new DropdownInputType
            {
                type = "dropdown",
                displayLabel = "Criminal History",
                editable = true,
                primaryKey = false,
                options = new List<DropdownOption> {
                new DropdownOption { value = "Yes", label = "Yes" },
                new DropdownOption { value = "No", label = "No" },
            }
            });
            settings.FieldSettings.Add("CriminalHistoryDesc", new InputType { type = "text", displayLabel = "Criminal History Description", editable = true, primaryKey = false });
            settings.FieldSettings.Add("ApprovalStatus", new DropdownInputType
            {
                type = "dropdown",
                displayLabel = "Volunteer Status",
                editable = false,
                primaryKey = false,
                options = new List<DropdownOption> {
                new DropdownOption { value = "Approved", label = "Approved" },
                new DropdownOption { value = "Pending", label = "Pending" },
            }
            });

            var allEmployees = EmployeeRepositoryOut.GetAll(filter: e => e.Role == "Employee");
            settings.FieldSettings.Add("ApprovedBy", new DropdownInputType
            {
                type = "dropdown",
                displayLabel = "Approved By",
                editable = false,
                primaryKey = false,
                allowEmpty = true,
                options = allEmployees.Select(
                    e => new DropdownOption
                    {
                        value = e.UserId,
                        label = e.username
                    }).ToList()
            });



            return JsonConvert.SerializeObject(new { success = true, data = settings, message = "Settings Successfully Retrieved" });
        }
    }
}
