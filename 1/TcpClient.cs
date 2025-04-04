using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetworkLab1; // Простір імен вашого проекту

namespace NetworkLab1.Clients
{
    public class TcpClient
    {
        public async Task ConnectAsync(string ip, int port, string message)
        {
            using (var client = new System.Net.Sockets.TcpClient())
            {
                await client.ConnectAsync(ip, port);
                Console.WriteLine($"Connected to {ip}:{port}");

                using (var stream = client.GetStream())
                using (var explainer = new Explainer()) 
                {
                   
                    byte[] data = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);
                    Console.WriteLine($"Sent: {message}");

                    
                    byte[] buffer = new byte[1024];
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Server response: {response}");

                    
                    using (var memoryStream = new MemoryStream(buffer, 0, bytesRead))
                    {
                        explainer.GotStream(memoryStream);
                    }
                }
            }
        }
    }
}