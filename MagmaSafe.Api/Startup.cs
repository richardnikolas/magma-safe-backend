using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Dapper;
using System;
using System.Data.Common;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Globalization;
using System.Reflection;
using MagmaSafe.Api.Models;
using MagmaSafe.Api.Extensions;
using MagmaSafe.Api.Configurations;
using MagmaSafe.Repositories.Helpers;
using MagmaSafe.Shared.Helpers;

namespace MagmaSafe
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string CorsPolicy = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationConfig = Configuration.LoadConfiguration();

            services.AddSingleton(applicationConfig);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IActionResultConverter, ActionResultConverter>();

            if (applicationConfig.CorsOrigins?.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithOrigins(applicationConfig.CorsOrigins)
                        .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                        .AllowAnyMethod();
                    });
                });
            }

            SqlMapper.AddTypeHandler(new DateTimeHandler());

            RepositoryConfig.ConfigureServices(services, applicationConfig);
            UseCaseConfig.ConfigureServices(services);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseCors(CorsPolicy);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
