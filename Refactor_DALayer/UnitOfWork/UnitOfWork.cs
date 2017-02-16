using Refactor_DALayer.Repositories;
using System;
using System.Data;

namespace Refactor_DALayer.UnitOfWork
{
    /// <summary>
    /// Responsible for transactions
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private RefactorDatabaseEntities _context = null;

        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductOption> _productOptionRepository;
        
        public UnitOfWork()
        {
            _context = new RefactorDatabaseEntities();
        }
        /// <summary>
        /// Get Property for product repository.
        /// </summary>
        public GenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._productRepository == null)
                    this._productRepository = new GenericRepository<Product>(_context);
                return _productRepository;
            }
        }
        /// <summary>
        /// Get property for ProductOptionRepository
        /// </summary>
        public GenericRepository<ProductOption> ProductOptionRepository
        {
            get
            {
                if (this._productOptionRepository == null)
                    this._productOptionRepository = new GenericRepository<ProductOption>(_context);
                return _productOptionRepository;
            }
        }
        /// <summary>
        /// Save method
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DataException e)
            {             
                throw e;
            }
        }
        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
