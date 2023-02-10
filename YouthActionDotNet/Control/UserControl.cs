using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YouthActionDotNet.Data;
using YouthActionDotNet.Models;
using YouthActionDotNet.DAL;
using YouthActionDotNet.Controllers;

namespace YouthActionDotNet.Control
{

    public class UserControl : IUserInterfaceCRUD<User>
    {
        private UserRepositoryIn UserRepositoryIn;

        private UserRepositoryOut UserRepositoryOut;

        public UserControl(DBContext context)
        {
            UserRepositoryIn = new UserRepositoryIn(context);
            UserRepositoryOut = new UserRepositoryOut(context);
        }

        public bool Exists(string id)
        {
            return UserRepositoryOut.GetByID(id) != null;
        }

        public async Task<ActionResult<String>> LoginUser(User user)
        {
            var validLoginUser = await UserRepositoryOut.Login(user.username, user.Password);
            if (validLoginUser == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Invalid Username or Password" });
            }
            return JsonConvert.SerializeObject(new { success = true, message = "Login Successful", data = validLoginUser });
        }

        public async Task<ActionResult<string>> Create(User template)
        {
            var users = await UserRepositoryOut.GetAllAsync();
            var existingUser = users.FirstOrDefault(u => u.username == template.username);
            if (existingUser != null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "User Already Exists" });
            }

            var createdUser = await UserRepositoryIn.Register(template.username, template.Password);
            if (createdUser == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Unexpected Error" });
            }
            return JsonConvert.SerializeObject(new { success = true, data = template, message = "User Successfully Created" });
        }

        public async Task<ActionResult<string>> Get(string id)
        {
            var user = await UserRepositoryOut.GetByIDAsync(id);
            if (user == null)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "User Not Found" });
            }
            return JsonConvert.SerializeObject(new { success = true, data = user, message = "User Successfully Retrieved" });
        }

        public async Task<ActionResult<string>> Update(string id, User template)
        {
            if (id != template.UserId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Id Mismatch" });
            }
            await UserRepositoryIn.UpdateAsync(template);
            try
            {
                return await Get(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> UpdateAndFetchAll(string id, User template)
        {
            if (id != template.UserId)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Id Mismatch" });
            }
            await UserRepositoryIn.UpdateAsync(template);
            try
            {
                return await All();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return JsonConvert.SerializeObject(new { success = false, data = "", message = "Volunteer Not Found" });
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<ActionResult<string>> Delete(string id)
        {
            var user = await UserRepositoryOut.GetByIDAsync(id);
            if (user == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "User Not Found" });
            }
            await UserRepositoryIn.DeleteAsync(user);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "User Successfully Deleted" });
        }

        public async Task<ActionResult<string>> Delete(User template)
        {
            var user = await UserRepositoryOut.GetByIDAsync(template.UserId);
            if (user == null)
            {
                return JsonConvert.SerializeObject(new { success = false, data = "", message = "User Not Found" });
            }
            await UserRepositoryIn.DeleteAsync(user);
            return JsonConvert.SerializeObject(new { success = true, data = "", message = "User Successfully Deleted" });
        }

        public async Task<ActionResult<string>> All()
        {
            var users = await UserRepositoryOut.GetAllAsync();
            return JsonConvert.SerializeObject(new { success = true, data = users, message = "Users Successfully Retrieved" });
        }
        public string Settings()
        {

            Settings settings = new UserSettings();
            settings.ColumnSettings.Add("UserId", new ColumnHeader { displayHeader = "User Id" });
            settings.ColumnSettings.Add("username", new ColumnHeader { displayHeader = "Username" });
            settings.ColumnSettings.Add("Email", new ColumnHeader { displayHeader = "Email" });
            settings.ColumnSettings.Add("Password", new ColumnHeader { displayHeader = "Password" });
            settings.ColumnSettings.Add("Role", new ColumnHeader { displayHeader = "Role" });

            return JsonConvert.SerializeObject(new { success = true, data = settings, message = "Settings Successfully Retrieved" });
        }
    }
}
