using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor_BusinessServices.Entities;
using Refactor_DALayer;
using Refactor_DALayer.UnitOfWork;
using AutoMapper;
using System.Transactions;


namespace Refactor_BusinessServices.Services
{
    /// <summary>
    /// Class responsible for Product business logic
    /// </summary>
    public class ProductService :IProductService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductService()
        {
            _unitOfWork = new UnitOfWork(); 
        }

        /// <summary>
        /// Retrieves products by name
        /// </summary>
        /// <param name="name">Name of the product</param>
        /// <returns>multiple products</returns>
        public IList<ProductEntity> GetProductsByName(string name)
        {
            var product = _unitOfWork.ProductRepository.SelectMany(p => p.Name == name);
            if (product != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                var lstProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductEntity>>(product);
                return lstProducts.ToList();
            }

            return null;


        }
        /// <summary>
        /// All products will be returned
        /// </summary>
        /// <returns>List of products</returns>
        public IList<ProductEntity> GetAllProducts()
        {
            try
            {
                var products = _unitOfWork.ProductRepository.SelectAll();
                if (products != null)
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                    var lstAllProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductEntity>>(products);
                    return lstAllProducts.ToList();
                }

                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Gets product by an id
        /// </summary>
        /// <param name="Id">Product id</param>
        /// <returns>Product entity</returns>
        public ProductEntity GetProductsById(Guid Id)
        {
            var product = _unitOfWork.ProductRepository.GetById(Id);
            if (product != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductEntity>());
                var lstProducts = Mapper.Map<Product, ProductEntity>(product);
                return lstProducts;
            }

            return null;


        }

        /// <summary>
        /// creates a new product
        /// </summary>
        /// <param name="productObj">Product entity</param>
        public void CreateProduct(ProductEntity productObj)
        {
            using (var scope = new TransactionScope())
            {
                var product = new Product
                {
                    Id = productObj.Id,
                    Name = productObj.Name,
                    DeliveryPrice = productObj.DeliveryPrice,
                    Description = productObj.Description,
                    Price = productObj.Price
                };
                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Save();
                scope.Complete();
             }

            
        }
        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="productObj">Producr object</param>
        /// <returns>true or false</returns>
        public bool UpdateProduct(object productId, ProductEntity productObj)
        {
            var success = false;
            if (productObj != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetById(productId);
                    if (product != null)
                    {
                        product.Name = productObj.Name;
                        product.Description = productObj.Description;

                        _unitOfWork.ProductRepository.Update(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


        /// <summary>
        /// Deeltes a product
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns> true or false</returns>
        public bool DeleteProduct(Guid productId) {

            var success = false;
            if (productId != Guid.Empty)
            {
                using (var scope = new TransactionScope())
                {
                    var product = _unitOfWork.ProductRepository.GetById(productId);
                    if (product != null)
                    {
                        _unitOfWork.ProductRepository.Delete(product);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;

        }

    }
}
