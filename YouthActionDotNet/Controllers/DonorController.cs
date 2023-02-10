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
    public class DonorController : ControllerBase,IUserInterfaceCRUD<Donor>
    {
        private DonorControl donorControl;
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };


        public DonorController(DBContext context)
        {
            donorControl = new DonorControl(context);
        }

        public bool Exists(string id)
        {
            return donorControl.Get(id) != null;
        }
        
        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Donor donor)
        {
            return await donorControl.Create(donor);    
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {   
            return await donorControl.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Donor donor)
        {
            return await donorControl.Update(id,donor);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Donor template)
        {
            return await donorControl.UpdateAndFetchAll(id,template);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await donorControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(Donor donor)
        {
            return await donorControl.Delete(donor);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await donorControl.All();                        
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return donorControl.Settings();
        }
    }
}

