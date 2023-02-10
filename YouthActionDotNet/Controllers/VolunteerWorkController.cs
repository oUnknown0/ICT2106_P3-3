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
using YouthActionDotNet.DAL;
using YouthActionDotNet.Control;
using System.Diagnostics;

namespace YouthActionDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerWorkController : ControllerBase, IUserInterfaceCRUD<VolunteerWork>
    {
        private VolunteerWorkControl volunteerWorkControl;
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public VolunteerWorkController(DBContext context)
        {
            volunteerWorkControl = new VolunteerWorkControl(context);
        }

        // GET: api/VolunteerWork
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(VolunteerWork template)
        {
            return await volunteerWorkControl.Create(template);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await volunteerWorkControl.Get(id);
        }

        [HttpGet("GetByVolunteerId/{id}")]
        public async Task<ActionResult<string>> GetByVolunteerId(string id)
        {
            Console.WriteLine("GetByVolunteerId");
            Console.WriteLine(id);
            return await volunteerWorkControl.GetByVolunteerId(id);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await volunteerWorkControl.All();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, VolunteerWork template)
        {
            return await volunteerWorkControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, VolunteerWork template)
        {
            return await volunteerWorkControl.UpdateAndFetchAll(id, template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await volunteerWorkControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(VolunteerWork template)
        {
            return await volunteerWorkControl.Delete(template);
        }

        public bool Exists(string id)
        {
            return volunteerWorkControl.Get(id) != null;
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return volunteerWorkControl.Settings();
        }
    }
}