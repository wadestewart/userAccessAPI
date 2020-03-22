using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace UserAccess.Data.Mongo
{
    public interface IMongoContext<TEntityType>
    {
        void CreateInstance(string collectionName);

        TEntityType InsertOne(TEntityType entity);

        IEnumerable<TEntityType> Find(Expression<Func<TEntityType, bool>> expression);

        IEnumerable<TEntityType> Find(FilterDefinition<TEntityType> filter);

        ReplaceOneResult ReplaceOne(FilterDefinition<TEntityType> filter, TEntityType update);

        UpdateResult UpdateOne(FilterDefinition<TEntityType> filter, UpdateDefinition<TEntityType> update);
    }
}