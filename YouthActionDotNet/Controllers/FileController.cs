using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YouthActionDotNet.Control;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController: ControllerBase{
        
        private FileControl fileControl;
        public FileController(DBContext context){
            fileControl = new FileControl(context);
        }

        [HttpPost("Upload")]
        public async Task<string> UploadFile(){
            try{
                var form = await Request.ReadFormAsync();
                var file = form.Files.FirstOrDefault();

                if(file != null)
                {
                    var filePath = Path.Combine("uploads", file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create)){
                        await file.OpenReadStream().CopyToAsync(stream);
                    }
                    
                    return await fileControl.UploadFile(file.FileName, filePath);
                }
                return JsonConvert.SerializeObject(new { success = false, message = "No file found" });
            }catch(Exception e){
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return JsonConvert.SerializeObject(new { success = false, message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<string> GetFile(string id){
            try{
                var file = await fileControl.GetFile(id);
                return file;

            }catch(Exception e){
                return JsonConvert.SerializeObject(new { success = false, message = e.Message });
            }
        }
    }
}