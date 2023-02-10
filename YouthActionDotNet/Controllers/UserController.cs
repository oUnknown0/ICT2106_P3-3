using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Control;

namespace YouthActionDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IUserInterfaceCRUD<User>
    {
        private UserControl userControl;

        public UserController(DBContext context)
        {
            userControl = new UserControl(context);
        }

        public bool Exists(string id)
        {
            return userControl.Get(id) != null;
        }

        // To login the user
        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<ActionResult<String>> LoginUser(User user)
        {
            return await userControl.LoginUser(user);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(User template)
        {
            return await userControl.Create(template);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await userControl.Get(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, User template)
        {
            return await userControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, User template)
        {
            return await userControl.UpdateAndFetchAll(id, template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await userControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(User template)
        {
            return await userControl.Delete(template);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await userControl.All();
        }
        [HttpGet("Settings")]
        public string Settings()
        {
            return userControl.Settings();
        }
    }
}
