using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nop.Services.Catalog;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Nop.Core.Domain.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.RestApi

{
    [Route("api/category")]
    [AllowAnonymous]
    [ApiController]
    public class ApiCategoryController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<ApiCategoryController> _logger;

        public ApiCategoryController(IProductService productService, ICategoryService categoryService,ILogger<ApiCategoryController> logger)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
        }
        
        [HttpGet("list")]
        public Task<IList<Category>> getCategory()
        {
            var categories = _categoryService.GetAllCategoriesAsync(0, false);
            return categories;
        }
         
        [HttpGet("item/{id}")]
        public IActionResult  getProductByCategory(int id)
        {
            var products = _productService.GetAllProductByCategory(id);
            return Ok(products);
        }
    }
}