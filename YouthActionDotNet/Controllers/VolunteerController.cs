using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Control;

namespace YouthActionDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase, IUserInterfaceCRUD<Volunteer>
    {
        private VolunteerControl volunteerControl;
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public VolunteerController(DBContext context)
        {
            volunteerControl = new VolunteerControl(context);
        }

        public bool Exists(string id)
        {
            return volunteerControl.Get(id) != null;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Volunteer template)
        {
            return await volunteerControl.Create(template);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<String>> Register(Volunteer template)
        {
            
            template.Role = "Volunteer";
            template.ApprovalStatus = "Pending";
            return await volunteerControl.Register(template);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await volunteerControl.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Volunteer template)
        {
            return await volunteerControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Volunteer template)
        {
            return await volunteerControl.UpdateAndFetchAll(id, template);
        }

        [HttpPut("Approve/{id}")]
        public async Task<ActionResult<string>> Approve(string id, Employee template)
        {
            return await volunteerControl.Approve(id, template);
        }

        [HttpPut("Revoke/{id}")]
        public async Task<ActionResult<string>> RevokeApproval(string id)
        {
            return await volunteerControl.RevokeApproval(id);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await volunteerControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(Volunteer template)
        {
            return await volunteerControl.Delete(template);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await volunteerControl.All();
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return volunteerControl.Settings();
        }

    }
}
