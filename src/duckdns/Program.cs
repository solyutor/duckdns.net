using Topshelf;
using Topshelf.HostConfigurators;

namespace Solyutor.DuckDns
{
    class Program
    {
        static int Main()
        {
            return (int)HostFactory.New(Service).Run();
        }

        private static void Service(HostConfigurator c)
        {
            c.SetServiceName("DuckDns.Net");
            c.SetDescription("Updated your domains within duckdns.org");

            c.Service<DuckDnsService>();
        }
    }
}