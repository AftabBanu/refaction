using System;
using System.Net;
using System.Web.Http;
using Refactor_BusinessServices.Services;
using Refactor_BusinessServices.Entities;
using System.Collections.Generic;
using System.Net.Http;

namespace refactor_me.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        IProductService _productService;
        IProductOptionService _productOptionService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProductsController));

        public ProductsController(IProductService productService, IProductOptionService optnService)
        {
            _productService = productService;
            _productOptionService = optnService;
        }

        [Route]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            log.Info("GetAll Method called for Products");
            var products = _productService.GetAllProducts();

            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products;
                if (productEntities.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }

            log.Info("Products not found");
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [Route]
        [HttpGet]
        public HttpResponseMessage SearchByName(string name)
        {
            log.Info(String.Format("SearchByName Method called for Products. Product Name is : {0}", name));

            var products = _productService.GetProductsByName(name);
            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products;
                if (productEntities.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }

            log.Info(String.Format("No Products listed for Name : {0}", name));

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid id)
        {
            log.Info(String.Format("GetProduct Method called for Product Id : {0}", id));

            var product = _productService.GetProductsById(id);
            if (product != null)
            {
                log.Info(String.Format("Product found infor for Product Name : {0}, Product Desc: {1}, Product Price: {2}", product.Name, product.Description, product.Price));

                return Request.CreateResponse(HttpStatusCode.OK, product);
            }
            log.Info(String.Format("No Product found for Product Id : {0}", id));

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [Route]
        [HttpPost]
        public void Create(ProductEntity product)
        {
            log.Info(String.Format("Create Method called for Product. Details are, Product Id : {0}, Product Name:{1}, Product Desc : {2}, Product Price : {3}", product.Id, product.Name, product.Description, product.Price));
            _productService.CreateProduct(product);
            log.Info(String.Format("Product created"));
        }

        [Route("{id}")]
        [HttpPut]
        public void Update(Guid id, ProductEntity product)
        {
            log.Info(String.Format("Update Product Method called for Product Id : {0}", id));

            if (id != Guid.Empty)
                _productService.UpdateProduct(id, product);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(Guid id)
        {
            log.Info(String.Format("Delete Product Method called for Product Id : {0}", id));

            if (id != Guid.Empty)
                _productService.DeleteProduct(id);
        }

        [Route("{productId}/options")]
        [HttpGet]
        public HttpResponseMessage GetOptions(Guid productId)
        {
            log.Info(String.Format("Get Product Options Method called for Product Id : {0}", productId));

            var prodOptions = _productOptionService.GetOptions(productId);
            if (prodOptions != null)
            {
                var productOptionsEntity = prodOptions as List<ProductOptionsEntity> ?? prodOptions;

                if (productOptionsEntity.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, productOptionsEntity);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [Route("{productId}/options/{id}")]
        [HttpGet]
        public HttpResponseMessage GetOption(Guid productId, Guid id)
        {
            log.Info(String.Format("GetOption Method called for Product Id : {0}, Option ID : {1}", productId, id));

            var prodOptions = _productOptionService.GetOption(productId, id);
            if (prodOptions != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, prodOptions);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Products not found");
        }

        [Route("{productId}/options")]
        [HttpPost]
        public void CreateOption(Guid productId, ProductOptionsEntity option)
        {
            log.Info(String.Format("CreateOption Method called for Product Id : {0}", productId));
            _productOptionService.CreateOption(productId, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpPut]
        public void UpdateOption(Guid id, ProductOptionsEntity option)
        {
            log.Info(String.Format("UpdateOption Method called for Product Id : {0}", id));
            _productOptionService.UpdateOption(id, option);
        }

        [Route("{productId}/options/{id}")]
        [HttpDelete]
        public void DeleteOption(Guid id)
        {
            log.Info(String.Format("DeleteOption Method called for Product Id : {0}", id));
            _productOptionService.DeleteOption(id);
        }
    }
}