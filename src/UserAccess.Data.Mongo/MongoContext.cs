using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace UserAccess.Data.Mongo
{
    /// <summary>
    ///     This class is an abstraction of the classes available through the
    ///     MongoDB Driver and must only contain method calls that can be shared
    ///     throughout Collection classes.
    /// </summary>
    /// <typeparam name="TEntityType">Collection Type</typeparam>
    [ExcludeFromCodeCoverage]
    public class MongoContext<TEntityType> : IMongoContext<TEntityType>
    {
        private readonly IMongoClient _client;
        private readonly MongoUrl _url;
        private IMongoDatabase _db;

        public MongoContext(MongoUrl url, IMongoClient client)
        {
            _url = url;
            _client = client;
        }
        
        private IMongoCollection<TEntityType> Collection { get; set; }

        public void CreateInstance(string collectionName)
        {
            _db = _client.GetDatabase(_url.DatabaseName);
            Collection = _db.GetCollection<TEntityType>(collectionName);
        }

        /// <summary>
        ///     Inserts a single entity into a document within the collection.
        /// </summary>
        /// <param name="entity">Entity to be inserted into the collection</param>
        /// <returns>The updated </returns>
        public TEntityType InsertOne(TEntityType entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }

        /// <summary>
        ///     Finds a document in the collection based on the expression.
        /// </summary>
        /// <param name="expression">LINQ expression</param>
        /// <returns></returns>
        public IEnumerable<TEntityType> Find(Expression<Func<TEntityType, bool>> expression)
        {
            return Collection.Find(expression).ToEnumerable();
        }

        /// <summary>
        ///     Finds a document in the collection based on the filter definition.
        /// </summary>
        /// <param name="filter">filter for the TEntityType</param>
        /// <returns></returns>
        public IEnumerable<TEntityType> Find(FilterDefinition<TEntityType> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }

        /// <summary>
        ///     Updates a single document in the collection by finding the document based on the
        ///     filter and applying the update defined to transform the document.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public ReplaceOneResult ReplaceOne(FilterDefinition<TEntityType> filter, TEntityType replacement)
        {
            return Collection.ReplaceOne(filter, replacement);
        }

        /// <summary>
        ///     Updates a single document in the collection by finding the document based on the
        ///     filter and applying the update defined to transform the document. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public UpdateResult UpdateOne(FilterDefinition<TEntityType> filter, UpdateDefinition<TEntityType> update)
        {
            return Collection.UpdateOne(filter, update);
        }
    }
}