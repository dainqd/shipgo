using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nop.Services.Catalog;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Nop.Core.Domain.Catalog;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Services.Media;

namespace Nop.Web.Controllers.RestApi

{
    [Route("api/product")]
    [AllowAnonymous]
    [ApiController]
    public class ApiProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IPictureService _pictureService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ApiProductController> _logger;

        public ApiProductController(
            IProductService productService, 
            IProductAttributeService productAttributeService,
            ICategoryService categoryService,
            IPictureService pictureService,
            ILogger<ApiProductController> logger
            )
        {
            _productService = productService;
            _productAttributeService = productAttributeService;
            _categoryService = categoryService;
            _pictureService = pictureService;
            _logger = logger;
        }
        
        [HttpGet("list-product")]
        public IActionResult? GetAll()
        {
            try
            {
                var products = _productService.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products for homepage.");
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("list-product-active")]
        public async Task<IActionResult> Test()
        {
            try
            {
                var products = await _productService.GetAllProductsDisplayedOnHomepageAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products for homepage.");
                return StatusCode(500, "Internal Server Error");
            }
        }
        
        [HttpGet("detail/{id}")]
        public Task<Product> Detail(int id)
        {
                var product = _productService.GetProductByIdAsync(id);
                return product;
        }
        
        [HttpGet("picture/{id}")]
        public async Task<ProductPicture> getPicture(int id)
        {
            var picture = await _productService.GetProductPictureByIdAsync(id);
            return picture;
        }
        
        [HttpGet("list-picture/{id}")]
        public async Task<IList<ProductPicture>> GetProductCategoriesByCategoryId(int id)
        {
            var productPictures = await _productService.GetProductPicturesByProductIdAsync(id);
            return productPictures;
        }
        
        [HttpGet("pic-binary/{id}")]
        public async Task<IActionResult> GetPictureBinaryByPictureIdAsync(int id)
        {
            var picture = await _pictureService.GetPictureBinaryByPictureIdAsync(id);
            var imageName = picture.BinaryData;
            var imagePath = Path.Combine("wwwroot/images/thumbs/", $"image_{id}.jpg");
            System.IO.File.WriteAllBytes(imagePath, imageName);
            string imageUrl = $"{imagePath}";
            return Ok(imageUrl);
        }
        
        [HttpGet("list")]
        public Task<IList<Category>> getCategory()
        {
            var categories = _categoryService.GetAllCategoriesAsync(0);
            return categories;
        }
        
        [HttpGet("category/{id}")]
        public IActionResult getProductByCategory(int id)
        {
            var products = _productService.GetAllProductByCategory(id);
            return Ok(products);
        }
        
        [HttpGet("attributes")]
        public Task<IPagedList<ProductAttribute>> GetAllProductAttributesAsync()
        {
            var attributes = _productAttributeService.GetAllProductAttributesAsync();
            return attributes;
        }
        
        [HttpGet("product-attributes/{productID}")]
        public Task<IList<ProductAttributeMapping>> GetProductAttributeMappingsByProductIdAsync(int productID)
        {
            var productAttributes = _productAttributeService.GetProductAttributeMappingsByProductIdAsync(productID);
            return productAttributes;
        }
        
        [HttpGet("product-attribute-value/{productAttributeID}")]
        public Task<IList<ProductAttributeValue>> GetProductAttributeValuesAsync(int productAttributeID)
        {
            var productAttributeValue = _productAttributeService.GetProductAttributeValuesAsync(productAttributeID);
            return productAttributeValue;
        }
    }
}