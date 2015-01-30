using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Logging;

namespace MvcApp {

    public class Startup {

        //private  Microsoft.Framework.ConfigurationModel.IConfiguration configuration;

        public Startup(IHostingEnvironment env) {
            //configuration = new Microsoft.Framework.ConfigurationModel.Configuration();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory) {
            loggerfactory.AddConsole();
            app.UseStaticFiles();

            app.UseMvc(routes => {
                //
                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{id?}"
                );
                //
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {
                        controller = "Home",
                        action = "Index"
                    }
                );

            });
        }
    }
}
