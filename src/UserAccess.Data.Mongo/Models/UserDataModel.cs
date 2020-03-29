using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserAccess.Data.Mongo.Models
{
    /// <summary>
    /// A MongoDB data model that represents a document in the users collection
    /// </summary>
    public class UserDataModel
    {
        
        /// <summary>
        /// The unique identifier for a user
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        [BsonElement("firstName")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        [BsonElement("lastName")]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string LastName { get; set; }

        /// <summary>
        /// The user's email
        /// </summary>
        [BsonElement("email")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public string Email { get; set; }

        /// <summary>
        /// The user's password
        /// </summary>
        [BsonElement("password")]
        [BsonRepresentation(BsonType.String)]
        [BsonRequired]
        public string Password { get; set; }
        
        [BsonElement("created")]
        [BsonRepresentation(BsonType.DateTime)]
        [BsonRequired]
        public DateTime Created { get; set; }
        
        /// <summary>
        /// The user's role
        /// </summary>
        [BsonElement("roles")]
        [BsonIgnoreIfNull]
        public List<string> Roles { get; set; }

    }
}