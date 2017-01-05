using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ewa.MessageObjects.Commands;
using Ewa.MessageObjects.Device.GPIO;
using Ewa.IOT.Device.Exceptions;

namespace Ewa.IOT.Operator.Operators
{
    /// <summary>
    /// Handles GPIO On/Off Devices
    /// </summary>
    public class GenericGPIOSwitchOperator : ICapabilityOperator
    {
        public OperationResult Operate(GPIOOnOffCommand command)
        {
            try
            {
                if (command.OnOff == OnOffSwitch.On) //Turn on the pin
                {
                    GPIOManager.TurnPinOn(command.DeviceControl.DevicePinNumber);
                }
                else if (command.OnOff == OnOffSwitch.Off) //Turn off the pin
                {
                    GPIOManager.TurnPinOff(command.DeviceControl.DevicePinNumber);
                }
                else if (command.OnOff == OnOffSwitch.Initialize) //Initialize the pin
                {
                    GPIOManager.InitializePins(new List<int> { command.DeviceControl.DevicePinNumber });
                }

                return new OperationResult(OperationState.Success);
            }
            catch (PinNotFoundException ex)
            {
                //TODO: Logging?
                return new OperationResult(OperationState.Failed, ex.Message);
            }
            catch (Exception ex)
            {
                //TODO: Logging?
                return new OperationResult(OperationState.Failed, ex.Message);
            }
        }

        /// <summary>
        /// Implementing interface explicitly
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        OperationResult ICapabilityOperator.Operate(ICommand command)
        {
            return Operate((GPIOOnOffCommand)command);
        }
    }
}
