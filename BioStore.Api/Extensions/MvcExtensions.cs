using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BioStore.Api.Extensions
{
    public static class MvcExtensions
    {
        public static IConfiguration Configuration
        {
            get
            {

                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
                return builder.Build();
            }
        }

        public static void AddAuthorizedMvc(this IServiceCollection services)
        {
            //AddJwtAuthorization(services);
            //AddAuthenticatedUser(services);
            AddMvc(services);
        }

        //private static void AddJwtAuthorization(IServiceCollection services)
        //{
        //    var authorityEndPoint = Configuration.GetSection("ConfigSettings:AuthorityEndPoint").Value;
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        //    })
        //  .AddCookie()
        //  .AddOpenIdConnect(options =>
        //  {
        //      options.Authority = authorityEndPoint; // Auth Server
        //      options.RequireHttpsMetadata = false;
        //      options.ClientId = "ss_api"; // API Resource Id
        //  });

        //}

        //private static void AddAuthenticatedUser(IServiceCollection services)
        //{
        //    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //    services.AddScoped<AuthenticatedUser>();
        //}

        private static void AddMvc(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();

                //config.Filters.Add(new AuthorizeFilter(policy));
            });
        }
    }
}
