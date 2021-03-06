using BioStore.Domain.StoreContext.Handlers;
using BioStore.Domain.StoreContext.Repositories;
using BioStore.Domain.StoreContext.Services;
using BioStore.Infra.DataContexts;
using BioStore.Infra.StoreContext.Repositories;
using BioStore.Infra.StoreContext.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Elmah.Io.AspNetCore;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using BioStore.Shared;
using BioStore.Api.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BioStore.IoC;

namespace BioStore.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApplicationInsightsTelemetry(Configuration);

            services.AddLogging();
            services.AddAuthorizedMvc();

            services.AddMvc().AddJsonOptions(
            options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddMemoryCache();

         

            services.AddResponseCompression();
            services.AddApplicationServices();
           // Settings.ConnectionString = $"{Configuration["connectionString"]}";
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseResponseCompression();

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta Store - V1");
            //});

            //app.UseElmahIo("923f4c946cc1435cb0ec665d6e7370b7", new Guid("e42a9995-df89-4d91-a625-ecc57d124004"));
        }
    }
}
