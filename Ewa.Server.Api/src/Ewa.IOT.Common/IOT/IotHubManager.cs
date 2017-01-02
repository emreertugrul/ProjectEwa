using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Ewa.IOT.Common.Settings;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace Ewa.IOT.Common.IOT
{
    /// <summary>
    /// Manages IOT Hub operations
    /// </summary>
    public static class IotHubManager
    {
        private static DeviceClient client;

        public static DeviceClient Client
        {
            get
            {
                if (client == null)
                {
                    InitializeDeviceClient();
                }
                return client;
            }
        }       

        /// <summary>
        /// Initializes the singleton DeviceClient instance if not already initialized. Throws exception if DeviceId or DeviceKey are not set.
        /// </summary>
        public static void InitializeDeviceClient()
        {
            if (SettingsManager.IOTDeviceId == null || SettingsManager.IOTHubDeviceKey == null)
            {
                throw new Exception("Device not registered. Register the device with IOT Hub first.");
            }
            client = DeviceClient.Create(Constants.IOT_HUB_URI,
                new DeviceAuthenticationWithRegistrySymmetricKey(SettingsManager.IOTDeviceId, SettingsManager.IOTHubDeviceKey));
        }


        public static async Task SendDeviceToCloudMessagesAsync()
        {
            try
            {
                var telemetryDataPoint = new
                {
                    deviceId = SettingsManager.IOTDeviceId,
                    message = "Hello @" + DateTime.Now.ToString()
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
                await client.SendEventAsync(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Begins receiving messages from the Azure SB.
        /// </summary>
        /// <param name="f">Will be invoked for each message received. Should return True if message is processed.</param>
        /// <returns></returns>
        public static async Task StartReceiveCloudToDeviceMessagesAsync(Func<string,bool> f)
        {
            while (true)
            {
                Message receivedMessage = await client.ReceiveAsync();
                if (receivedMessage == null) continue;

                var msg = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                var result = f?.Invoke(msg);
                //TODO: Handle message when the function returns false

                await client.CompleteAsync(receivedMessage);
            }
        }

        public static async Task StopReceivingCloudToDeviceMessagesAsync()
        {
            await client.CloseAsync();
        }       
    }
}
