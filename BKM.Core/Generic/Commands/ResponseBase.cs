using System;

namespace BKM.Core.Generic
{
    public abstract class ResponseBase
    {
        public string Requester { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
