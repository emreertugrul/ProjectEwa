using Ewa.MessageObjects.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Commands
{
    /// <summary>
    /// Issues a GPIO on/off/initialize command for a GPIOPinControl controltype
    /// </summary>
    public class GPIOOnOffCommand : BaseCommand, ICommand
    {
        public OnOffSwitch OnOff { get; set; }
        public GPIOPinControl DeviceControl { get; set; }

        public GPIOOnOffCommand(ControlTypes capabilityType)
        {
            this.CapabilityType = capabilityType;
        }
    }

    public enum OnOffSwitch
    {
        Off = 0,
        On = 1,
        Initialize = 2
    }
}
