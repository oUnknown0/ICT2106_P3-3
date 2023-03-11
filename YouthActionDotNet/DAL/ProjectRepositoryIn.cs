using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{

    public class ProjectRepositoryIn : GenericRepositoryIn<Project>
    {
        public ProjectRepositoryIn(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Project>();
        }

        public virtual bool UpdateStatus(Project entityToUpdate)
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
        public virtual async Task<bool> UpdateStatusToPinned(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                entityToUpdate.ProjectStatus = "Pinned";
                context.Entry(entityToUpdate).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
         public virtual async Task<bool> UpdateStatusToArchive(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                entityToUpdate.ProjectStatus = "Archived";
                context.Entry(entityToUpdate).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
   public virtual async Task<bool> UpdateStatusToInProgress(Project entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                entityToUpdate.ProjectStatus = "In progress";
                context.Entry(entityToUpdate).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}