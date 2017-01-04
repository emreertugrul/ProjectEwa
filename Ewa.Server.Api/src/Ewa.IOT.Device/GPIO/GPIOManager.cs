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
        public static Dictionary<int, GpioPin> Pins { get; set; }


        static GPIOManager()
        {
            controller = GpioController.GetDefault();
            Pins = new Dictionary<int, GpioPin>();
        }

        public static void InitializePins(List<int> pins)
        {
            if (controller != null)
            {
                foreach (int i in pins)
                {
                    GpioPin pin = controller.OpenPin(i);
                    pin.Write(GpioPinValue.Low);
                    pin.SetDriveMode(GpioPinDriveMode.Output);
                    GpioPin pinExits;
                    if (!Pins.TryGetValue(i, out pinExits))
                    {
                        Pins.Add(i, pin);
                    }
                }
            }
        }

        public static void TurnPinOn(int pinNumber)
        {
            var pin = GetPin(pinNumber);
            pin.Write(GpioPinValue.High);
        }

        public static void TurnPinOff(int pinNumber)
        {
            var pin = GetPin(pinNumber);
            pin.Write(GpioPinValue.Low);
        }

        private static GpioPin GetPin(int pinNumber)
        {
            GpioPin pin;
            Pins.TryGetValue(pinNumber, out pin);
            if (pin == null)
            {
                throw new Exception("Pin is not initialized");
            }
            return pin;
        }
    }
}
