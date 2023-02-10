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
    public class DonationsController : ControllerBase, IUserInterfaceCRUD<Donations>
    {
        private DonationsControl donationsControl;

        public DonationsController(DBContext context)
        {
            donationsControl = new DonationsControl(context);
        }
        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await donationsControl.All();
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Donations template)
        {
            return await donationsControl.Create(template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await donationsControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(Donations template)
        {
            return await donationsControl.Delete(template);
        }

        public bool Exists(string id)
        {
            return donationsControl.Exists(id);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await donationsControl.Get(id);
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return donationsControl.Settings();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Donations template)
        {
            return await donationsControl.Update(id,template);
        }
        
        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Donations template)
        {
            return await donationsControl.UpdateAndFetchAll(id,template);
        }
    }
}