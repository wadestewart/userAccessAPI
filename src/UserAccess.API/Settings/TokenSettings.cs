namespace UserAccess.API.Settings
{
    public class TokenSettings
    {
        /// <summary>
        /// Key used to validate the Token
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The issuer of the token
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The length of time the token is valid
        /// </summary>
        public double Expiration { get; set; }
    }
}