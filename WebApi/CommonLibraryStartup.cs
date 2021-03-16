using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommonLibraries.Config;
using CommonLibraries.RemoteCall.Extensions;
using CommonLibraries.WebApiPack.Extensions;
using Microsoft.OpenApi.Models;

namespace CommonLibraries.WebApiPack
{
    public abstract class CommonLibraryStartup
    {
        #region props

        public static IConfiguration Configuration { get; protected set; }

        protected List<string> SwaggerXmlCommentsFileNameList { get; set; } = new List<string>();


        #endregion

        #region ctor

        protected CommonLibraryStartup(bool reloadAppSettingsOnChange = true)
        {
            Configuration = new ConfigurationBuilder()
                .LoadConfiguration(reloadAppSettingsOnChange)
                .Build();
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.RegisterDateTimeService();
            //services.AddCaching();
            //services.AddSingleton(Configuration);
            services.RegisterRemoteCall();

            //if (SwaggerXmlCommentsFileNameList != null && SwaggerXmlCommentsFileNameList.Any())
            //{
            //    services.AddSwaggerGenWithXmlDocs(SwaggerXmlCommentsFileNameList, UseHideDocsFilter, SwaggerInfo);
            //}
            //else
            //{
            //    services.AddSwaggerGenWithDocs(UseHideDocsFilter, SwaggerInfo);
            //}


            //services.AddMvc()
            //        .AddJsonOptions(options =>
            //        {
            //            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            //        });


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

            ConfigureServiceCollections(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCommonLibraryLoggingVariables();

            app.UserApiErrorHandling();

            //app.UseCommonLibraryExceptionsHandling();

            //ConfigurePipelineAfterExceptionsHandling(app);

            //app.UseMvc();

            //app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigurePipelineAfterMvc(app);
        }

        protected abstract void ConfigureServiceCollections(IServiceCollection services);

        protected abstract void ConfigurePipelineAfterExceptionsHandling(IApplicationBuilder app);

        protected abstract void ConfigurePipelineAfterMvc(IApplicationBuilder app);
    }
}
