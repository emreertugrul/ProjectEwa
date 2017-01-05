using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Messaging
{
    /// <summary>
    /// Contains a GPIOOnOffCommand, to be sent cloud2device for operating GPIO related device controls
    /// </summary>
    public class GPIOOnOffCommandMessage : BaseCommandMessage, IMessage
    {
        public GPIOOnOffCommand Command { get; set; }
        
    }

}
