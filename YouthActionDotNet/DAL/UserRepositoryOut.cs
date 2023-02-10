using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;

namespace YouthActionDotNet.DAL
{
    public class UserRepositoryOut : GenericRepositoryOut<User>
    {   
        public UserRepositoryOut(DBContext context) : base(context)
        {
            this.context = context;
            this.dbSet = context.Set<User>();
        }

        public virtual async Task<User> Login(string username, string password)
        {
            string hashedPassword = Utils.hashpassword(password);
            var user = await dbSet.FirstOrDefaultAsync(u => u.username == username && u.Password == hashedPassword);
            if (user == null)
                return null;
            return user;
        }
    }
}