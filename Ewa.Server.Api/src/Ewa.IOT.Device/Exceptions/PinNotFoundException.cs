using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.IOT.Device.Exceptions
{
    public class PinNotFoundException : Exception
    {
        public PinNotFoundException()
        {
        }

        public PinNotFoundException(string message)
        : base(message)
        {
        }

        public PinNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }

    }
}
