using Azure.Storage.Queues;
using BKM.Core.Notifications;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BKM.Infrastructure.EventHandlers
{
    public class LogEventHandler : INotificationHandler<CreateEntityNotification>
    {
        public async Task Handle(CreateEntityNotification notification, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(notification.QueueName) == false && string.IsNullOrEmpty(notification.StorageConnectionString) == false)
            {
                var queue = new QueueClient(notification.StorageConnectionString, notification.QueueName);
                await queue.CreateIfNotExistsAsync();
                var messageText = JsonConvert.SerializeObject(notification.Object);
                await queue.SendMessageAsync(messageText);
            }
        }
    }
}
