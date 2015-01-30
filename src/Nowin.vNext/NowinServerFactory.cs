using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Owin;
using Microsoft.Framework.ConfigurationModel;

namespace Nowin.vNext {

    public class NowinServerFactory : IServerFactory {

        private Func<object, Task> callback;

        private Task HandleRequest(IDictionary<string, object> env) {
            return callback(new OwinFeatureCollection(env));
        }

        public IServerInformation Initialize(IConfiguration configuration) {
            // simple Parse config
            var server = configuration.Get("server");
            var serverUrls = configuration.Get("server.urls");
            Console.WriteLine("Owin server is: {0}, listening at {1}", server, serverUrls);

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

            var builder = ServerBuilder.New()
                                       .SetAddress(ip)
                                       .SetPort(port)
                                       .SetOwinApp(OwinWebSocketAcceptAdapter.AdaptWebSockets(HandleRequest));

            return new NowinServerInformation(builder);
        }

        public IDisposable Start(IServerInformation serverInformation, Func<object, Task> application) {
            var information = (NowinServerInformation)serverInformation;
            callback = application;
            INowinServer server = information.Builder.Build();
            server.Start();
            return server;
        }

        private class NowinServerInformation : IServerInformation {

            public NowinServerInformation(ServerBuilder builder) {
                Builder = builder;
            }

            public ServerBuilder Builder { get; private set; }

            public string Name => "Nowin";
        }
    }
}
