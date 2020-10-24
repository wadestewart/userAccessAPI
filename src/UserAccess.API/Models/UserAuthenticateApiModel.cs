namespace UserAccess.API.Models
{
    /// <summary>
    /// Model to authenticate when logging in
    /// </summary>
    public class UserAuthenticateApiModel
    {
        /// <summary>
        /// The returning user's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The returning user's password
        /// </summary>
        public string Password { get; set; }
    }
}