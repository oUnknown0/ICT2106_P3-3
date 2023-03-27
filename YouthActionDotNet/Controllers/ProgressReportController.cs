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
    public class ProgressReportController : ControllerBase, IUserInterfaceCRUD<ProgressReport>
    {
        private ProgressReportControl progressReportControl;
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

        public ProgressReportController(DBContext context)
        {
            progressReportControl = new ProgressReportControl(context);
        }

        public bool Exists(string id)
        {
            return progressReportControl.Get(id) != null;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(ProgressReport template)
        {
            return await progressReportControl.Create(template);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await progressReportControl.Get(id);
        }
        //---------------------------------------------TO BE UPDATED----------------------------------//
        [HttpGet("RetrieveReport")]
        public async Task<ActionResult<string>> RetrieveReport(string id)
        {
            Console.WriteLine("RetrieveReport");
            return await progressReportControl.RetrieveReport(id);
        }
        [HttpPut("CreateReport")]
        public async Task<ActionResult<string>> CreateReport(ProgressReport template)
        {
            return await progressReportControl.CreateProgressReport(template);
        }
        [HttpPut("UpdateReport")]
        public async Task<ActionResult<string>> UpdateReport(string id, ProgressReport template)
        {
            return await progressReportControl.UpdateProgressReport(id, template);
        }
        [HttpPut("DeleteReport")]
        public async Task<ActionResult<string>> DeleteReport(string id, ProgressReport template)
        {
            return await progressReportControl.DeleteProgressReport(template);
        }
      
        [HttpPut("GenrateMonthlyReport/{id}/{date}")]
        public async Task<ActionResult<string>> GenerateMonthlyReport(string id, ProgressReport template)
        {
            return await progressReportControl.RetrieveReport(id);
        }


        [HttpPut("GenrateMonthlyReportAll/{date}")]
        public async Task<ActionResult<string>> GenerateMonthlyReportAll(string id, ProgressReport template)
        {
            return await progressReportControl.RetrieveReport(id);
        }


        [HttpPut("exportMonthlyProgressReport/{date}")]
        public async Task<ActionResult<string>> exportMonthlyProgressReport(string id, ProgressReport template)
        {
            return await progressReportControl.RetrieveReport(id);
        }
        [HttpPut("exportMonthlyProgressReportAll/{date}")]
        public async Task<ActionResult<string>> Updateprogressreport(string id, ProgressReport template)
        {
            return await progressReportControl.Update(id, template);
        }


        //---------------------------------------------TO BE UPDATED----------------------------------//
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, ProgressReport template)
        {
            return await progressReportControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, ProgressReport template)
        {
            return await progressReportControl.UpdateAndFetchAll(id, template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await progressReportControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(ProgressReport template)
        {
            return await progressReportControl.Delete(template);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await progressReportControl.All();
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return progressReportControl.Settings();
        }
    }
}