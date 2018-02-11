using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Solyutor.DuckDns
{
    [SuppressMessage("ReSharper", "ConsiderUsingConfigureAwait")]
    public static class IpResolver
    {
        public static async Task<IPAddress> ResolveIpfy()
        {
            HttpResponseMessage response = await Http.Client.GetAsync("https://api.ipify.org/");
            string ipstring = await response.Content.ReadAsStringAsync();
            return IPAddress.Parse(ipstring);
        }

        public static async Task<IPAddress> ResolveDns(string host)
        {
            var addresses = await Dns.GetHostAddressesAsync(host);
            return addresses[0];
        }
    }
}