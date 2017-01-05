using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.IOT.Operator
{
    /// <summary>
    /// Common object to hold any operation result from the Operators
    /// </summary>
    public class OperationResult
    {
        public OperationState State { get; set; }
        public string FailMessage { get; set; }

        public OperationResult()
        {
        }

        public OperationResult(OperationState result)
        {
            this.State = result;
        }

        public OperationResult(OperationState result, string failedMessage)
        {
            this.State = result;
            this.FailMessage = failedMessage;
        }
    }

    /// <summary>
    /// State result of the operation
    /// </summary>
    public enum OperationState
    {
        Success = 1,
        Failed = 2
    }
}
