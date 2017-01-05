using Ewa.MessageObjects.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.IOT.Operator.Operators
{
    interface ICapabilityOperator
    {
        OperationResult Operate(ICommand command);
    }
}
