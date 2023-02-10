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
    public class GenericRepositoryIn<TEntity> where TEntity: class{
        internal DbSet<TEntity> dbSet;
        internal DbContext context;

        public GenericRepositoryIn(DbContext context){
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public virtual bool Insert(TEntity entity)
        {
            try{
                dbSet.Add(entity);
                return true;
            }catch{
                return false;
            }
        }

        public virtual bool Update(TEntity entityToUpdate)
        {
            try{
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                return true;
            }catch{
                return false;
            }
        }

        public virtual bool Delete(object id)
        {
            try{
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
                return true;
            }catch{
                return false;
            }
        }

        public virtual bool Delete(TEntity entityToDelete)
        {
            try{
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
                context.SaveChanges();
                return true;
            }catch{
                return false;
            }
        }

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            try{
                await dbSet.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }catch{
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity entityToUpdate)
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

        public virtual async Task<bool> DeleteAsync(object id)
        {
            try{
                TEntity entityToDelete = await dbSet.FindAsync(id);
                return await DeleteAsync(entityToDelete);
            }catch{
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
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