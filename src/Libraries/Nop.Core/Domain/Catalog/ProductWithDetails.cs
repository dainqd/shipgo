using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;

namespace Nop.Core.Domain.Catalog

{
    public class ProductWithDetails
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public IList<ProductPicture> ProductPictures { get; set; }
    }
}