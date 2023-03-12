using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL {

    public class LogRepositoryOut: GenericRepositoryOut<Logs> {

        public LogRepositoryOut(DBContext context): base(context) {
            this.context = context;
            this.dbSet = context.Set<Logs>();
        }

        // public Task<Logs> getLogsByUser(string user) {
        //     return dbSet.FirstOrDefaultAsync(p => p.logUserName == user);
        // }

        // public List<Logs> getAllLogs() {
        //     return dbSet.Where(p => )
        // }

    }
}