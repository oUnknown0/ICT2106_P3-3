
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace YouthActionDotNet.Controllers
{
    internal interface IUserInterfaceCRUD<T> where T : class
    {
        [HttpPost("Create")]
        Task<ActionResult<string>> Create(T template);
        [HttpGet("{id}")]
        Task<ActionResult<string>> Get(string id);
        [HttpPut("{id}")]
        Task<ActionResult<string>> Update(string id, T template);
        [HttpPut("UpdateAndFetch/{id}")]
        Task<ActionResult<string>> UpdateAndFetchAll(string id, T template);
        [HttpDelete("{id}")]
        Task<ActionResult<string>> Delete(string id);
        [HttpDelete("Delete")]
        Task<ActionResult<string>> Delete(T template);
        [HttpPost("All")]
        Task<ActionResult<string>> All();
        [HttpGet("Settings")]
        string Settings();
        bool Exists(string id);
    }
}