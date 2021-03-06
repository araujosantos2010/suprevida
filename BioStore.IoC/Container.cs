using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BioStore.Shared;
using System.IO;
using BioStore.Infra.DataContexts;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Infra.StoreContext.Repositories;
using BioStore.Domain.StoreContext.Handlers;

namespace BioStore.IoC
{
    public static class Container
    {
        public static IConfiguration Configuration { get; set; }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            services.AddRepositories();
            services.AddHandlers();
            services.AddServices();
            return services;
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<BioStoreDataContext, BioStoreDataContext>();
            services.AddTransient<IMarcaRepository, MarcaRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();

        }

        public static void AddHandlers(this IServiceCollection services)
        {
            services.AddTransient<MarcaHandler, MarcaHandler>();
            services.AddTransient<ProdutoHandler, ProdutoHandler>();
            services.AddTransient<CategoriaHandler, CategoriaHandler>();
            services.AddTransient<GradeHandler, GradeHandler>();            
        }

        public static void AddServices(this IServiceCollection services)
        {
            
        }
    }
}
