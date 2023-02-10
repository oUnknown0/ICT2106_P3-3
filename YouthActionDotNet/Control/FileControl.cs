using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.Control{
    public class FileControl{
        
        private FileRepositoryIn FileRepositoryIn;
        private FileRepositoryOut FileRepositoryOut;
        public FileControl(DBContext context){
            FileRepositoryIn = new FileRepositoryIn(context);
            FileRepositoryOut = new FileRepositoryOut(context);
        }
        public async Task<string> UploadFile(string fileName, string filePath){
            try{
                var fileId = await FileRepositoryIn.UploadFile(fileName,filePath);
                return JsonConvert.SerializeObject(new { success = true, message = "File uploaded successfully", data = fileId });
                
            }catch(Exception e){
                System.Diagnostics.Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return JsonConvert.SerializeObject(new { success = false, message = e.Message });
            }
        }
        public async Task<string> GetFile(string id){
            try{
                var file = await FileRepositoryOut.getFilePath(id);
                if(file == null){
                    return JsonConvert.SerializeObject(new { success = false, message = "File Does not exist" });
                
                }else{
                    return JsonConvert.SerializeObject(new { success = true, message = "File path retrieved successfully", data = file });
            
                }

                    }catch(Exception e){
                return JsonConvert.SerializeObject(new { success = false, message = e.Message });
            }
        }
    }
}