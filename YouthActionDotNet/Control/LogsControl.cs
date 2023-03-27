using System;
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
    public class LogsControl : IUserInterfaceCRUD<Logs>
    {
        private LogRepositoryIn LogRepositoryIn;
        private LogRepositoryOut LogRepositoryOut;


        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore};


        public LogsControl(DbContext context)
        {
            LogRepositoryIn = new LogRepositoryIn(context);
            LogRepositoryOut = new LogRepositoryOut(context);
        }

        public bool Exists(string id)
        {
            return LogRepositoryOut.GetByID(id) != null;
        }

        public async Task<ActionResult<string>> Create(Logs template)
        {
            var log = await LogRepositoryIn.InsertAsync(template);
            return JsonConvert.SerializeObject(new { sucess = true, message = "Log Created", data = log});
        }

        public async Task<ActionResult<string>> Get(string id)
        {
            var log = await LogRepositoryOut.GetByIDAsync(id);
            if (log == null)
            {
                return JsonConvert.SerializeObject(new {success = false, message = "Log not found"});
            }
            return JsonConvert.SerializeObject(new {success =true, data = log, message ="Log Retrieved"});
        }

        public async Task<ActionResult<string>> Update(string id, Logs template)
        {
            if (id != template.logId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log id not match" });
            }
            await LogRepositoryIn.UpdateAsync(template);
            try
            {
                return JsonConvert.SerializeObject(new { success = true, data = template, message = "Log Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Logs template)
        {
            if (id != template.logId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log Id Mismatch" });
            }
            await LogRepositoryIn.UpdateAsync(template);
            try
            {
                var projects = await LogRepositoryOut.GetAllAsync();
                return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Log Successfully Updated" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> Delete(string id)
        {
            var project = await LogRepositoryOut.GetByIDAsync(id);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log Not Found" });
            }
            await LogRepositoryIn.DeleteAsync(id);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Log Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Delete(Logs template)
        {
            var project = await LogRepositoryOut.GetByIDAsync(template.logId);
            if (project == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Log Not Found" });
            }
            await LogRepositoryIn.DeleteAsync(template);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "Log Successfully Deleted" });
        }

        public async Task<ActionResult<string>> All()
        {
            var projects = await LogRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = projects, message = "Log Successfully Retrieved" });
        }

        public string Settings()
        {
            throw new NotImplementedException();
        }
    }
}