using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Owin;
using Microsoft.Extensions.Configuration;

namespace Nowin.vNext {

    public class ServerFactory : IServerFactory {

        private Func<IFeatureCollection, Task> callback;
        
        private Task HandleRequest(IDictionary<string, object> env) {
            return callback(new OwinFeatureCollection(env));
        }

        public IFeatureCollection Initialize(IConfiguration configuration) {
            var information = new NowinServerInformation();
            information.Initialize(configuration);
            var features = new FeatureCollection();
            features.Set<NowinServerInformation>(information);
            return features;
        }

        public IDisposable Start(IFeatureCollection serverFeatures, Func<IFeatureCollection, Task> application) {
            callback = application;
            var information = serverFeatures.Get<NowinServerInformation>();
            var builder = Nowin.ServerBuilder.New().SetAddress(information.Address)
                               .SetPort(information.Port)
                               .SetOwinApp(OwinWebSocketAcceptAdapter.AdaptWebSockets(HandleRequest));
            var server = builder.Build();
            return server;
        }
    }

}
