using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class IReportRepoIn : GenericRepositoryOut<Project>
    {   
        public IReportRepoIn(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Project>();
        }

        public async Task<List<Project>> retriveReport(string tag){
            return await dbSet.Where(p => p.ProjectName == tag).ToListAsync();
        }
                public virtual bool createReport(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
                public virtual bool UpdateReport(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
                public virtual bool deleteReport(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}