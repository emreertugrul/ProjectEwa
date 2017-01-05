using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ewa.MessageObjects.Controls;

namespace Ewa.MessageObjects.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public ControlTypes CapabilityType { get; protected set; }
     
    }


    public enum MessageTypes
    {
        GPIOOnOf = 1
    }
}
