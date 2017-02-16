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
    /// Class responsible for ProductOptions business logic
    /// </summary>
    public class ProductOptionService : IProductOptionService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductOptionService()
        {
            _unitOfWork =  new UnitOfWork();

        }

        /// <summary>
        /// Retrieves multiple options of a product
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>List of products</returns>
        public List<ProductOptionsEntity> GetOptions(Guid productId)
        {
            var options = _unitOfWork.ProductOptionRepository.SelectMany(op => op.ProductId == productId);
            if (options != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductOption, ProductOptionsEntity>());
                IEnumerable<ProductOptionsEntity> productOptionsModel = Mapper.Map<IEnumerable<ProductOption>, IEnumerable<ProductOptionsEntity>>(options);

                return productOptionsModel.ToList();

            }
            return null;
        }
        /// <summary>
        /// Gets an option of a product 
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="OptionId">Option id</param>
        /// <returns>ProductOption</returns>
        public ProductOptionsEntity GetOption(Guid productId, Guid OptionId)
        {
            var productOptions = _unitOfWork.ProductOptionRepository.Get(op => op.Id == OptionId && op.ProductId == productId);
            if (productOptions != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<ProductOption, ProductOptionsEntity>());
                var lstAllOptions = Mapper.Map<ProductOption, ProductOptionsEntity>(productOptions);
                return lstAllOptions;
            }
            return null;

        }
        /// <summary>
        /// Creates new option
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="option">Option</param>
        public void CreateOption(Guid productId, ProductOptionsEntity option)
        {
            using (var scope = new TransactionScope())
            {
                var productOpt = new ProductOption
                {
                    Id = option.Id,
                    ProductId = productId,
                    Description = option.Description,
                    Name = option.Name
                };
                _unitOfWork.ProductOptionRepository.Insert(productOpt);
                _unitOfWork.Save();
                scope.Complete();
            }

        }
        /// <summary>
        /// Updates an option
        /// </summary>
        /// <param name="id">Optionid</param>
        /// <param name="option">ProductOption</param>
        public void UpdateOption(Guid id, ProductOptionsEntity option)
        {
            if (option != null)
            {
                using (var scope = new TransactionScope())
                {
                    var productOption = _unitOfWork.ProductOptionRepository.GetById(id);
                    if (productOption != null)
                    {
                        productOption.Name = option.Name;
                        productOption.Description = option.Description;

                        _unitOfWork.ProductOptionRepository.Update(productOption);
                        _unitOfWork.Save();
                        scope.Complete();
                    }
                }
            }

        }
        /// <summary>
        /// Delets an option
        /// </summary>
        /// <param name="OptionId">Optionid</param>
        public void DeleteOption(Guid OptionId)
        {
            using (var scope = new TransactionScope())
            {
                var product = _unitOfWork.ProductOptionRepository.GetById(OptionId);
                if (product != null)
                {
                    _unitOfWork.ProductOptionRepository.Delete(product);
                    _unitOfWork.Save();
                    scope.Complete();
                }
            }

        }

    }
}
