using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refactor_BusinessServices.Services;
using Refactor_BusinessServices.Entities;

namespace RefactorTests
{
    [TestFixture]
    public class IntegrationTests
    {
        IProductOptionService _productsOptionService;
        IProductService _productsService;
        [SetUp]
        public void SetUp()
        {
            _productsOptionService = new ProductOptionService();
            _productsService = new ProductService();
        }

        [TearDown]
        public void DisposeTest()
        {
            _productsService = null;
            _productsOptionService = null;
             
        }

        #region Product Tests

        //this test case covers Creating new products, and Selecting all products
        [Test]
        public void GetProducts()
        {
            Guid Id1 = Guid.NewGuid();
            var Id2 = Guid.NewGuid();
            var Id3 = Guid.NewGuid();
            var product1 = new ProductEntity()
            {
                Id = Id1,
                Name = "Product 1",
                Description = "Product 1 Description",
                Price = 999.99M,
                DeliveryPrice = 9.8M
            };
            var product2 = new ProductEntity()
            {
                Id = Id2,
                Name = "Product 2",
                Description = "Product 2 Description",
                Price = 143,
                DeliveryPrice = 8
            };
            var product3 = new ProductEntity()
            {
                Id = Id3,
                Name = "Product 3",
                Description = "Product 3 Description",
                Price = 890.3M,
                DeliveryPrice = 11
            };
            _productsService.CreateProduct(product1);
            _productsService.CreateProduct(product2);
            _productsService.CreateProduct(product3);

            var products = _productsService.GetAllProducts();
            int productCount = products.Where(n => (n.Id == Id1) || (n.Id == Id2) || (n.Id == Id3)).ToList().Count;
            Assert.True(3 == productCount);
        }

        //This test case tests CreateProduct, GetProductById, DeleteProduct methods
        [Test]
        public void DeleteProduct()
        {
            Guid Id = Guid.NewGuid();

            var product = new ProductEntity()
            {
                Id = Id,
                Name = "Product to delete",
                Description = "Product to delete Description",
                Price = 2999.99M,
                DeliveryPrice = 93.8M
            };

            //Creating a product
            _productsService.CreateProduct(product);

            //Verifying product created or not
            var prod = _productsService.GetProductsById(Id);
            Assert.True(prod != null, "Product successfully Created");

            //Deleting the product
            _productsService.DeleteProduct(Id);

            //Verifying product deleted or not
            var prodAfterDeletion = _productsService.GetProductsById(Id);

            Assert.True(prodAfterDeletion == null, "Product successfully Deleted");
        }

        //This test case tests CreateProduct, GetProductByName, UpdateProduct methods
        [Test]
        public void UpdateProduct()
        {
            Guid Id = Guid.NewGuid();

            var productOriginal = new ProductEntity()
            {
                Id = Id,
                Name = "ProductToUpdate",
                Description = "Product to Update Description",
                Price = 2999.99M,
                DeliveryPrice = 93.8M
            };

            var productUpdate = new ProductEntity()
            {
                Id = Id,
                Name = "AfterUpdate",
                Description = "After update desription",
                Price = 2999.99M,
                DeliveryPrice = 93.8M
            };

            //Creating a product
            _productsService.CreateProduct(productOriginal);

            //Verifying product created or not
            var prod = _productsService.GetProductsByName("ProductToUpdate");
            Assert.True(prod != null, "Product successfully Created");

            //Updating the product
            _productsService.UpdateProduct(Id, productUpdate);

            //Verifying product deleted or not
            var prodAfterUpdate = _productsService.GetProductsById(Id);

            Assert.True(prodAfterUpdate.Name == "AfterUpdate", "Product successfully Updated");
        }

        #endregion
        #region ProductOptions

        //this test case covers CreateOption, GetOptions (by product id), GetOption (by product id and option id) methods
        [Test]
        public void GetOptions()
        {
            Guid ProductId = Guid.NewGuid();
            Guid OptionId1 = Guid.NewGuid();
            var OptionId2 = Guid.NewGuid();

            var product = new ProductEntity()
            {
                Id = ProductId,
                Name = "Product With Options",
                Description = "Product with options Description",
                Price = 999.99M,
                DeliveryPrice = 9.8M
            };
            var productOption1 = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId1,
                Name = "Option 1",
                Description = "Option 1 Description"

            };
            var productOption2 = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId2,
                Name = "Option 2",
                Description = "Option 2 Description"
            };

            _productsService.CreateProduct(product);
            _productsOptionService.CreateOption(ProductId, productOption1);
            _productsOptionService.CreateOption(ProductId, productOption2);

            var productOptions = _productsOptionService.GetOptions(ProductId);
            Assert.True(2 == productOptions.Count, "Two options created successfully");

            var productOption = _productsOptionService.GetOption(ProductId, OptionId1);

            Assert.True("Option 1" == productOption.Name, "Successfully retrieved option 1");

        }


        //this test case covers CreateOption, GetOptions (by product id), GetOption (by product id and option id) methods
        [Test]
        public void DeleteOption()
        {
            Guid ProductId = Guid.NewGuid();
            Guid OptionId1 = Guid.NewGuid();
            var OptionId2 = Guid.NewGuid();

            var product = new ProductEntity()
            {
                Id = ProductId,
                Name = "Product With Options",
                Description = "Product with options Description",
                Price = 999.99M,
                DeliveryPrice = 9.8M
            };
            var productOption1 = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId1,
                Name = "Option 1",
                Description = "Option 1 Description"

            };
            var productOption2 = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId2,
                Name = "Option 2",
                Description = "Option 2 Description"
            };

            _productsService.CreateProduct(product);
            _productsOptionService.CreateOption(ProductId, productOption1);
            _productsOptionService.CreateOption(ProductId, productOption2);

            var productOptions = _productsOptionService.GetOptions(ProductId);
            Assert.True(2 == productOptions.Count, "Two options created successfully");

            _productsOptionService.DeleteOption(OptionId1);

            var productOption = _productsOptionService.GetOption(ProductId, OptionId1);

            Assert.True(productOption == null, "Successfully deleted option 1");

        }

        //this test case covers Update methods
        [Test]
        public void UpdateOption()
        {
            Guid ProductId = Guid.NewGuid();
            Guid OptionId1 = Guid.NewGuid();

            var product = new ProductEntity()
            {
                Id = ProductId,
                Name = "Product With Options",
                Description = "Product with options Description",
                Price = 999.99M,
                DeliveryPrice = 9.8M
            };
            var productOptionOriginal = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId1,
                Name = "Option Original",
                Description = "Option original Description"

            };
            var productOptionUpdate = new ProductOptionsEntity()
            {
                ProductId = ProductId,
                Id = OptionId1,
                Name = "Option_updated",
                Description = "Option 1 updated Description"
            };

            _productsService.CreateProduct(product);
            _productsOptionService.CreateOption(ProductId, productOptionOriginal);

            var productOptions = _productsOptionService.GetOptions(ProductId);
            Assert.True(1 == productOptions.Count, "One option created successfully");

            _productsOptionService.UpdateOption(OptionId1, productOptionUpdate);

            var productOption = _productsOptionService.GetOption(ProductId, OptionId1);
            Assert.True("Option_updated" == productOption.Name, "Option name updated successfully");

        }

        #endregion
    }
}
