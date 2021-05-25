using System;

namespace BKM.Core.Generic
{
    public abstract class CommandResponseBase
    {
        public string Requester { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
