﻿namespace SocketExhaustion
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // Run netstat in a console to see sockets being used
            // Use netstat -n on Windows to skip DNS resolution
            var i = 0;
            while (true)
            {
                using var client = new HttpClient();

                i++;
                // jsonplaceholder.typicode.com = 172.64.107.5
                var result = await client.GetAsync("https://jsonplaceholder.typicode.com/albums/1");
                Console.WriteLine($"Response {i}: {result.StatusCode}");
            }
        }
    }
}
