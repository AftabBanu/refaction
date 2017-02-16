using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor_BusinessServices.Entities;

namespace Refactor_BusinessServices.Services
{
    public interface IProductOptionService
    {
        /// <summary>
        /// Gets the list of options
        /// </summary>
        /// <param name="productId">Uniue Product id</param>
        /// <returns>List of options</returns>
        List<ProductOptionsEntity> GetOptions(Guid productId);

        /// <summary>
        /// Retuns single option
        /// </summary>
        /// <param name="productId"> Product id</param>
        /// <param name="id">Option Id</param>
        /// <returns>Option entity</returns>
        ProductOptionsEntity GetOption(Guid productId, Guid id);
        
        /// <summary>
        /// Creates a new option for a product
        /// </summary>
        /// <param name="productId">ProductID to which option has to be created</param>
        /// <param name="option">Option entity</param>
        void CreateOption(Guid productId, ProductOptionsEntity option);
        
        /// <summary>
        /// Updates an option
        /// </summary>
        /// <param name="id">Option Id</param>
        /// <param name="option">Option entity</param>
        void UpdateOption(Guid id, ProductOptionsEntity option);
        
        
        /// <summary>
        /// Deletes an option
        /// </summary>
        /// <param name="id">Option Id</param>
        void DeleteOption(Guid id);
    }
}
