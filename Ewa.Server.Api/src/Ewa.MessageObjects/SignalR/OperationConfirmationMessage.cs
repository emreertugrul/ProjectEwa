using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.MessageObjects.SignalR
{
    /// <summary>
    /// Can be used for sending back feedback messages to calling client applications, informing them about the operation success.
    /// </summary>
    public class OperationConfirmationMessage
    {
        public string OriginalMessageId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
