using System.ComponentModel.DataAnnotations;

namespace UserAccess.API.Models
{
    /// <summary>
    ///     This is the model to create a user
    /// </summary>
    public class CreateUserApiModel
    {
        /// <summary>
        ///     The first name of the new user
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        ///     The last name of the new user
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        ///     The email of the new user
        /// </summary>
        [Required]
        public string Email { get; set; }
        
        /// <summary>
        ///     The new user's password
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}