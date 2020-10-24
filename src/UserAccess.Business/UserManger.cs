using System;
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
        ///     This method manges creating a user
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns>New User Model</returns>
        public User CreateUser(string firstName, string lastName, string email, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new UserDataModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = hash,
                Created = DateTime.Now
            };
            var result = _userCollection.Create(user);
            return _mapper.Map<User>(result);
        }

        /// <summary>
        ///     This method authenticates a user
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public User Authenticate(string email, string password)
        {
            // null check on passed in values
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            // get user from the data layer
            var userDataModel = _userCollection.GetUser(email);
            
            // verify user exists in db
            if (userDataModel == null) return null;

            // map data model to user
            var user = _mapper.Map<User>(userDataModel);

            // authenticate user
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
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


        #region Private Methods
        

        #endregion
    }
}