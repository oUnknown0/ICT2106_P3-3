using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class ProgressReportRepositoryIn : GenericRepositoryOut<ProgressReport>
    {   
        public ProgressReportRepositoryIn(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<ProgressReport>();
        }

        public async Task<List<ProgressReport>> retriveReport(string id){
            return await dbSet.Where(p => p.reportId == id).ToListAsync();
        }
        public virtual async Task<bool> createReport(ProgressReport entityToCreate)
        {
            try{
                await dbSet.AddAsync(entityToCreate);
                await context.SaveChangesAsync();
                return true;
            }catch{
                return false;
            }
        }
        public virtual async Task<bool> UpdateReport(ProgressReport entityToUpdate)
        {
            try{
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }catch{
                return false;
            }
        }
        public virtual async Task<bool> deleteReport(ProgressReport entityToDelete)
        {
            try{
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                await context.SaveChangesAsync();
                return true;
            }catch{
                return false;
            }
        }


    }
}