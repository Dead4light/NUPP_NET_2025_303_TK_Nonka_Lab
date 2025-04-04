using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetworkLab1; 

namespace NetworkLab1.Servers
{
    public class TcpServer
    {
        private TcpListener _listener;
        private bool _isRunning;

        public void Start(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            _isRunning = true;
            Console.WriteLine($"TCP Server started on port {port}");

            Task.Run(() => AcceptClients());
        }

        private async Task AcceptClients()
        {
            while (_isRunning)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");
                    Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    if (_isRunning)
                        Console.WriteLine($"Accept error: {ex.Message}");
                }
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            using (client)
            using (var stream = client.GetStream())
            using (var explainer = new Explainer()) 
            {
                byte[] buffer = new byte[1024];
                try
                {
                    while (_isRunning)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;

                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {message}");

                        
                        using (var memoryStream = new MemoryStream(buffer, 0, bytesRead))
                        {
                            explainer.GotStream(memoryStream);
                        }

                       
                        byte[] response = Encoding.UTF8.GetBytes($"ACK: {message}");
                        await stream.WriteAsync(response, 0, response.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Client error: {ex.Message}");
                }
            }
            Console.WriteLine("Client disconnected");
        }

        public void Stop()
        {
            _isRunning = false;
            _listener?.Stop();
            Console.WriteLine("Server stopped");
        }
    }
}