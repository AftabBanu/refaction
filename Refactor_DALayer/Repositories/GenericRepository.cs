using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Data;

namespace Refactor_DALayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal RefactorDatabaseEntities _context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(RefactorDatabaseEntities context)
        {
            this._context = context;
            this.DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Generic Insert method
        /// </summary>
        /// <param name="entity">New entity</param>
        public void Insert(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        /// <summary>
        /// Generic delete method.
        /// </summary>
        /// <param name="entityToDelete"> Entity to get deleted</param>
        public void Delete(TEntity entityToDelete)
        {
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }
                DbSet.Remove(entityToDelete);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        /// <summary>
        /// Genericc Update method.
        /// </summary>
        /// <param name="entity">Entity being updated</param>
        public void Update(TEntity entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        /// <summary>
        /// generic select method based on condition.
        /// </summary>
        /// <param name="where">generic condition</param>
        /// <returns> Returns list of entities</returns>
        public IEnumerable<TEntity> SelectMany(Func<TEntity, Boolean> where)
        {
            try
            {
                return DbSet.Where(where).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        
        /// <summary>
        ///  generic select all method
        /// </summary>
        /// <returns>enumerable list of entities </returns>
        public IEnumerable<TEntity> SelectAll()
        {
            try
            {
                return DbSet.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        /// <summary>
        /// generic select method based on unique id. One entity will be retuned.
        /// </summary>
        /// <param name="id">Unique id</param>
        /// <returns>Entity</returns>
        public TEntity GetById(object id)
        {
            try
            {
                return DbSet.Find(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// generic select method , selects data based on condition.
        /// </summary>
        /// <param name="where">Condition</param>
        /// <returns>Entity</returns>
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            try
            {
                return DbSet.Where(where).FirstOrDefault<TEntity>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
