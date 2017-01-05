using Ewa.MessageObjects.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Commands
{
    public interface ICommand
    {
        ControlTypes CapabilityType { get; }
        
    }
}
