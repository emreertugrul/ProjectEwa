using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Commands
{
    public abstract class BaseCommand : ICommand
    {
    }


    public enum CommandTypes
    {
        OnOffCommand = 1
    }
}
