using Application.Features.Products.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infrastructure.ModelBinding
{
    public class CreateProductCommandModelBinderProvider:IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(CreateProductCommand))
            {
                return new BinderTypeModelBinder(typeof(CreateProuductCommandBinder));
            }
            return null;
        }
    }
}
