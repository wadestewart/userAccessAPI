using System;
using System.Collections.Generic;
using UserAccess.Data.Mongo.Models;
using UserAccess.Models;

namespace UserAccess.Business
{
    public interface IUserManager
    {

        User CreateUser(UserDataModel userModel);

        User GetUser(string email);
        
        User GetUserById(string userId);

        User Update(User user);

        bool UpdateUserRoles(string id, List<string> roles);

    }
}