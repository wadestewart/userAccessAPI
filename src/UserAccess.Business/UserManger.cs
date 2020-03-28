using System.Collections.Generic;
using AutoMapper;
using UserAccess.Data.Mongo.Collections;
using UserAccess.Data.Mongo.Models;
using UserAccess.Models;

namespace UserAccess.Business
{
    /// <summary>
    ///     This class contains the implementation for managing users.
    /// </summary>
    public class UserManger : IUserManager
    {

        #region Variables
        private readonly IMapper _mapper;
        private readonly IUserCollection _userCollection;
        #endregion

        #region Constructor
        public UserManger(IUserCollection collection, IMapper mapper)
        {
            _userCollection = collection;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods

        /// <summary>
        ///     Creates a new user.
        /// </summary>
        /// <param name="user">The user model</param>
        /// <returns></returns>
        public User CreateUser(UserDataModel user)
        {
            var result = _userCollection.Create(user);
            return _mapper.Map<User>(result);
        }

        /// <summary>
        ///     Gets a user based on the user's email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUser(string email)
        {
            var userDataModel = _userCollection.GetUser(email);
            if (userDataModel == null) return null;

            var user = _mapper.Map<User>(userDataModel);
            return user;
        }

        /// <summary>
        ///     Gets user by the user's id in the db.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserById(string id)
        {
            var result = _userCollection.GetUserById(id);
            return _mapper.Map<User>(result);
        }

        /// <summary>
        ///     Updates a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User Update(User user)
        {
            var result = _userCollection.UpdateUser(_mapper.Map<UserDataModel>(user));
            return _mapper.Map<User>(result);
        }

        /// <summary>
        ///     Update the roles of a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool UpdateUserRoles(string id, List<string> roles)
        {
            return _userCollection.UpdateUserRoles(id, roles);
        }

        #endregion

    }
}