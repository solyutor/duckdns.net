using System.Net.Http;

namespace Solyutor.DuckDns
{
    public static class Http
    {
        public static readonly HttpClient Client = new HttpClient();
    }
}