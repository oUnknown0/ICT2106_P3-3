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

    public class LogsController : ControllerBase, IUserInterfaceCRUD<Logs>
    {

        private LogsControl logsControl;


        public LogsController(DBContext context)
        {
            logsControl = new LogsControl(context);
        }

        public Task<ActionResult<string>> All()
        {
            throw new NotImplementedException();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Logs template)
        {
             return await logsControl.Create(template);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(Logs template)
        {
            return await logsControl.Delete(template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await logsControl.Delete(id);
        }

        public bool Exists(string id)
        {
            return logsControl.Get(id) != null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await logsControl.Get(id);
        }

        public string Settings()
        {
            return logsControl.Settings();
        }

        public async Task<ActionResult<string>> Update(string id, Logs template)
        {
            return await logsControl.Update(id, template);
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Logs template)
        {
            return await logsControl.UpdateAndFetchAll(id, template);
        }
    }
}