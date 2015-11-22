using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Owin;
using Microsoft.Extensions.Configuration;

namespace Nowin.vNext {

    public class NowinServerFactory : IServerFactory {

        private Func<IFeatureCollection, Task> callback;

        IFeatureCollection IServerFactory.Initialize(IConfiguration configuration) {
            // adapt aspnet to owin app;
            var owinApp = OwinWebSocketAcceptAdapter.AdaptWebSockets(HandleRequest);

            // Get server info, write to console.
            var server = configuration["server"];
            var serverUrls = configuration["server.urls"];
            Console.WriteLine("Owin server is: {0}, listening at {1}", server, serverUrls);
            // parse ip address and port.
            var uri = new Uri(serverUrls, UriKind.Absolute);
            IPAddress ip;
            if (!IPAddress.TryParse(uri.Host, out ip)) {
                if (uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)) {
                    ip = IPAddress.Parse("127.0.0.1");
                }
                else {
                    ip = IPAddress.Any;
                }
            }
            var port = uri.Port;

            // build nowin server;
            var builder = ServerBuilder.New()
                .SetAddress(ip)
                .SetPort(port)
                .SetOwinApp(owinApp);

            var serverInfo = new NowinServerInformation(builder);
            var rev = serverInfo.Revision;
            
            return serverInfo;
        }

        private Task HandleRequest(IDictionary<string, object> env) {
            return callback(new OwinFeatureCollection(env));
        }

        IDisposable IServerFactory.Start(IFeatureCollection serverInformation, Func<IFeatureCollection, Task> application) {
            var info = (NowinServerInformation)serverInformation;
            var server = info.Builder.Build();
            callback = application;
            server.Start();
            return server;
        }

    }
}
