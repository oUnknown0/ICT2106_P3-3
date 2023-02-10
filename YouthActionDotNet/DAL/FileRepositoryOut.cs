using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL{
    public class FileRepositoryOut: GenericRepositoryOut<Models.File>{

        public FileRepositoryOut(DBContext context): base(context){
            this.context = context;
            this.dbSet = context.Set<Models.File>();            
        }
        public async Task<Models.File> getFilePath(string fileId){
            var file = await dbSet.FindAsync(fileId);
            if(file == null){
                return null;
            }else{
                file.FileUrl = Path.Combine("uploads", file.FileName);
                return file;
            }
        }
    }
}