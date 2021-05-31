using BKM.Core.DTO;
using MediatR;

namespace BKM.Core.Notifications
{
    public class CreateBookNotification : INotification
    {
        public string QueueName { get; set; }
        public string StorageConnectionString { get; set; }
        public DtoBook Book { get; set; }
    }
}
