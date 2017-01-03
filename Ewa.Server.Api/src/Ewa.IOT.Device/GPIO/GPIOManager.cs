using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace Ewa.MessageObjects.Device.GPIO
{
    public static class GPIOManager
    {
        private static GpioController controller { get; }
        private static GpioPin LedPin { get; }


        static GPIOManager()
        {
            controller = GpioController.GetDefault();

            if (controller != null)
            {
                LedPin = controller.OpenPin(4);
                LedPin.SetDriveMode(GpioPinDriveMode.Output);
            }

        }


    }
}
