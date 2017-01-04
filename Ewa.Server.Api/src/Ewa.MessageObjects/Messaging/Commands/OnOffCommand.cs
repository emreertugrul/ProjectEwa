using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Commands
{
    public class OnOffCommand :BaseCommand, ICommand
    {
        public OnOffSwitch OnOff { get; set; }
        public string TargetName { get; set; }        
    }

    public enum OnOffSwitch
    {
        Off = 0,
        On = 1
    }
}
