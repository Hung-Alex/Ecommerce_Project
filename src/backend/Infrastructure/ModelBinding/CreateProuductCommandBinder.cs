using Application.Features.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;


namespace Infrastructure.ModelBinding
{
    public class CreateProuductCommandBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
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
                IEnumerable<CreateProductSkus> variant = null;

                if (variantJsonList.Count > 0)
                {
                    try
                    {
                        variant = variantJsonList.Select(v => JsonConvert.DeserializeObject<CreateProductSkus>(v));
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Invalid Variant JSON");
                    }
                }
                var images = form.Files.Count > 0 ? form.Files : null;
                var result = new CreateProductCommand(
                    name,
                    description,
                    urlSlug,
                    price,
                    discount,
                    brandId,
                    categoryId,
                    variant,
                    images
                );
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (Exception ex)
            {
                bindingContext.HttpContext.Response.StatusCode = 400; // Set status code to 400
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex.Message);
                bindingContext.Result = ModelBindingResult.Failed();
            }
            return Task.CompletedTask;
        }
    }
}
