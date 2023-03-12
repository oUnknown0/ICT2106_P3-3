using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL {

    public class LogRepositoryIn : GenericRepositoryIn<Project> {

        public LogRepositoryIn(DBContext context) : base(context) {
            // this.context = context;
            // this.dbSet = context.Set<Logs>();
        }

        //to be implemented
        // public async Task<Logs> insertLog(string user, string action) {
        //     Logs newLog = new Logs();
        //     newLog.logUserName  = user;
        //     newLog.logAction = action;
        //     //await.dbSet.Add(newLog);
        //     //context.SaveChanges();
        //    return newLog.LogId;
        // }
    }
}