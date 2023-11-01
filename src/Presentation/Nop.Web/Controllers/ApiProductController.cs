using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nop.Services.Catalog;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace Nop.Web.Controllers

{
    [Route("api/product")]
    [ApiController]
    public class ApiProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ApiProductController> _logger;

        public ApiProductController(IProductService productService, ILogger<ApiProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
    
        [AllowAnonymous]
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
    
        [AllowAnonymous]
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
        
        [AllowAnonymous]
        [HttpGet("list-product-test")]
        public async Task<IActionResult> Demo()
        {
            try
            {
                var products = await _productService.GetAllProductsDisplayed();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products for homepage.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}