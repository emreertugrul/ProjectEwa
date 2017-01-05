using Ewa.IOT.Operator.Operators;
using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.IOT.Operator
{
    /// <summary>
    /// Entry point to operators. This broker will receive a command and initialize an appropriate operator and operate the command on the operator, and return the result
    /// </summary>
    public class OperatorCommandBroker
    {
        /// <summary>
        /// Operates any command on the appropriate operator
        /// </summary>
        /// <param name="command">Command to operate</param>
        /// <returns></returns>
        public static OperationResult OperateCommand(ICommand command)
        {
            OperationResult result = null;
            ICapabilityOperator coperator = GetOperator(command.CapabilityType);
            if (coperator != null)
            {
                result = coperator.Operate(command);
            }
            return result;
        }

        /// <summary>
        /// Gets/initializes the appropriate operator for the given control type.
        /// </summary>
        /// <param name="controlType">Device ControlType to get operator for</param>
        /// <returns></returns>
        private static ICapabilityOperator GetOperator(MessageObjects.Controls.ControlTypes controlType)
        {
            switch(controlType)
            {
                case MessageObjects.Controls.ControlTypes.GPIOPinOnOff:
                    return new GenericGPIOSwitchOperator();
            }

            return null;
        }
    }
}
