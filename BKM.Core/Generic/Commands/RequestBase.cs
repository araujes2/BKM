using System;

namespace BKM.Core.Generic
{
    public abstract class RequestBase
    {
        public string Requester { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
