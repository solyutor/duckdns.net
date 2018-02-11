using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Topshelf;

namespace Solyutor.DuckDns
{
    [SuppressMessage("ReSharper", "ConsiderUsingConfigureAwait")]
    public class DuckDnsService : ServiceControl
    {
        private Task _task;
        private bool _stopped;

        public bool Start(HostControl hostControl)
        {
            _task = Task.Run((Action)UpdateDuckDns);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _stopped = true;
            return true;
        }

        private async void UpdateDuckDns()
        {
            while (!_stopped)
            {
                try
                {
                    await PerformUpdate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Dns update failed:" + Environment.NewLine + ex);
                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
            }
        }

        private async Task PerformUpdate()
        {
            if (await IpChanged())
            {
                Console.WriteLine("Ip address has changed. Updating DNS record");
                await CallDuckDns();
            }

            await Task.Delay(TimeSpan.FromMinutes(1));
        }

        private async Task<bool> IpChanged()
        {
            Task<IPAddress> dnsTask = IpResolver.ResolveDns("juso.duckdns.org");
            Task<IPAddress> realTask = IpResolver.ResolveIpfy();

            IPAddress dnsAddress = await dnsTask;
            IPAddress realAddress = await realTask;

            Console.WriteLine($"Ip check result: real={realAddress}, dns={dnsAddress}");
            return !dnsAddress.Equals(realAddress);
        }

        private Task CallDuckDns()
        {
            var domains = "juso,ultimaone";
            var token = "011ab114-c43d-494a-ac43-5d61ebd0cd3d";

            var url = $"https://www.duckdns.org/update?domains={Config.Domains}&token={Config.Token}";
            Console.WriteLine($"Call to {url}");
            return Http.Client.GetAsync(url);
        }
    }
}