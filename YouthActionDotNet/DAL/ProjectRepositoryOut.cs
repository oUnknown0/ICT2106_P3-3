using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class ProjectRepositoryOut : GenericRepositoryOut<Project>
    {   
        public ProjectRepositoryOut(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Project>();
        }
        //-------------------------------TO BE UPDATED---------------------------------------//
        public async Task<List<Project>> GetProjectByTag(string tag){
            return await dbSet.Where(p => p.ProjectName == tag).ToListAsync();
        }
        public async Task<List<Project>> GetProjectInProgress(){
            return await dbSet.Where(p => p.ProjectStatus == "In progress").ToListAsync();
        }
        public async Task<List<Project>> GetProjectPinned(){
            return await dbSet.Where(p => p.ProjectStatus == "Pinned").ToListAsync();
        }
        public async Task<List<Project>> GetProjectArchived(){
            return await dbSet.Where(p => p.ProjectStatus == "Archived").ToListAsync();
        }
        //-------------------------------TO BE UPDATED---------------------------------------//
    }
}