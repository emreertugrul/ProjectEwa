using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Messaging
{
    public class BaseCommandMessage : IMessage
    {
        public string MessageName { get; set; }
        public MessageTypes MessageType { get; set; }
        public string DeviceId { get; set; }
    }
}
