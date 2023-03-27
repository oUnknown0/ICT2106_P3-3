using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class ProgressReportRepositoryOut : GenericRepositoryOut<Project>
    {   
        public ProgressReportRepositoryOut(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<Project>();
        }

        public async Task<List<Project>> retriveReport(string tag){
            return await dbSet.Where(p => p.ProjectName == tag).ToListAsync();
        }


    }
}