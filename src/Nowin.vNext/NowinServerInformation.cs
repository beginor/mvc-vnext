using Microsoft.AspNet.Hosting.Server;

namespace Nowin.vNext {

    public class NowinServerInformation : IServerInformation {

        public ServerBuilder Builder { get; }

        string IServerInformation.Name => "Nowin";

        public NowinServerInformation(ServerBuilder builder) {
            Builder = builder;
        }

    }

}