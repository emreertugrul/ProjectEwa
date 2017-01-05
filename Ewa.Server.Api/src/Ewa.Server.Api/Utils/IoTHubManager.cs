using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using System.Text;
using Ewa.MessageObjects;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
using Ewa.Server.API.Hubs;
using Ewa.MessageObjects.SignalR;

namespace Ewa.Server.API.Utils
{
    /// <summary>
    /// Manages IOT Hub interactions for the web api
    /// </summary>
    public static class IoTHubManager
    {
        static ServiceClient serviceClient;
        static string connectionString = Constants.IOTHUBCONNECTIONSTRING;

        static IoTHubManager()
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            ReceiveFeedbackAsync();
        }

        /// <summary>
        /// Outing point for messages to IOT Hub
        /// </summary>
        /// <param name="message">Message template to send</param>
        /// <returns></returns>
        public async static Task<string> SendCloudToDeviceMessageAsync(IMessage message)
        {
            var messageString = JsonConvert.SerializeObject(message);
            var msg = new Message(Encoding.ASCII.GetBytes(messageString));
            msg.MessageId = Guid.NewGuid().ToString(); // Give it a unique message id, this will be used to track this message on feedback etc.
            msg.Ack = DeliveryAcknowledgement.Full; // We want full feedback acknowledgement of this message
            msg.ExpiryTimeUtc = DateTime.UtcNow.AddSeconds(30);
            await serviceClient.SendAsync(message.DeviceId, msg);
            return msg.MessageId; // return this to the client to record (then combine with feedback)
        }

        /// <summary>
        /// Starts receiving feedback messages from the IOT Hub. Feedback messages are sent back to calling client thru SignalR hub
        /// </summary>
        private async static void ReceiveFeedbackAsync()
        {
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();
            IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<IOTMessageHub>();

            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;

                foreach (var feedback in feedbackBatch.Records)
                {
                    var confirmationMessage = new OperationConfirmationMessage
                    {
                        OriginalMessageId = feedback.OriginalMessageId, //Original message Id
                        IsSuccess = feedback.StatusCode == FeedbackStatusCode.Success //Operation succeeded or not, coming from device.
                    };
                    //Send to signalr hub
                    //TODO: Only send to client that sent this message!
                    hubContext.Clients.All.OperationConfirmationMessage(confirmationMessage);
                }
                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }
    }
}
