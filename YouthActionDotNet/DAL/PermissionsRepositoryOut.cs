using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL{

    public class PermissionsRepositoryOut : GenericRepositoryOut<Permissions> {
        public PermissionsRepositoryOut(DBContext context) : base(context){
            this.context = context;
            this.dbSet = context.Set<Permissions>();
        }

        public Task<Permissions> GetByRole(string role){
            return dbSet.FirstOrDefaultAsync(p => p.Role == role);
        }
        //To do get normal roles
        public Task<List<Permissions>> GetNormalRoles(){
            string [] roles = {"Volunteer", "Employee", "Donor"};
            return dbSet.Where(p => roles.Contains(p.Role)).ToListAsync();
        }

        public List<Permissions> GetEmployeeRoles(){
            
            string [] roles = {"Volunteer", "Employee", "Donor"};
            return dbSet.Where(p => !roles.Contains(p.Role)).ToList();
        }
        
    }
}