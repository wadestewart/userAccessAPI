using System.Collections.Generic;

namespace UserAccess.API.Models
{
    /// <summary>
    ///     The API model that represents a User.
    /// </summary>
    public class UserApiModel
    {
        /// <summary>
        ///     The id of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     The email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The role of the user.
        /// </summary>
        public List<string> Roles { get; set; }
    }
}