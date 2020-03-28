using System;
using System.Collections.Generic;

namespace UserAccess.Models
{
    /// <summary>
    ///     Represents a user.
    /// </summary>
    public class User
    {
        
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public List<string> Roles { get; set; }
        
    }
}