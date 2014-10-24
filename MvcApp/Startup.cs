using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.DependencyInjection;

namespace MvcApp {

    public class Startup {

        public void Configure(IApplicationBuilder app) {
            app.UseServices(services => {
                services.AddMvc();
            });

            app.UseStaticFiles();

            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {
                        controller = "Home",
                        action = "Index"
                    }
                );

                routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{id?}"
                );
            });
        }
    }
}
