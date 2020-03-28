using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using UserAccess.Data.Mongo.Models;

namespace UserAccess.Data.Mongo.Collections
{
    /// <summary>
    ///     This class represents the mongoDB `users` collection
    /// </summary>
    public class UserCollection : IUserCollection
    {
        public UserCollection(IMongoContext<UserDataModel> mongoContext)
        {
            MongoContext = mongoContext;
            MongoContext.CreateInstance("users");
        }
        
        private IMongoContext<UserDataModel> MongoContext { get;  }

        /// <summary>
        ///     Creates a user in the collection.
        /// </summary>
        /// <param name="user">The user data model</param>
        /// <returns></returns>
        public UserDataModel Create(UserDataModel user)
        {
            return MongoContext.InsertOne(user);
        }

        /// <summary>
        ///     Gets a user based on their email.
        /// </summary>
        /// <param name="email">The user's email</param>
        /// <returns></returns>
        public UserDataModel GetUser(string email)
        {
            return MongoContext.Find(x => x.Email == email).FirstOrDefault();
        }

        /// <summary>
        ///     Gets a user based on their id in the db.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDataModel GetUserById(string id)
        {
            var filter = Builders<UserDataModel>.Filter.Eq(x => x.Id, id);
            return MongoContext.Find(filter).FirstOrDefault();
        }

        /// <summary>
        ///     Updates a single user.
        /// </summary>
        /// <param name="user">The user data model</param>
        /// <returns></returns>
        public UserDataModel UpdateUser(UserDataModel user)
        {
            var filter = Builders<UserDataModel>.Filter.Eq(x => x.Id, user.Id);

            var update = Builders<UserDataModel>.Update
                .Set(x => x.FirstName, user.FirstName)
                .Set(x => x.LastName, user.LastName)
                .Set(x => x.Email, user.Email)
                .Set(x => x.Roles, user.Roles);

            var result = MongoContext.UpdateOne(filter, update);
            return result.ModifiedCount > 0 && result.MatchedCount > 0 ? user : null;
        }

        /// <summary>
        ///     Updates a user's role.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="roles">The role to be updated</param>
        /// <returns></returns>
        public bool UpdateUserRoles(string id, List<string> roles)
        {
            var filter = Builders<UserDataModel>.Filter.Eq(x => x.Id, id);
            var update = Builders<UserDataModel>.Update.Set(x => x.Roles, roles);

            var result = MongoContext.UpdateOne(filter, update);
            return result.MatchedCount > 0 && result.MatchedCount > 0;
        }
    }
}