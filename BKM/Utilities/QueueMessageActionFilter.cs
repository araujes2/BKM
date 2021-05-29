using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace BKM.API.Utilities
{
    public class QueueMessageActionFilter : ActionFilterAttribute
    {
        private readonly ServiceOptions _options;
        public QueueMessageActionFilter(IOptions<ServiceOptions> options)
        {
            _options = options.Value;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var result = (ObjectResult)context.Result;
            if (result.StatusCode == 200)
            {
                var queue = new QueueClient(_options.StorageConnectionString, _options.QueueName);
                queue.CreateIfNotExistsAsync().GetAwaiter().GetResult();
                queue.SendMessageAsync(result.Value.ToString()).GetAwaiter().GetResult();
            }
        }
    }
}
