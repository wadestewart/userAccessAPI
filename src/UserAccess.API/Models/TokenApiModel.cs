namespace UserAccess.API.Models
{
    /// <summary>
    /// The token for a successful authentication response
    /// </summary>
    public class TokenApiModel
    {
        /// <summary>
        /// The generated token when a user is authenticated
        /// </summary>
        public string Token { get; set; }
    }
}