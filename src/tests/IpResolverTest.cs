using System;
using FluentAssertions;
using Solyutor.DuckDns;
using Xunit;

namespace tests
{
    public class IpResolverTest
    {
        [Fact]
        public void Should_resolve_ip_address_using_ipfy()
        {
            var address = IpResolver.ResolveIpfy().Result;
            Console.Write(address.ToString());

            address.Should().NotBeNull();
        }

        [Fact]
        public void Should_resolve_ip_address_using_dns()
        {
            var address = IpResolver.ResolveDns("juso.duckdns.org").Result;

            Console.Write(address.ToString());

            address.Should().NotBeNull();
        }
    }
}