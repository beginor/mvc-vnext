using Microsoft.AspNet.Http.Features;

namespace Nowin.vNext {

    public class NowinServerInformation : FeatureCollection {

        public ServerBuilder Builder { get; }

        public NowinServerInformation(ServerBuilder builder) {
            Builder = builder;
        }

    }

}