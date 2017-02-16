using System;
using System.Collections.Generic;

namespace Refactor_DALayer.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Geneic Method to select many entities 
        /// </summary>
        /// <param name="name"> condition</param>
        /// <returns>list of entities</returns>
        IEnumerable<TEntity> SelectMany(Func<TEntity, Boolean> name);
        
        
        /// <summary>
        /// Generic method to select all items in entity
        /// </summary>
        /// <returns>list of entites</returns>
        IEnumerable<TEntity> SelectAll();
        
        
        /// <summary>
        /// Generic add method
        /// </summary>
        /// <param name="entity">new entity</param>
        void Insert(TEntity entity);
        
        
        /// <summary>
        /// Generic delete method
        /// </summary>
        /// <param name="entity">entity to delete</param>
        void Delete(TEntity entity);
        
        /// <summary>
        /// Generic update method
        /// </summary>
        /// <param name="entity">entity to update</param>
        void Update(TEntity entity);

        /// <summary>
        /// Generic method to select particular entity
        /// </summary>
        /// <param name="id"> id</param>
        /// <returns>single entity</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Generic method to select single entity 
        /// </summary>
        /// <param name="where">condition</param>
        /// <returns>entity</returns>
        TEntity Get(Func<TEntity, Boolean> where);
    }
}
