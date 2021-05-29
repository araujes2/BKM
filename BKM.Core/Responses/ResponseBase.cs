using System;

namespace BKM.Core.Responses
{
    public abstract class ResponseBase
    {
        public int Status { get; set; }
        public string Requester { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
