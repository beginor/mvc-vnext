using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting.Server;
using Nowin;


namespace Nowin.vNext {

    public class NowinServerInformation : IServerInformation {

        public ServerBuilder Builder { get; }

        string IServerInformation.Name => "Nowin";

        public NowinServerInformation(ServerBuilder builder) {
            Builder = builder;
        }

    }

}