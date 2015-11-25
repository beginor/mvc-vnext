using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Nowin.vNext {

    public class NowinServerInformation {

        public IPAddress Address { get; private set; }

        public int Port { get; private set; }

        public void Initialize(IConfiguration configuration) {
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
            Address = ip;
            Port = uri.Port;
        }

    }

}