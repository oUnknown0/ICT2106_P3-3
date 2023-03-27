using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{

    public class LogRepositoryIn : GenericRepositoryIn<Logs> {

        public LogRepositoryIn(DbContext context) : base(context)
        {
             this.context = context;
            this.dbSet = context.Set<Logs>();
        }

        public virtual async Task<Logs> MakeLog(string username, string action)
        {
            Logs newLog = new Logs();
            newLog.logUserName = username;
            newLog.logAction = action;

            var log = await dbSet.FirstOrDefaultAsync(l => l.logUserName == newLog.logUserName);
            if (log == null) {
                dbSet.Add(newLog);
                context.SaveChanges();
                return await dbSet.FirstOrDefaultAsync(l => l.logUserName == newLog.logUserName && l.logAction == newLog.logAction);
            }

            return null;
        }

    }
}