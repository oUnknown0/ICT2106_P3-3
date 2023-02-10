using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL{
    public class FileRepositoryIn: GenericRepositoryIn<Models.File>{

        public FileRepositoryIn(DBContext context): base(context){
            this.context = context;
            this.dbSet = context.Set<Models.File>();            
        }

        public async Task<string> UploadFile(string fileName, string fileUrl){
            Models.File template = new Models.File();
            template.FileName = fileName;
            template.FileUrl = fileUrl;
            var fileExtention = Utils.GetFileExt(template.FileName);
            template.FileMIME = Utils.GetMime(fileExtention);
            template.FileExt = fileExtention;
            await dbSet.AddAsync(template);
            context.SaveChanges();
            return template.FileId;
        }
    }
}