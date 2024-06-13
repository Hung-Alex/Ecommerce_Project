using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Filters.Brand
{
    public record BrandFilter : SpecificationParams
    {
        public string? Name { get; set; }
    }
}
