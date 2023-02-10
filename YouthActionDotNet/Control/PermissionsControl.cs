using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Controllers;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Control{
    public class PermissionsControl:  IUserInterfaceCRUD<Permissions>
    {
        private PermissionsRepositoryIn PermissionRepositoryIn;
        private PermissionsRepositoryOut PermissionRepositoryOut;

        public PermissionsControl(DBContext context)
        {
            PermissionRepositoryIn = new PermissionsRepositoryIn(context);
            PermissionRepositoryOut = new PermissionsRepositoryOut(context);
        }
        public async Task<ActionResult<string>> All()
        {
            var permissions = await PermissionRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = permissions, message = "Permissions Successfully Retrieved" });
        }
        public async Task<ActionResult<string>> Create(Permissions template)
        {
            Console.WriteLine(template.Role);
            Console.WriteLine(template.Permission);
            var existingRole = await PermissionRepositoryOut.GetByRole(template.Role);
            if (existingRole != null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Role Already Exists" });
            }
            var permission = await PermissionRepositoryIn.InsertAsync(template);
            return JsonConvert.SerializeObject(new { success = true, message = "Permission Created", data = permission });
        }
        public async Task<ActionResult<string>> Delete(string id)
        {
            var permission = PermissionRepositoryOut.GetByID(id);
            if (permission == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
            }
            await PermissionRepositoryIn.DeleteAsync(permission);
            return JsonConvert.SerializeObject(new { success = true, message = "Permission Deleted" });
        }
        public async Task<ActionResult<string>> Delete(Permissions template)
        {
            var permission = PermissionRepositoryOut.GetByID(template.Id);
            if (permission == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
            }
            await PermissionRepositoryIn.DeleteAsync(permission);
            return JsonConvert.SerializeObject(new { success = true, message = "Permission Deleted" });
        }

        public bool Exists(string id)
        {
            if (PermissionRepositoryOut.GetByID(id) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<ActionResult<string>> Get(string id)
        {
            var permission = await PermissionRepositoryOut.GetByIDAsync(id);
            if (permission == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, message = "Permission Successfully Retrieved", data = permission });
        }
        public async Task<ActionResult<string>> GetByRole(string role)
        {
            var permission = await PermissionRepositoryOut.GetByRole(role);
            if (permission == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, message = "Permission Successfully Retrieved", data = permission });
        }

        public async Task<ActionResult<string>> CreateModule(string name){
            var permissions = await PermissionRepositoryOut.GetAllAsync();

            try{
                foreach(var permission in permissions){
                    List<Permission> permissionList = JsonConvert.DeserializeObject<List<Permission>>(permission.Permission);
                    permissionList.Add(new Permission(name));
                    permission.Permission = JsonConvert.SerializeObject(permissionList);
                    await PermissionRepositoryIn.UpdateAsync(permission);
                }
                return JsonConvert.SerializeObject(new { success = true, message = "Module Created" });
            }catch(Exception e){
                return JsonConvert.SerializeObject(new { success = false, message = e.Message});
            
            }
        }

        public string GetAllModules()
        {
            return JsonConvert.SerializeObject(new { success = true, message = "Modules Retrieved", data = Permissions.GetAllModules() });
        }

        public async Task<ActionResult<string>> DeleteModule(string name){
            var permissions = await PermissionRepositoryOut.GetAllAsync();

            try{
                foreach(var permission in permissions){
                    List<Permission> permissionList = JsonConvert.DeserializeObject<List<Permission>>(permission.Permission);
                    permissionList.RemoveAll(x => x.Module == name);
                    permission.Permission = JsonConvert.SerializeObject(permissionList);
                    await PermissionRepositoryIn.UpdateAsync(permission);
                }
                return JsonConvert.SerializeObject(new { success = true, message = "Module Deleted" });
            }catch(Exception e){
                return JsonConvert.SerializeObject(new { success = false, message = e.Message});
            
            }
        }

        public string Settings()
        {
            Settings settings = new Settings();
            settings.ColumnSettings = new Dictionary<string, ColumnHeader>();
            settings.FieldSettings = new Dictionary<string, InputType>();

            settings.ColumnSettings.Add("Id", new ColumnHeader{ displayHeader = "Permission ID"});
            settings.ColumnSettings.Add("Role", new ColumnHeader{ displayHeader = "Role"});
            
            settings.FieldSettings.Add("Id", new InputType{ type = "text", displayLabel="Permission ID", editable = false, primaryKey = true});
            settings.FieldSettings.Add("Role", new InputType{ type = "text", displayLabel="Role", editable = true, primaryKey = false});
        
            return JsonConvert.SerializeObject(new { success = true, message = "Settings Retrieved", data = settings });
        }
        public async Task<ActionResult<string>> Update(string id, Permissions template)
        {
            if(id != template.Id){
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Id Mismatch" });
            }
            await PermissionRepositoryIn.UpdateAsync(template);
            try{
                return JsonConvert.SerializeObject(new { success = true, message = "Permission Updated", data = template });
            }catch(DbUpdateConcurrencyException){
                if(!Exists(id)){
                    return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
                }else{
                    throw;
                }
            }
        }
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Permissions template)
        {
            if(id != template.Id){
                return JsonConvert.SerializeObject(new { success = false, message = "Permission Id Mismatch" });
            }
            await PermissionRepositoryIn.UpdateAsync(template);
            try{
                var permissions = await PermissionRepositoryOut.GetAllAsync();
                return JsonConvert.SerializeObject(new { success = true, message = "Permission Updated", data = permissions });
            }catch(DbUpdateConcurrencyException){
                if(!Exists(id)){
                    return JsonConvert.SerializeObject(new { success = false, message = "Permission Not Found" });
                }else{
                    throw;
                }
            }
        }
    }
}