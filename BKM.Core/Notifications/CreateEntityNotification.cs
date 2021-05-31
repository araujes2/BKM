using BKM.Core.DTO;
using MediatR;

namespace BKM.Core.Notifications
{
    public class CreateEntityNotification : INotification
    {
        public string Entity { get; set; }
        public object Object { get; set; }
        public string StorageConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
