using System;
using System.Threading.Tasks;
using NetworkLab1.Servers;
using NetworkLab1.Clients;

namespace NetworkLab1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            var server = new TcpServer();
            server.Start(8888);

            
            await Task.Delay(500);

            
            var client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 8888, "Hello from client!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            server.Stop();
        }
    }
}