using Ewa.MessageObjects;
using Ewa.MessageObjects.Commands;
using Ewa.Server.API.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ewa.Server.API.Operators
{
    public static class LightsOperator
    {
        public static async Task<string> OperateLight(string lightId, OnOffSwitch onoff)
        {

            var msg = new CommandMessage
            {
                DeviceId = "testdevice1",
                CommandType = MessageObjects.Commands.CommandTypes.OnOffCommand,
                CommandName = "turnthelighton",
                Command = new OnOffCommand
                {
                    OnOff = onoff,
                    TargetName = lightId
                }
            };

            return await IoTHubManager.SendCloudToDeviceMessageAsync(msg);
        }
    }
}
