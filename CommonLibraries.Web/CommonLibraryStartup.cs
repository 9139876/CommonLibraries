using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using CommonLibraries.Config;
using CommonLibraries.RemoteCall.Extensions;
using CommonLibraries.Web.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace CommonLibraries.Web
{
    public abstract class CommonLibraryStartup
    {
        #region props

        public static IConfiguration Configuration { get; protected set; }

        protected List<string> SwaggerXmlCommentsFileNameList { get; set; } = new List<string>();

        protected abstract bool _loadFromConfigService { get; }
        protected abstract bool _reloadAppSettingsOnChange { get; }
        protected abstract bool _requiredConfigService { get; }

        #endregion

        #region ctor

        protected CommonLibraryStartup()
        {
            Configuration = new ConfigurationBuilder()
                .LoadConfiguration(loadFromConfigService:_loadFromConfigService, reloadAppSettingsOnChange: _reloadAppSettingsOnChange, requiredConfigService: _requiredConfigService)
                .Build();
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            services.RegisterRemoteCall();

            services.AddMvc();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                var assemblyInfo = AssemblyInfo.GetAssemblyInfo();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = assemblyInfo.Version,
                    Title = assemblyInfo.Title,
                    Description = assemblyInfo.Description
                });
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();

            ConfigureServiceCollections(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureApplication(app, env);
        }

        protected void ConfigureWebApi(IApplicationBuilder app, IWebHostEnvironment env, bool useSwagger = true)
        {
            app.UseErrorHandling();

            ConfigurePipelineAfterExceptionsHandling(app);

            if (useSwagger == true)
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigurePipelineAfterMvc(app);
        }

        protected void ConfigureWebApp(IApplicationBuilder app, IWebHostEnvironment env, bool useSwagger = true)
        {
            app.UseErrorHandling();

            ConfigurePipelineAfterExceptionsHandling(app);

            app.UseStaticFiles();

            app.UseRouting();

            ConfigureRoutes(app);

            if (useSwagger == true)
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            ConfigurePipelineAfterMvc(app);
        }

        protected void ConfigureWebAppStaticFiles(IApplicationBuilder app, IWebHostEnvironment env, bool useSwagger = true)
        {
            app.UseErrorHandling();

            ConfigurePipelineAfterExceptionsHandling(app);

            app.UseFileServer();

            app.UseRouting();

            ConfigureRoutes(app);

            if (useSwagger == true)
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            ConfigurePipelineAfterMvc(app);
        }

        protected abstract void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env);

        protected abstract void ConfigureServiceCollections(IServiceCollection services);

        protected abstract void ConfigurePipelineAfterExceptionsHandling(IApplicationBuilder app);

        protected abstract void ConfigurePipelineAfterMvc(IApplicationBuilder app);

        protected abstract void ConfigureRoutes(IApplicationBuilder app);
    }
}
