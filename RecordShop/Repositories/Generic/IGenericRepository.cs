﻿using RecordShop.Model;

namespace RecordShop.Repositories.Generic
{
    public interface IGenericRepository<TEntity> where TEntity : class, IIdentifiable
    {
        // Create
        public Func<int> InsertEntity(TEntity entity);

        // Read
        public IEnumerable<TEntity> GetEntities();

        public TEntity? GetEntityById(int id);

        // Update
        public TEntity? UpdateEntity(TEntity entity);

        // Delete
        public TEntity? DeleteEntityById(int id);

        // Save
        public void Save();
    }
}
