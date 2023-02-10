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

namespace YouthActionDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase, IUserInterfaceCRUD<Expense>
    {
        private ExpenseControl expenseControl;
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public ExpenseController(DBContext context)
        {
            expenseControl = new ExpenseControl(context);
        }

        [HttpGet("All")]
        public async Task<ActionResult<string>> All()
        {
            return await expenseControl.All();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await expenseControl.Get(id);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> Create(Expense template)
        {
            return await expenseControl.Create(template);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Update(string id, Expense template)
        {
            return await expenseControl.Update(id, template);
        }

        [HttpPut("UpdateAndFetch/{id}")]
        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, Expense template)
        {
            return await expenseControl.UpdateAndFetchAll(id, template);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            return await expenseControl.Delete(id);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<String>> Delete(Expense template)
        {
            return await expenseControl.Delete(template);
        }

        public bool Exists(string id)
        {
            return expenseControl.Get(id) != null;
        }

        [HttpGet("Settings")]
        public string Settings()
        {
            return expenseControl.Settings();
        }
    }
}
