using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Text;
using Ewa.MessageObjects;
using Newtonsoft.Json;

namespace Ewa.Server.API.Utils
{
    public static class IoTHubManager
    {
        static ServiceClient serviceClient;
        static string connectionString = Constants.IOTHUBCONNECTIONSTRING;

        static IoTHubManager()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        public async static Task SendCloudToDeviceMessageAsync(IMessage message)
        {
            var messageString = JsonConvert.SerializeObject(message);
            var msg = new Message(Encoding.ASCII.GetBytes(messageString));
            await serviceClient.SendAsync(message.DeviceId, msg);
        }
    }
}
