using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Control;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase, IUserInterfaceCRUD<Permissions>
    {
        private PermissionsControl permissionsControl;
        public PermissionsController(DBContext context)
        {
            permissionsControl = new PermissionsControl(context);
        }
        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await permissionsControl.All();
        }
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Permissions template)
        {
            return await permissionsControl.Create(template);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await permissionsControl.Delete(id);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<string>> Delete(Permissions template)
        {
            return await permissionsControl.Delete(template);
        }

        public bool Exists(string id)
        {
            return permissionsControl.Exists(id);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await permissionsControl.Get(id);
        }

        [HttpGet("GetPermissions/{role}")]
        public async Task<ActionResult<string>> GetByRole(string role)
        {
            return await permissionsControl.GetByRole(role);
        }
        [HttpGet("Settings")]
        public string Settings()
        {
            return permissionsControl.Settings();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Permissions template)
        {
            return await permissionsControl.Update(id, template);
        }
        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Permissions template)
        {
            return await permissionsControl.UpdateAndFetchAll(id, template);
        }

        [HttpPost("CreateModule/{name}")]
        public async Task<ActionResult<string>> CreateModule(string name)
        {
            Console.WriteLine(name);
            Permissions.UpdateDefaultPermissions(name);
            return await permissionsControl.CreateModule(name);
        }

        [HttpPost("RemoveModule/{name}")]
        public async Task<ActionResult<string>> RemoveModule(string name)
        {
            Permissions.RemoveDefaultPermissions(name);
            return await permissionsControl.DeleteModule(name);
        }

        [HttpGet("GetAllModules")]
        public string GetAllModules()
        {
            return  permissionsControl.GetAllModules();
        }
    }
}