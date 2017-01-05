using Ewa.MessageObjects;
using Ewa.MessageObjects.Commands;
using Ewa.MessageObjects.Messaging;
using Ewa.Server.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ewa.Server.API.Operators
{
    public static class LightsOperator
    {
        public static async Task<string> OperateLight(string lightName, OnOffSwitch onoff)
        {
            // Go to database and find out which deviceId has the lightName, and get it's properties (gpiopin etc)
            var msg = new GPIOOnOffCommandMessage
            {
                DeviceId = "testdevice1",
                MessageType = MessageObjects.Commands.MessageTypes.GPIOOnOf,
                MessageName = "turnthelighton",
                Command = new GPIOOnOffCommand(MessageObjects.Controls.ControlTypes.GPIOPinOnOff)
                {
                    OnOff = onoff,
                    DeviceControl = new MessageObjects.Controls.GPIOPinControl
                    {
                        DevicePinNumber = 4,
                        Name = lightName,
                        Synonim = lightName
                    }
                }
            };

            return await IoTHubManager.SendCloudToDeviceMessageAsync(msg);
        }
    }
}
