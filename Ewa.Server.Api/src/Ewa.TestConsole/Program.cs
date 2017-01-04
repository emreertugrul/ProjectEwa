using Ewa.MessageObjects;
using Ewa.MessageObjects.Commands;
using Ewa.MessageObjects.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Establishing connection to signalr");

            var hubConnection = new HubConnection("http://localhost:5000");
            IHubProxy stockTickerHubProxy = hubConnection.CreateHubProxy("IOTMessageHub");
            stockTickerHubProxy.On<OperationConfirmationMessage>("OperationConfirmationMessage", c =>
            {
                Console.WriteLine($"Operation:{c.OriginalMessageId} is {c.IsSuccess.ToString()}");
            });

            hubConnection.Start();

            Console.WriteLine("Enter 1 to turn on, 0 to turn off");



            while (true)
            {
               string command = Console.ReadLine();

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                

                var res = client.PostAsync($"http://localhost:5000/api/lights/4/{command}", null).Result;


                Console.WriteLine($"Operation:{ res.Content.ReadAsStringAsync().Result}");


            }
        }
    }
}
