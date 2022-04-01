using Shop.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password) 
        {
            var users = new List<User>();
            users.Add(new User {Id = 1, UserName= "Paulo" , Password = "batman" , Role="employee" });
            users.Add(new User { Id = 2, UserName = "Sandro", Password="robin", Role="employee"});
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower() ).FirstOrDefault();
        }
    }
}
