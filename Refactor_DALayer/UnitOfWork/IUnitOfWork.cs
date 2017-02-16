using System;

namespace Refactor_DALayer.UnitOfWork
{
    /// <summary>
    /// Interface for unit of work. It is responsible for transactions
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save method
        /// </summary>
        void Save();

    }
}
