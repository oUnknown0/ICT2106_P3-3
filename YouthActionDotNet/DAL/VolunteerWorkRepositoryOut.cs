using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class VolunteerWorkRepositoryOut : GenericRepositoryOut<VolunteerWork>
    {   
        public VolunteerWorkRepositoryOut(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<VolunteerWork>();
        }
        
        public async Task<List<VolunteerWork>> GetByVolunteerId(string id){
            return await dbSet.Where(v => v.VolunteerId == id).ToListAsync();
        }
    }
}