using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    public class Program
    {
        static string time = "";
        static void Main()
        {
            ConnectSV().Wait();
        }

        static async Task ConnectSV()
        {
            HubConnection connection;
            connection = new HubConnectionBuilder().WithUrl("http://localhost:51327/realTime").Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync()
                ;
            };

            try
            {
                await connection.StartAsync();
                Console.WriteLine("Connect sucess!");
                while (true)
                {
                    time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    await connection.InvokeAsync("SendMessage", "client", time);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
