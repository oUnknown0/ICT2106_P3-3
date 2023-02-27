using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL{

    public class ProjectRepositoryIn : GenericRepositoryIn<Project> {
        public ProjectRepositoryIn(DBContext context) : base(context){
            this.context = context;
            this.dbSet = context.Set<Project>();
        }
    }
}