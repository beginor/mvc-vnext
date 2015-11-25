using System.Linq;

namespace Nowin.vNext {

    public class Program {

        public static void Main(string[] args) {
            var mergedArgs = new [] { "--server", "Nowin.vNext" }.Concat(args).ToArray();
            Microsoft.AspNet.Hosting.Program.Main(mergedArgs);
        }
    }
}