using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects
{
    public interface IMessage
    {
        string DeviceId { get; set; }
    }
}
