using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor_BusinessServices.Entities;

namespace Refactor_BusinessServices.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Retrieves products by name 
        /// </summary>
        /// <param name="name"> name of the product</param>
        /// <returns>list of productentities</returns>
        IList<ProductEntity> GetProductsByName(string name);

        /// <summary>
        /// Selects all products 
        /// </summary>
        /// <returns>List of products</returns>
        IList<ProductEntity> GetAllProducts();

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productEntity">Product entity</param>
        void CreateProduct(ProductEntity productEntity);

        /// <summary>
        /// Updates  product
        /// </summary>
        /// <param name="productId"> Product id</param>
        /// <param name="productEntity"> Product entity to get updated</param>
        /// <returns></returns>
        bool UpdateProduct(object productId, ProductEntity productEntity);


        /// <summary>
        ///  Delets prduct based on product id
        /// </summary>
        /// <param name="productId">Product</param>
        /// <returns>retuns true or false</returns>
        bool DeleteProduct(Guid productId);

        /// <summary>
        /// Gets one product by product id
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns>product</returns>
        ProductEntity GetProductsById(Guid productId);

    }
}
