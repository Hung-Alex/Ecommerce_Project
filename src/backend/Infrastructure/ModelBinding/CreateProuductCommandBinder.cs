using Application.Features.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;


namespace Infrastructure.ModelBinding
{
    public class CreateProuductCommandBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var form = bindingContext.HttpContext.Request.Form;

            try
            {
                var name = form["Name"].FirstOrDefault();
                var description = form["Description"].FirstOrDefault();
                var urlSlug = form["UrlSlug"].FirstOrDefault();
                if (!decimal.TryParse(form["Price"].FirstOrDefault(), out var price))
                {
                    throw new ArgumentException("Invalid Price");
                }
                if (!decimal.TryParse(form["OldPrice"].FirstOrDefault(), out var oldPrice))
                {
                    throw new ArgumentException("Invalid Old Price");
                }
                var discount = int.TryParse(form["Discount"].FirstOrDefault(), out var disc) ? disc : (int?)null;
                if (!Guid.TryParse(form["BrandId"].FirstOrDefault(), out var brandId))
                {
                    throw new ArgumentException("Invalid BrandId");
                }
                if (!Guid.TryParse(form["CategoryId"].FirstOrDefault(), out var categoryId))
                {
                    throw new ArgumentException("Invalid BrandId");
                }
                var variantJsonList = form["Variant"].ToList();
                List<CreateProductSkus> variants = new List<CreateProductSkus>();
                if (variantJsonList.Count > 0)
                {
                    var cleanedVariantJsonList = variantJsonList
                        .Select(v => (v))
                        .ToList();

                    foreach (var v in cleanedVariantJsonList)
                    {
                        try
                        {

                            var variant = JsonConvert.DeserializeObject<CreateProductSkus>(v);
                            if (variant != null)
                            {
                                variants.Add(variant);
                            }
                            else
                            {
                                Console.WriteLine($"Warning: Deserialization returned null for JSON: {v}");
                            }
                        }
                        catch (JsonException jsonEx)
                        {
                            bindingContext.HttpContext.Response.StatusCode = 400; // Set status code to 400
                            bindingContext.ModelState.AddModelError(bindingContext.ModelName, jsonEx.Message);
                            bindingContext.Result = ModelBindingResult.Failed();
                        }
                    }
                    var images = form.Files.Count > 0 ? form.Files : null;
                    var result = new CreateProductCommand(
                        name,
                        description,
                        urlSlug,
                        price,
                        oldPrice,
                        discount,
                        brandId,
                        categoryId,
                        variants,
                        images
                    );
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }
            catch (Exception ex)
            {
                bindingContext.HttpContext.Response.StatusCode = 400; // Set status code to 400
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}

