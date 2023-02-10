using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ServiceCenterController : ControllerBase, IUserInterfaceCRUD<ServiceCenter>
    {
        private ServiceCenterControl serviceCenterControl;
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        public ServiceCenterController(DBContext context)
        {
            serviceCenterControl = new ServiceCenterControl(context);
        }

        public bool Exists(string id)
        {
            return serviceCenterControl.Get(id) != null;
        }
        // GET: api/ServiceCenter
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(ServiceCenter template)
        {
            return await serviceCenterControl.Create(template);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await serviceCenterControl.Get(id);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await serviceCenterControl.All();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, ServiceCenter template)
        {
            return await serviceCenterControl.Update(id, template);
        }
        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, ServiceCenter template)
        {
            return await serviceCenterControl.UpdateAndFetchAll(id, template);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await serviceCenterControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(ServiceCenter template)
        {
            return await serviceCenterControl.Delete(template);
        }
        [HttpGet("Settings")]
        public string Settings()
        {
            return serviceCenterControl.Settings();
        }

    }
}
