using System;
using System.IO;

namespace Solyutor.DuckDns
{
    public static class Config
    {
        public static string Token { get; }

        public static string Domains { get; }

        static Config()
        {
            var directory = new Uri(Path.GetDirectoryName(typeof(Config).Assembly.CodeBase)).AbsolutePath;
            var configFile = Path.Combine(directory, "duckdns.config");
            Console.WriteLine($"Looking up config at '{configFile}'");
            var lines = File.ReadAllLines(configFile);

            foreach (string line in lines)
            {
                Token = line.GetValue("token") ?? Token;
                Domains = line.GetValue("domains") ?? Domains;
            }
        }

        private static string GetValue(this string line, string key)
        {
            return line.StartsWith(key, StringComparison.OrdinalIgnoreCase)
                ? line.Replace(key + "=", string.Empty, StringComparison.OrdinalIgnoreCase).Trim()
                : null;
        }
    }
}