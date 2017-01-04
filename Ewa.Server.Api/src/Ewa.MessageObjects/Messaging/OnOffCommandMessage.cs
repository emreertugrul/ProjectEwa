using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Messaging
{
    public class OnOffCommandMessage : BaseCommandMessage, IMessage
    {
        public OnOffCommand Command { get; set; }
        
    }

}
