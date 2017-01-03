using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects
{
    public class CommandMessage : IMessage
    {
        public string CommandName { get; set; }
        public CommandTypes CommandType { get; set; }
        public ICommand Command { get; set; }

        public string DeviceId { get; set; }
    }

}
