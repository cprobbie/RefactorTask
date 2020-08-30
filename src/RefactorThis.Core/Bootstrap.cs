using Microsoft.Extensions.DependencyInjection;

using RefactorThis.Core.OptionProcessor;
using RefactorThis.Core.Processor;

namespace RefactorThis.Core
{
    public static class Bootstrap
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICreateProductRequestProcessor, CreateProductRequestProcessor>();
            services.AddScoped<IGetProductRequestProcessor, GetProductRequestProcessor>();
            services.AddScoped<IUpdateProductRequestProcessor, UpdateProductRequestProcessor>();
            services.AddScoped<IDeleteProductRequestProcessor, DeleteProductRequestProcessor>();

            services.AddScoped<ICreateOptionRequestProcessor, CreateOptionRequestProcessor>(); 
            services.AddScoped<IGetOptionsRequestProcessor, GetOptionsRequestProcessor>(); 
            services.AddScoped<IUpdateOptionRequestProcessor, UpdateOptionRequestProcessor>();
            services.AddScoped<IDeleteOptionRequestProcessor, DeleteOptionRequestProcessor>();
        }
    }
}
