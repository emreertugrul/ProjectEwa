using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.Controls
{
    /// <summary>
    /// GPIO related devices
    /// </summary>
    public class GPIOPinControl : IDeviceControl
    {
        public string Name { get; set; }
        public string Synonim { get; set; }
        public int DevicePinNumber { get; set; }


    }
}
