using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Control;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase, IUserInterfaceCRUD<Project>
    {
        private ProjectControl projectControl;
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public ProjectController(DBContext context)
        {
            projectControl = new ProjectControl(context);
        }

        public bool Exists(string id)
        {
            return projectControl.Get(id) != null;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Project template)
        {
            return await projectControl.Create(template);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await projectControl.Get(id);
        }
        //---------------------------------------------TO BE UPDATED----------------------------------//
        [HttpGet("GetProjectByTag/{tag}")]
        public async Task<ActionResult<string>> GetProjectByTag(string tag)
        {
            Console.WriteLine("GetProjectByTag");
            Console.WriteLine(tag);
            return await projectControl.GetProjectByTag(tag);
        }
        [HttpGet("GetProjectInProgress")]
        public async Task<ActionResult<string>> GetProjectInProgress()
        {
            Console.WriteLine("GetProjectInProgress");
            return await projectControl.GetProjectInProgress();
        }
        [HttpGet("GetProjectPinned")]
        public async Task<ActionResult<string>> GetProjectPinned()
        {
            Console.WriteLine("GetProjectPinned");
            return await projectControl.GetProjectPinned();
        }
        [HttpGet("GetProjectArchived")]
        public async Task<ActionResult<string>> GetProjectArchived()
        {
            Console.WriteLine("GetProjectArchived");
            return await projectControl.GetProjectArchived();
        }
        
        [HttpPut("UpdateStatusToPinned/{id}")]
        public async Task<ActionResult<string>> UpdateStatusToPinned(string id, Project template)
        {
            return await projectControl.UpdateStatusToPinned(id, template);
        }


        [HttpPut("UpdateStatusToArchive/{id}")]
        public async Task<ActionResult<string>> UpdateStatusToArchive(string id, Project template)
        {
            return await projectControl.UpdateStatusToArchive(id, template);
        }

        //---------------------------------------------TO BE UPDATED----------------------------------//
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Project template)
        {
            return await projectControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Project template)
        {
            return await projectControl.UpdateAndFetchAll(id, template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await projectControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(Project template)
        {
            return await projectControl.Delete(template);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await projectControl.All();
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return projectControl.Settings();
        }
    }
}
