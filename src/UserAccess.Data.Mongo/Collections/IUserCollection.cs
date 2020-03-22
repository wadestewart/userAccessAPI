using System.Collections.Generic;
using UserAccess.Data.Mongo.Models;

namespace UserAccess.Data.Mongo.Collections
{
    public interface IUserCollection
    {
        /// <summary>
        ///     Creates a user.
        /// </summary>
        /// <param name="user">The user data model</param>
        /// <returns></returns>
        UserDataModel Create(UserDataModel user);

        /// <summary>
        ///     Gets a user based on the user's email.
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns></returns>
        UserDataModel GetUser(string email);

        /// <summary>
        ///     Gets a user based on the Id in the db.
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <returns></returns>
        UserDataModel GetUserById(string id);

        /// <summary>
        ///     Updates a user.
        /// </summary>
        /// <param name="user">The user data model</param>
        /// <returns></returns>
        UserDataModel UpdateUser(UserDataModel user);

        /// <summary>
        ///     Updates a user's role.
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <param name="roles">The role to be updated</param>
        /// <returns></returns>
        bool UpdateUserRoles(string id, List<string> roles);
    }
}