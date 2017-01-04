using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ewa.Server.API.Hubs
{
    /// <summary>
    /// SignalR hub that handles client app's signalR feedbacks
    /// </summary>
    [HubName("IOTMessageHub")]
    public class IOTMessageHub :Hub
    {
        public void SendMessageConfirmationToClient(string clientId, string messageId)
        {
            Clients.Client(clientId).SendConfirmation(messageId);
        }
    }
}
